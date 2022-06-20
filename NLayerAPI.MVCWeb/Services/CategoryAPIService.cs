using NLayerAPI.Core.DTOs;

namespace NLayerAPI.MVCWeb.Services
{
    public class CategoryAPIService
    {

        private readonly HttpClient _httpClient;

        public CategoryAPIService(HttpClient httpClient)
        {


            _httpClient = httpClient;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<CategoryDTO>>>("category");
            return response.Data;
        }

    }
}
