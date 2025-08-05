// 代码生成时间: 2025-08-05 15:20:39
using System;
using System.IO;
using System.Data;
using System.Text;
using System.Data.OleDb;
using ClosedXML.Excel;
using Excel = ClosedXML.Excel.ExcelWorksheet;

// 定义Excel表格自动生成器类
public class ExcelGenerator
{
    // 构造函数，初始化Excel工作簿
    public ExcelGenerator()
    {
        workbook = new XLWorkbook();
        worksheet = workbook.Worksheets.Add("Sheet1");
    }

    // 私有成员，存储工作簿和工作表
    private XLWorkbook workbook;
    private Excel worksheet;

    // 向工作表添加标题行
    public void AddHeader(string[] headers)
    {
        int row = 1;
        foreach (var header in headers)
        {
            worksheet.Cell(row, worksheet.Dimension.End.Column + 1).Value = header;
        }
    }

    // 向工作表添加数据行
    public void AddDataRow(object[] rowData)
    {
        int row = worksheet.Dimension.End.Row + 1;
        foreach (var data in rowData)
        {
            worksheet.Cell(row, worksheet.Dimension.End.Column + 1).Value = data;
        }
    }

    // 保存工作簿到文件
    public void SaveToFile(string filePath)
    {
        try
        {
            workbook.SaveAs(filePath);
        }
        catch (Exception ex)
        {
            // 错误处理: 记录异常信息
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    // 销毁工作簿和工作表对象
    public void Dispose()
    {
        workbook.Dispose();
        worksheet.Dispose();
    }
}

// 使用示例
public class Program
{
    public static void Main(string[] args)
    {
        var generator = new ExcelGenerator();
        try
        {
            // 添加标题行
            generator.AddHeader(new string[] { "Column1", "Column2", "Column3" });

            // 添加数据行
            generator.AddDataRow(new object[] { "Data1", 123, "Data3" });
            generator.AddDataRow(new object[] { "Data4", 456, "Data6" });

            // 保存工作簿到文件
            generator.SaveToFile("Example.xlsx");
        }
        catch (Exception ex)
        {
            // 错误处理: 记录异常信息
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // 销毁Excel生成器对象
            generator.Dispose();
        }
    }
}