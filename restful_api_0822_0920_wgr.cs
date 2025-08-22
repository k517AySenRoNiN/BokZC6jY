// 代码生成时间: 2025-08-22 09:20:44
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 定义一个模型类，用于数据传输
public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

// 创建一个控制器类，用于处理API请求
[ApiController]
[Route("[controller]s")]
public class ItemsController : ControllerBase
{
    private static readonly List<Item> items = new List<Item>
    {
        new Item { Id = 1, Name = "Item1", Description = "Description1" },
        new Item { Id = 2, Name = "Item2", Description = "Description2" }
    };

    // 获取所有项的API
    [HttpGet]
    public IActionResult GetItems()
    {
        try
        {
            return Ok(items);
        }
        catch (Exception ex)
        {
            // 错误处理，返回错误信息
            return StatusCode(500, ex.Message);
        }
    }

    // 获取单个项的API
    [HttpGet("{id}")]
    public IActionResult GetItem(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound("Item not found.");
        }
        return Ok(item);
    }

    // 创建新项的API
    [HttpPost]
    public IActionResult CreateItem(Item item)
    {
        if (item == null)
        {
            return BadRequest("Invalid item.");
        }
        if (items.Any(i => i.Id == item.Id))
        {
            return BadRequest("Item with the same id already exists.");
        }
        items.Add(item);
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    // 更新现有项的API
    [HttpPut("{id}")]
    public IActionResult UpdateItem(int id, Item item)
    {
        if (item == null || item.Id != id)
        {
            return BadRequest("Invalid item.");
        }
        var existingItem = items.FirstOrDefault(i => i.Id == id);
        if (existingItem == null)
        {
            return NotFound("Item not found.");
        }
        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        return Ok(existingItem);
    }

    // 删除项的API
    [HttpDelete("{id}")]
    public IActionResult DeleteItem(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound("Item not found.");
        }
        items.Remove(item);
        return NoContent();
    }
}