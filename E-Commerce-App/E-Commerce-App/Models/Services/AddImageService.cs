using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using E_Commerce_App.Models.Interfaces;

namespace E_Commerce_App.Models.Services
{
    public class AddImageService : IAddImageToCloud
    {
        private readonly IConfiguration _configuration;

        public AddImageService(IConfiguration config)
        {
            _configuration = config;
        }
        public async Task<Product> UploadProductImage(IFormFile file, Product product)
        {
            BlobContainerClient blobContainerClient =
                new BlobContainerClient
                (_configuration.GetConnectionString("StorageAccount"), "productsimages");

            await blobContainerClient.CreateIfNotExistsAsync();

            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

            using var filestream = file.OpenReadStream();

            BlobUploadOptions blobUploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
            };

            if (!blobClient.Exists())
                await blobClient.UploadAsync(filestream, blobUploadOptions);

            product.ProductImage = blobClient.Uri.ToString();
            return product;
        }
    }
}
