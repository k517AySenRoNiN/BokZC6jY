// 代码生成时间: 2025-09-22 14:57:22
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

// InventoryManagementSystem.cs
// 该类提供库存管理系统的基本功能
public class InventoryManagementSystem
{
    // 存储库存项的列表
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    // 添加库存项
    public void AddInventoryItem(InventoryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        inventoryItems.Add(item);
    }

    // 删除库存项
    public bool RemoveInventoryItem(int id)
    {
        var item = inventoryItems.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            inventoryItems.Remove(item);
            return true;
        }
        return false;
    }

    // 更新库存项
    public bool UpdateInventoryItem(InventoryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existingItem = inventoryItems.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem != null)
        {
            existingItem.Name = item.Name;
            existingItem.Quantity = item.Quantity;
            return true;
        }
        return false;
    }

    // 获取库存项列表
    public List<InventoryItem> GetInventoryItems()
    {
        return inventoryItems.ToList();
    }

    // 获取单个库存项
    public InventoryItem GetInventoryItemById(int id)
    {
        return inventoryItems.FirstOrDefault(i => i.Id == id);
    }
}

// InventoryItem.cs
// 库存项类
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}
