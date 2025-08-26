// 代码生成时间: 2025-08-26 23:12:54
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentFormatConverterApp
{
    // Controller responsible for handling document conversion requests
    [ApiController]
    [Route("[controller]/[action]"])
    public class DocumentFormatConverterController : ControllerBase
    {
        private readonly IDocumentConverter _documentConverter;

        // Constructor injection for IDocumentConverter
        public DocumentFormatConverterController(IDocumentConverter documentConverter)
        {
            _documentConverter = documentConverter;
        }

        // POST method to convert documents
        [HttpPost]
        public IActionResult ConvertDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                // Convert the uploaded file to the desired format
                byte[] convertedFile = _documentConverter.Convert(file);

                // Create a response with the converted file content
                return File(convertedFile, "application/octet-stream",
                    $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
            }
            catch (DocumentConversionException ex)
            {
                // Handle specific conversion errors
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during document conversion.");
            }
        }
    }

    // Interface for document conversion service
    public interface IDocumentConverter
    {
        byte[] Convert(IFormFile file);
    }

    // Implementation of the IDocumentConverter interface
    public class DocumentConverter : IDocumentConverter
    {
        public byte[] Convert(IFormFile file)
        {
            // Check if the file is a supported format
            if (!IsSupportedFormat(file.FileName))
            {
                throw new DocumentConversionException("Unsupported file format.");
            }

            // Implement the conversion logic here
            // For demonstration, we're just returning the original file content
            return File.ReadAllBytes(file.FilePath);
        }

        // Helper method to check if the file format is supported
        private bool IsSupportedFormat(string fileName)
        {
            var supportedFormats = new[] { ".docx", ".pdf", ".txt" };
            return supportedFormats.Any(format => fileName.EndsWith(format, StringComparison.OrdinalIgnoreCase));
        }
    }

    // Custom exception for document conversion errors
    public class DocumentConversionException : Exception
    {
        public DocumentConversionException(string message) : base(message)
        {
        }
    }
}
