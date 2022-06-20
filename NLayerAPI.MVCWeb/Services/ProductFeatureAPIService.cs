using NLayerAPI.Core.DTOs;

namespace NLayerAPI.MVCWeb.Services
{
    public class ProductFeatureAPIService
    {

        private readonly HttpClient _httpClient;

        public ProductFeatureAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductFeatureWithProductDTO>> GetProductFeaturesWithProductAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<ProductFeatureWithProductDTO>>>("productFeature/GetProductFeaturesWithProduct");

            return response.Data;
        }

        public async Task<ProductFeatureDTO> GetByIdAsync(int id)
        {

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<ProductFeatureDTO>>($"productFeature/{id}");
            return response.Data;

        }

        public async Task<ProductFeatureDTO> SaveAsync(ProductFeatureDTO newProductFeature)
        {
            var response = await _httpClient.PostAsJsonAsync("productFeature", newProductFeature);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductFeatureDTO>>();

            return responseBody.Data;

        }

        public async Task<bool> UpdateAsync(ProductFeatureDTO newProductFeature)
        {
            var response = await _httpClient.PutAsJsonAsync("productFeature", newProductFeature);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"productFeature/{id}");

            return response.IsSuccessStatusCode;
        }


    }
}
