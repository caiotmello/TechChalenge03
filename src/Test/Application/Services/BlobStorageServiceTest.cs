using Application.Services;
using Azure.Storage.Blobs;
using Domain.Enums;
using Domain.Validations;
using Microsoft.Extensions.Configuration;


namespace Test.Application.Services
{
    public class BlobStorageServiceTest
    {
        private const string _connectionString = "DefaultEndpointsProtocol=https;AccountName=caiotascheti;AccountKey=BqTAn6A76ptGrXQ+nJ2m9z8Xv5qBLeVBExTrx1XtSK3D+jpgi9QSqAn3XCQklYGhGIV4E2fwIua++AStKNG/Ew==;EndpointSuffix=core.windows.net";
        private IConfiguration _configuration;

        public BlobStorageServiceTest()
        {
            var configValues = new Dictionary<string, string>
            {
                { "AzureStorage:ConnectionString", _connectionString }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();
        }

        [Theory(DisplayName = "Validate file extension for upload")]
        [Trait("Categoria","Upload Validation")]
        [InlineData(Media.Image, "image.jpeg")]
        [InlineData(Media.Image, "Image.png")]
        [InlineData(Media.Video,"video.avi")]
        [InlineData(Media.Video, "video.mp4")]
        public void GetTypeMediaValidation_ShouldVerifyIfIsImageOrVideo(Media media, string filename)
        {
            var blobClient = new BlobServiceClient(_configuration.GetSection("AzureStorage:ConnectionString").Value);
            var service = new BlobStorageService(blobClient, _configuration);

            var result = service.GetTypeMedia(filename);

            Assert.Equal(media, result);
        }

        [Theory(DisplayName = "Validate file extension for upload")]
        [Trait("Categoria", "Upload Validation")]
        [InlineData(Media.Image, "image.gif")]
        [InlineData(Media.Video, "video.mkr")]
        public void GetTypeMediaValidation_ShouldThrowException_WhenIsNotImageOrVideo(Media media, string filename)
        {

            var blobClient = new BlobServiceClient(_configuration.GetSection("AzureStorage:ConnectionString").Value);
            var service = new BlobStorageService(blobClient, _configuration);

            var result = Assert.Throws<DomainValidationException>(() =>
                service.GetTypeMedia(filename));
            
            Assert.Equal("Wrong format file!", result.Message);
        }
    }
}
