// 代码生成时间: 2025-08-29 02:10:42
using System;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// 防止SQL注入的示例类
/// </summary>
public class SqlInjectionPrevention
{
    /// <summary>
    /// 获取数据库连接字符串
    /// </summary>
    private string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
    }

    /// <summary>
    /// 使用参数化查询防止SQL注入
    /// </summary>
    /// <param name="username">用户名称</param>
    /// <returns>查询结果</returns>
    public DataTable SearchByUsername(string username)
    {
        // 使用参数化查询来防止SQL注入
        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        return result;
                    }
                }
            }
            catch (SqlException ex)
            {
                // 错误处理
                Console.WriteLine("SQL Exception: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                // 其他异常处理
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}
