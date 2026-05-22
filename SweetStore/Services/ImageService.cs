using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;


namespace SweetStore.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        private static readonly string[] AllowedExtensions =
        {
            ".jpg", ".jpeg", ".png", ".webp"
        };

        private const long MaxFileSize = 5 * 1024 * 1024;
        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                throw new ArgumentException("Invalid file extension.");

            if (file.Length > MaxFileSize)
                throw new Exception("Too large");

            var folderPath = Path.Combine(_env.WebRootPath, "images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(folderPath, fileName);

            await using(var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/images/" + fileName;
        }

        public void DeleteImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            if (!imagePath.StartsWith("/images/"))
                return;
            var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
