using Application.Dtos.Response;
using Application.Services.Interface;
using Azure.Storage.Blobs;
using Domain.Enums;
using Domain.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class BlobStorageService : IUploadService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public BlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public async Task<ResultService<string>> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                ResultService.Fail("File is empty!");

            var validateTypeMedia = GetTypeMedia(file.FileName);
            return ResultService.Ok<string>(validateTypeMedia == Media.Image ? await UploadImageAsync(file) : await UploadVideoAsync(file));
        }
    

        public string Delete(string fileName)
        {
            throw new NotImplementedException();
        }


        public Media GetTypeMedia(string filename)
        {
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".webp" };

            string[] videoExtensions = {
                ".avi", ".mp4" };

            var fileInfo = new FileInfo(filename);

            return imageExtensions.Contains(fileInfo.Extension) ? Media.Image : videoExtensions.Contains(fileInfo.Extension) ? Media.Video : throw new DomainValidationException("Wrong format file!"); ;
        }

        private async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                var containerName = _configuration.GetSection("AzureStorage:ImageContainerName").Value;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                // Upload File
                using (Stream stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                //Return File URL
                return (blobClient.Uri.ToString());
 
            }
            catch
            {
                throw new ApplicationException("Failed Upload Image to blob Storage");
            }
        }

        private async Task<string> UploadVideoAsync(IFormFile file)
        {
            try
            {
                var containerName = _configuration.GetSection("AzureStorage:VideoContainerName").Value;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                // Upload File
                using (Stream stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                //Return File URL
                return (blobClient.Uri.ToString());

            }
            catch
            {
                throw new ApplicationException("Failed Upload Video to blob Storage");
            }

        }
    }
}
