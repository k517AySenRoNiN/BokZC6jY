// 代码生成时间: 2025-09-17 05:59:00
using System;
using System.IO;
using System.Web;
using EPPlus;

namespace ExcelGeneratorApp
{
    // Exception class for Excel generation errors
    public class ExcelGenerationException : Exception
    {
        public ExcelGenerationException(string message) : base(message)
        {
        }
    }

    public class ExcelGenerator
    {
        // Method to generate Excel file
        public static void GenerateExcel(string filePath, string sheetName, object data)
        {
            try
            {
                // Ensure the file path ends with .xlsx extension
                if (!filePath.EndsWith(".xlsx")) filePath += ".xlsx";

                // Create a new Excel package
                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet with the provided sheet name
                    var worksheet = package.Workbook.Worksheets.Add(sheetName);

                    // Convert the data to a DataTable (assuming data is a list of objects)
                    var dataTable = ConvertDataToDataTable(data);

                    // Load data into the worksheet
                    worksheet.Cells[1, 1].LoadFromDataTable(dataTable, true);

                    // Save the package to the specified file path
                    FileInfo fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during Excel generation
                throw new ExcelGenerationException($"Error generating Excel file: {ex.Message}");
            }
        }

        // Helper method to convert data to a DataTable
        private static ExcelWorksheet ConvertDataToDataTable(object data)
        {
            // Assuming data is a collection of objects with properties
            // Create a new DataTable with columns derived from the object properties
            var dataTable = new ExcelWorksheet();
            var properties = data.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                // Add columns to the DataTable based on property names
                dataTable.Cells[1, i + 1].Value = properties[i].Name;
            }

            // Add rows to the DataTable based on object property values
            foreach (var item in (dynamic[])data)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    // Set cell values based on property values
                    dataTable.Cells[i + 2, i + 1].Value = properties[i].GetValue(item);
                }
            }

            return dataTable;
        }
    }
}

// Usage example:
// var data = new List<MyDataObject> { new MyDataObject { Property1 = "Value1", Property2 = "Value2" } };
// ExcelGenerator.GenerateExcel("path/to/your/excelfile.xlsx", "MySheet", data);
