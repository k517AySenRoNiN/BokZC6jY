// 代码生成时间: 2025-09-11 15:02:31
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// DocumentConverterController is a controller that handles document format conversion.
[ApiController]
[Route("api/[controller]/[action]")]
public class DocumentConverterController : ControllerBase
{
    // Supported file extensions and their corresponding MIME types.
    private static readonly Dictionary<string, string> FileTypeMap = new Dictionary<string, string>
    {
        { ".txt", "text/plain" },
        { ".pdf", "application/pdf" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }
    };

    // ConvertDocument action method to handle document conversion.
    [HttpPost]
    public IActionResult ConvertDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            // Return an error message if the file is null or empty.
            return BadRequest("No file uploaded.");
        }

        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!FileTypeMap.ContainsKey(fileExtension))
        {
            // Return an error message if the file type is not supported.
            return BadRequest("Unsupported file type.");
        }

        string mimeType = FileTypeMap[fileExtension];
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
        string targetExtension = ".txt"; // Default target format is text.
        string targetFileName = $