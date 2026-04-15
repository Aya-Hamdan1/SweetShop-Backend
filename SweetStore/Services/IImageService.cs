namespace SweetStore.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile file);
        void DeleteImage(string imagePath);
    }
}
