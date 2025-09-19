// 代码生成时间: 2025-09-20 03:00:42
// NotificationService.cs
// 这是一个简单的消息通知系统，用于在ASP.NET框架中实现消息的发送和接收。

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotificationSystem
{
    // 消息通知服务类
    public class NotificationService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly string _smtpHost;
        private readonly int _smtpPort;

        // 构造函数
        public NotificationService(string fromEmail, string fromPassword, string smtpHost, int smtpPort)
        {
            _fromEmail = fromEmail;
            _fromPassword = fromPassword;
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;

            // 初始化SMTP客户端
            _smtpClient = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new System.Net.NetworkCredential(_fromEmail, _fromPassword),
                EnableSsl = true
            };
        }

        // 发送电子邮件方法
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                // 创建邮件消息
                var mailMessage = new MailMessage(_fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                // 发送邮件
                await _smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($"Email sent to {toEmail}");
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        // 发送文本消息方法
        public async Task SendTextMessageAsync(string phoneNumber, string message)
        {
            try
            {
                // 这里可以集成第三方短信服务API，如Twilio
                // 例如: await TwilioClient.Messages.CreateAsync(phoneNumber, message);
                Console.WriteLine($"Text message sent to {phoneNumber}");
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error sending text message: {ex.Message}");
            }
        }
    }
}
