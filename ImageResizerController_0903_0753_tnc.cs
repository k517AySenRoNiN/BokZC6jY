// 代码生成时间: 2025-09-03 07:53:52
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageResizer.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]"])
    public class ImageResizerController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageResizerController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult ResizeImages([FromForm] List<IFormFile> files, int newWidth, int newHeight)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }

            if (newWidth <= 0 || newHeight <= 0)
            {
                return BadRequest("Invalid dimensions.");
            }

            var outputPath = Path.Combine(_hostingEnvironment.WebRootPath, "resize_output");
            Directory.CreateDirectory(outputPath);

            foreach (var file in files)
            {
                try
                {
                    if (file.Length > 0)
                    {
                        using (var inputStream = file.OpenReadStream())
                        using (var bitmap = new Bitmap(inputStream))
                        {
                            using (var resizeStream = new MemoryStream())
                            {
                                var newImage = new Bitmap(bitmap, new Size(newWidth, newHeight));
                                newImage.Save(resizeStream, ImageFormat.Jpeg);

                                var filePath = Path.Combine(outputPath, Path.GetFileName(file.FileName));
                                File.WriteAllBytes(filePath, resizeStream.ToArray());

                                // Return the path to the resized image
                                return Ok(new {
                                    Message = "Image resized successfully.",
                                    Path = filePath
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and return an error response
                    return StatusCode(500, $"Error resizing image: {ex.Message}");
                }
            }

            return Ok("All images have been resized successfully.");
        }
    }
}