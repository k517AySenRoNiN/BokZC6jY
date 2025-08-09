// 代码生成时间: 2025-08-09 18:04:49
using System;
# TODO: 优化性能
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace ApiFormatterTool
{
# NOTE: 重要实现细节
    /// <summary>
    /// API响应格式化工具
    /// </summary>
    public static class ApiFormatterTool
    {
        /// <summary>
        /// 格式化API响应
        /// </summary>
        /// <typeparam name="T">响应数据类型</typeparam>
        /// <param name="response">HTTP响应对象</param>
        /// <returns>格式化后的响应数据</returns>
# 添加错误处理
        public static T FormatResponse<T>(HttpResponseMessage response) where T : new()
        {
            try
            {
                // 检查HTTP响应状态码
                if (!response.IsSuccessStatusCode)
                {
# FIXME: 处理边界情况
                    throw new Exception(\$"HTTP请求失败，状态码：{response.StatusCode}");
                }

                // 读取HTTP响应内容
                string content = response.Content.ReadAsStringAsync().Result;

                // 尝试将响应内容转换为目标类型
                T result = JsonConvert.DeserializeObject<T>(content);

                // 检查转换结果
                if (result == null)
                {
                    throw new Exception("JSON反序列化失败");
# 扩展功能模块
                }

                // 返回格式化后的响应数据
# FIXME: 处理边界情况
                return result;
            }
            catch (Exception ex)
            {
                // 记录错误信息
                Console.WriteLine(\$"格式化API响应出错：{ex.Message}");
# 优化算法效率

                // 抛出异常
                throw new Exception("API响应格式化失败