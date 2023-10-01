using Microsoft.AspNetCore.Http;

namespace Application.Services.Interface
{
    public interface IUploadService
    {
        Task<ResultService<string>> UploadFileAsync(IFormFile file);
        public string Delete(string fileName);
    }
}
