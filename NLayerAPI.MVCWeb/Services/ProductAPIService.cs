using NLayerAPI.Core.DTOs;

namespace NLayerAPI.MVCWeb.Services
{
    public class ProductAPIService
    {

        private readonly HttpClient _httpClient;

        public ProductAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDTO>> GetProductsWithCategoryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<ProductWithCategoryDTO>>>("products/GetProductsWithCategory");

            return response.Data;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<ProductDTO>>($"products/{id}");
            return response.Data;

        }

        public async Task<ProductDTO> SaveAsync(ProductDTO newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("products", newProduct);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductDTO>>();

            return responseBody.Data;

        }

        public async Task<bool> UpdateAsync(ProductDTO newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("products", newProduct);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");

            return response.IsSuccessStatusCode;
        }



    }
}
