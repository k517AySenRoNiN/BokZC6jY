// 代码生成时间: 2025-09-23 13:14:08
// <copyright file="ExcelGenerator.cs" company="YourCompany">
//   Copyright (c) YourCompany. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using OfficeOpenXml;

namespace YourNamespace
{
    /// <summary>
    /// Excel表格自动生成器，用于创建和保存Excel文件。
    /// </summary>
    public class ExcelGenerator
    {
        private const string ExcelFileExtension = ".xlsx";
        private readonly string _connectionString;

        /// <summary>
        /// 构造函数，初始化数据库连接字符串。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public ExcelGenerator(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 生成Excel文件。
        /// </summary>
        /// <param name="dataTable">包含数据的DataTable</param>
        /// <param name="filePath">文件保存路径</param>
        /// <returns>生成的Excel文件路径</returns>
        public string GenerateExcel(DataTable dataTable, string filePath)
        {
            try
            {
                // 确保DataTable不为空
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new ArgumentException("DataTable cannot be null or empty.");
                }

                // 确保文件路径不为空
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentException("File path cannot be empty.");
                }

                // 确保文件路径以.xlsx结尾
                if (!filePath.EndsWith(ExcelFileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    filePath += ExcelFileExtension;
                }

                // 使用EPPlus库创建Excel文件
                using (var excelPackage = new ExcelPackage())
                {
                    // 添加一个工作表
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                    // 将DataTable写入工作表
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true, TableStyles.None);

                    // 保存Excel文件
                    File.WriteAllBytes(filePath, excelPackage.GetAsByteArray());
                }

                return filePath;
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 从数据库中获取数据并生成Excel文件。
        /// </summary>
        /// <param name="query">SQL查询字符串</param>
        /// <param name="filePath">文件保存路径</param>
        /// <returns>生成的Excel文件路径</returns>
        public string GenerateExcelFromDatabase(string query, string filePath)
        {
            try
            {
                // 使用OleDb连接数据库并执行查询
                using (var connection = new OleDbConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new OleDbCommand(query, connection))
                    {
                        using (var adapter = new OleDbDataAdapter(command))
                        {
                            // 执行查询并获取DataTable
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // 调用GenerateExcel方法生成Excel文件
                            return GenerateExcel(dataTable, filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
