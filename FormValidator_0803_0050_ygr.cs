// 代码生成时间: 2025-08-03 00:50:02
using System;
using System.ComponentModel.DataAnnotations;

// FormValidator 类用于验证表单数据
public class FormValidator
{
    // 验证表单数据的方法
    // T 表示表单的数据类型
    public bool ValidateForm<T>(T formData) where T : class, new()
    {
        // 验证数据是否为null
        if (formData == null)
        {
            throw new ArgumentNullException(nameof(formData), "Form data cannot be null.");
        }

        // 使用DataAnnotations验证器进行验证
        var validationContext = new ValidationContext(formData, serviceProvider: null, items: null)
        {
            DisplayName = typeof(T).Name
        };

        // 存储验证结果
        var results = new List<ValidationResult>();

        // 执行验证逻辑
        bool isValid = Validator.TryValidateObject(formData, validationContext, results, true);

        // 如果验证结果不为空，说明验证失败
        if (!isValid)
        {
            // 抛出验证异常，包含所有验证失败的错误信息
            throw new ValidationException($"Validation failed for {typeof(T).Name}.", results);
        }

        // 如果验证通过，则返回true
        return isValid;
    }
}

// 使用示例
// public class FormData
// {
//     [Required]
//     [StringLength(100, MinimumLength = 3)]
//     public string Name { get; set; }

//     [Required]
//     [EmailAddress]
//     public string Email { get; set; }

//     // 其他表单字段...
// // }

// try
// {
//     var formData = new FormData
//     {
//         Name = "John Doe",
//         Email = "john.doe@example.com"
//         // 设置其他字段...
//     };

//     var validator = new FormValidator();

//     if (validator.ValidateForm(formData))
//     {
//         // 表单验证通过，继续处理...
//     }
//     else
//     {
//         // 表单验证失败，处理错误...
//     }
// }
// catch (ValidationException ex)
// {
//     // 处理验证异常...
// }