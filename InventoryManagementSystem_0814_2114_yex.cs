// 代码生成时间: 2025-08-14 21:14:46
using System;
using System.Collections.Generic;
using System.Linq;

// 库存管理系统
public class InventoryManagementSystem
{
    // 库存项类
    private class InventoryItem
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public InventoryItem(string itemId, string itemName, int quantity)
        {
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;
        }
    }

    // 存储库存项的列表
    private List<InventoryItem> inventoryList = new List<InventoryItem>();

    // 添加库存项
    public void AddItem(string itemId, string itemName, int quantity)
    {
        if (string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(itemName) || quantity <= 0)
        {
            throw new ArgumentException("Invalid item details provided.");
        }

        inventoryList.Add(new InventoryItem(itemId, itemName, quantity));
    }

    // 更新库存项数量
    public void UpdateItemQuantity(string itemId, int newQuantity)
    {
        if (string.IsNullOrEmpty(itemId) || newQuantity <= 0)
        {
            throw new ArgumentException("Invalid item details provided.");
        }

        var item = inventoryList.FirstOrDefault(i => i.ItemId == itemId);
        if (item == null)
        {
            throw new KeyNotFoundException("There is no item with the specified ID.");
        }

        item.Quantity = newQuantity;
    }

    // 获取库存项信息
    public string GetItemInfo(string itemId)
    {
        if (string.IsNullOrEmpty(itemId))
        {
            throw new ArgumentException("Item ID cannot be null or empty.");
        }

        var item = inventoryList.FirstOrDefault(i => i.ItemId == itemId);
        if (item == null)
        {
            return "Item not found.";
        }

        return $"Item ID: {item.ItemId}, Item Name: {item.ItemName}, Quantity: {item.Quantity}";
    }

    // 删除库存项
    public void DeleteItem(string itemId)
    {
        if (string.IsNullOrEmpty(itemId))
        {
            throw new ArgumentException("Item ID cannot be null or empty.");
        }

        var itemToRemove = inventoryList.FirstOrDefault(i => i.ItemId == itemId);
        if (itemToRemove == null)
        {
            throw new KeyNotFoundException("There is no item with the specified ID.");
        }

        inventoryList.Remove(itemToRemove);
    }
}
