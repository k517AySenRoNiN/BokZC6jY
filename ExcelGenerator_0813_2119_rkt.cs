// 代码生成时间: 2025-08-13 21:19:46
using System;
using System.IO;
using System.Data;
using System.Text;
using ClosedXML.Excel;

// 定义ExcelGenerator类，用于生成Excel表格
public class ExcelGenerator
{
    // 文件路径
    private string filePath;

    // 构造函数，传入文件路径
    public ExcelGenerator(string filePath)
    {
        this.filePath = filePath;
    }

    // 生成Excel表格的方法
    public void GenerateExcel(DataTable dataTable)
    {
        try
        {
            // 使用ClosedXML库创建Excel工作簿
            using (var workbook = new XLWorkbook())
            {
                // 添加工作表
                var worksheet = workbook.Worksheets.Add(dataTable.TableName);

                // 添加标题行
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                }

                // 添加数据行
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = dataTable.Rows[i][j];
                    }
                }

                // 保存工作簿到文件
                workbook.SaveAs(filePath);
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error generating Excel file: {ex.Message}");
        }
    }
}

// 示例用法
class Program
{
    static void Main(string[] args)
    {
        try
        {
            // 创建DataTable实例
            DataTable table = new DataTable("SampleTable");
            table.Columns.Add("Column1", typeof(string));
            table.Columns.Add("Column2", typeof(int));

            // 添加数据行
            table.Rows.Add("Row 1", 1);
            table.Rows.Add("Row 2", 2);

            // 创建ExcelGenerator实例
            ExcelGenerator generator = new ExcelGenerator("Sample.xlsx");

            // 生成Excel文件
            generator.GenerateExcel(table);

            Console.WriteLine("Excel file generated successfully.");
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}