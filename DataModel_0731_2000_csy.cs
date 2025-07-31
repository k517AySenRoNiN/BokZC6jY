// 代码生成时间: 2025-07-31 20:00:16
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// DataModel.cs
// 此文件包含数据模型的定义，用于表示数据库中的实体

// 定义一个通用的数据模型基类
public abstract class BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    // 构造函数
    protected BaseModel() { }
}

// 定义用户模型
public class User : BaseModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // 用户构造函数
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    // 用户验证方法
    public bool IsValid()
    {
        // 这里可以添加验证逻辑，例如检查密码强度等
        return true;
    }
}

// 定义产品模型
public class Product : BaseModel
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Range(0, 10000)]
    public decimal Price { get; set; }

    // 产品构造函数
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
