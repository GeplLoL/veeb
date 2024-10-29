using Microsoft.AspNetCore.Mvc;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParcelMachineController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ParcelMachineController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Omniva pakiautomaatide andmete saamine
        [HttpGet("omniva")]
        public async Task<IActionResult> GetOmnivaParcelMachines()
        {
            var response = await _httpClient.GetAsync("https://www.omniva.ee/locations.json");
            var responseBody = await response.Content.ReadAsStringAsync();
            return Content(responseBody, "application/json");
        }

        // SmartPost pakiautomaatide andmete saamine
        [HttpGet("smartpost")]
        public async Task<IActionResult> GetSmartPostParcelMachines()
        {
            var response = await _httpClient.GetAsync("https://www.smartpost.ee/places.json");
            var responseBody = await response.Content.ReadAsStringAsync();
            return Content(responseBody, "application/json");
        }
    }
}
