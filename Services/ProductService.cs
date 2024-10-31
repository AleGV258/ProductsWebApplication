using IGRTEC_WEBApplication_MichellAlejandroGarciaVargas.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IGRTEC_WEBApplication_MichellAlejandroGarciaVargas.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:ProductApiUrl"];
        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ProductModel>>(json);
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductModel>(json);
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            using (var content = new MultipartFormDataContent())
            {
                using (var stream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(stream);
                    content.Add(new ByteArrayContent(stream.ToArray(), 0, (int)stream.Length), "file", imageFile.FileName);
                }
                var response = await _httpClient.PostAsync($"https://api.escuelajs.co/api/v1/files/upload", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var uploadedImageResponse = JsonSerializer.Deserialize<UploadedImageResponse>(responseBody);
                return uploadedImageResponse.location;
            }
        }

        public async Task CreateProductAsync(ProductModel product)
        {
            var productData = new
            {
                product.title,
                product.price,
                product.description,
                images = product.images,
                categoryId = product.category.id,
            };
            var jsonContent = JsonSerializer.Serialize(productData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            var productData = new
            {
                product.id,
                product.title,
                product.price,
            };
            var jsonContent = JsonSerializer.Serialize(productData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{product.id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
