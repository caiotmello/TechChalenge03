using Application.Services;
using Azure.Storage.Blobs;
using Domain.Enums;
using Domain.Validations;
using Microsoft.Extensions.Configuration;


namespace Test.Application.Services
{
    public class BlobStorageServiceTest
    {
        private const string _connectionString = "DefaultEndpointsProtocol=https;AccountName=techchallenge03;AccountKey=a9mvdkg7fGxoRrldYFaMvdWH/+L8mo03e2y/I/vXCNi2pPWYIc8u9KWCo8pSBYdNfQXvVrOKoaIz+AStsaqkSg==;EndpointSuffix=core.windows.net";
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

        [Theory(DisplayName = "[UnitTest] BlobStorageService - Validate file extension for upload")]
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

        [Theory(DisplayName = "[UnitTest] BlobStorageService - Validate file extension for upload")]
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
