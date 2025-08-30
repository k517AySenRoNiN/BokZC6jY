// 代码生成时间: 2025-08-30 15:51:32
using System;
using System.Collections.Generic;
using System.Linq;

// ShoppingCartService 负责管理购物车相关的业务逻辑
public class ShoppingCartService
{
    // 购物车项类
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    // 购物车类，存储所有购物车项
    public class ShoppingCart
    {
        private List<CartItem> items;

        public ShoppingCart()
        {
            items = new List<CartItem>();
        }

        // 添加商品到购物车
        public void AddItem(CartItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var existingItem = items.FirstOrDefault(it => it.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                items.Add(item);
            }
        }

        // 从购物车移除商品
        public void RemoveItem(int productId)
        {
            var item = items.FirstOrDefault(it => it.ProductId == productId);
            if (item != null)
            {
                items.Remove(item);
            }
            else
            {
                throw new InvalidOperationException($"Item with product ID {productId} not found in the cart.");
            }
        }

        // 获取购物车中所有商品的总价格
        public decimal GetTotalPrice()
        {
            return items.Sum(item => item.Price * item.Quantity);
        }

        // 获取购物车中的商品列表
        public List<CartItem> GetItems()
        {
            return items;
        }
    }

    // 实例化购物车，用于演示
    public ShoppingCart ShoppingCartInstance { get; private set; } = new ShoppingCart();

    // 添加商品到购物车
    public void AddProductToCart(int productId, string productName, decimal price, int quantity)
    {
        var item = new ShoppingCart.CartItem
        {
            ProductId = productId,
            ProductName = productName,
            Price = price,
            Quantity = quantity
        };

        try
        {
            ShoppingCartInstance.AddItem(item);
        }
        catch (Exception ex)
        {
            // 处理异常，例如记录日志等
            Console.WriteLine($"Error adding product to cart: {ex.Message}");
        }
    }

    // 从购物车移除商品
    public void RemoveProductFromCart(int productId)
    {
        try
        {
            ShoppingCartInstance.RemoveItem(productId);
        }
        catch (Exception ex)
        {
            // 处理异常，例如记录日志等
            Console.WriteLine($"Error removing product from cart: {ex.Message}");
        }
    }

    // 获取购物车总价格
    public decimal GetCartTotalPrice()
    {
        return ShoppingCartInstance.GetTotalPrice();
    }

    // 获取购物车中的商品列表
    public List<ShoppingCart.CartItem> GetCartItems()
    {
        return ShoppingCartInstance.GetItems();
    }
}
