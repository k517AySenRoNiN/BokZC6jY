// 代码生成时间: 2025-08-21 14:57:13
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

// 此控制器用于处理交互式图表生成器的请求
[ApiController]
[Route("api/interactive-chart")]
public class InteractiveChartGeneratorController : ControllerBase
{
    // POST: api/interactive-chart/generate
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateChart([FromBody] ChartRequest chartRequest)
    {
        try
        {
            if (chartRequest == null || chartRequest.Data == null || chartRequest.Type == null)
            {
                return BadRequest("Missing chart configuration");
            }

            // 根据请求类型生成图表
            Chart chart = await GenerateChartAsync(chartRequest);

            // 序列化图表数据为JSON格式
            string chartJson = JsonConvert.SerializeObject(chart, Formatting.Indented);

            return Ok(chartJson);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // 模拟异步图表生成方法
    private async Task<Chart> GenerateChartAsync(ChartRequest chartRequest)
    {
        // 这里是图表生成的逻辑，实际应用中可能需要调用图表库或服务
        // 例如，根据chartRequest参数来生成不同类型的图表
        await Task.Delay(100); // 模拟异步操作

        // 假设生成图表成功
        Chart chart = new Chart
        {
            Title = chartRequest.Title,
            Data = chartRequest.Data,
            Type = chartRequest.Type
        };
        return chart;
    }
}

// 图表请求模型
public class ChartRequest
{
    public string Title { get; set; }
    public List<ChartData> Data { get; set; }
    public string Type { get; set; }
}

// 图表数据模型
public class ChartData
{
    public string Label { get; set; }
    public double Value { get; set; }
}

// 图表模型
public class Chart
{
    public string Title { get; set; }
    public List<ChartData> Data { get; set; }
    public string Type { get; set; }
}
