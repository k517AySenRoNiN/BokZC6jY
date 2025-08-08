// 代码生成时间: 2025-08-09 00:01:52
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageResizerApp
{
    /// <summary>
    /// 图片尺寸批量调整器
    /// </summary>
    public class ImageResizer
    {
        private string sourceFolderPath;
        private string destinationFolderPath;
        private int targetWidth;
        private int targetHeight;

        /// <summary>
        /// 初始化图片尺寸批量调整器
        /// </summary>
        /// <param name="sourceFolderPath">源文件夹路径</param>
        /// <param name="destinationFolderPath">目标文件夹路径</param>
        /// <param name="targetWidth">目标宽度</param>
        /// <param name="targetHeight">目标高度</param>
        public ImageResizer(string sourceFolderPath, string destinationFolderPath, int targetWidth, int targetHeight)
        {
            this.sourceFolderPath = sourceFolderPath;
            this.destinationFolderPath = destinationFolderPath;
            this.targetWidth = targetWidth;
            this.targetHeight = targetHeight;
        }

        /// <summary>
        /// 批量调整图片尺寸
        /// </summary>
        public void ResizeImages()
        {
            try
            {
                // 获取源文件夹中的所有图片文件
                string[] files = Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    ResizeImage(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生错误: " + ex.Message);
            }
        }

        /// <summary>
        /// 调整单个图片的尺寸
        /// </summary>
        /// <param name="filePath">图片文件路径</param>
        private void ResizeImage(string filePath)
        {
            using (Image originalImage = Image.FromFile(filePath))
            {
                // 创建新的Bitmap对象
                using (Bitmap resizedImage = new Bitmap(targetWidth, targetHeight))
                {
                    using (Graphics graphics = Graphics.FromImage(resizedImage))
                    {
                        // 设置高质量插值法
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        // 设置高质量输出
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        // 清除非透明像素
                        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                        // 绘制调整大小后的图像
                        graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);

                        // 获取源文件的名称和扩展名
                        string fileName = Path.GetFileName(filePath);
                        string fileExtension = Path.GetExtension(filePath);

                        // 创建目标文件路径
                        string destinationFile = Path.Combine(destinationFolderPath, fileName);

                        // 保存调整大小的图片
                        resizedImage.Save(destinationFile, ImageFormat.Jpeg);
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string sourceFolderPath = @"C:\SourceImages";
            string destinationFolderPath = @"C:\ResizedImages";
            int targetWidth = 800;
            int targetHeight = 600;

            ImageResizer imageResizer = new ImageResizer(sourceFolderPath, destinationFolderPath, targetWidth, targetHeight);
            imageResizer.ResizeImages();

            Console.WriteLine("图片尺寸批量调整完成！");
        }
    }
}
