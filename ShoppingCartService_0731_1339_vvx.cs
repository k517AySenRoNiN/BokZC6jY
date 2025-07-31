// 代码生成时间: 2025-07-31 13:39:49
using System;
using System.Collections.Generic;
using System.Linq;

// ShoppingCartService.cs
// This class represents the shopping cart service which manages the shopping cart operations.
public class ShoppingCartService
{
    private readonly Dictionary<int, CartItem> _cartItems = new Dictionary<int, CartItem>();

    // Adds an item to the shopping cart.
    // If the item already exists in the cart, its quantity is increased.
    public void AddToCart(int itemId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        if (_cartItems.ContainsKey(itemId))
        {
            // Increase the quantity of the existing item.
            _cartItems[itemId].Quantity += quantity;
        }
        else
        {
            // Add a new item to the cart.
            _cartItems.Add(itemId, new CartItem(itemId, quantity));
        }
    }

    // Removes an item from the shopping cart.
    // If the quantity is greater than 0, it decreases the quantity.
    public void RemoveFromCart(int itemId, int quantity)
    {
        if (!_cartItems.ContainsKey(itemId))
        {
            throw new KeyNotFoundException("Item not found in the cart.");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        if (_cartItems[itemId].Quantity <= quantity)
        {
            // Remove the item completely from the cart.
            _cartItems.Remove(itemId);
        }
        else
        {
            // Decrease the quantity of the item.
            _cartItems[itemId].Quantity -= quantity;
        }
    }

    // Retrieves the shopping cart items.
    public List<CartItem> GetCartItems()
    {
        return _cartItems.Values.ToList();
    }

    // Represents an item in the cart, including its identifier and quantity.
    public class CartItem
    {
        public int ItemId { get; }
        public int Quantity { get; set; }

        public CartItem(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
