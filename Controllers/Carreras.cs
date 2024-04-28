using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITLCarreras.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarrerasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetDatosFromFirebase()
        {
            // Código para obtener datos de Firebase (GET)
            var httpClient = _httpClientFactory.CreateClient();
            var firebaseUrl = "https://itldb-1d768-default-rtdb.firebaseio.com/ITL.json";

            HttpResponseMessage response = await httpClient.GetAsync(firebaseUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Content(json); // Devuelve el JSON directamente como respuesta
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDatoToFirebase([FromBody] string newData)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var firebaseUrl = "https://itldb-1d768-default-rtdb.firebaseio.com/ITL.json";

            HttpResponseMessage response = await httpClient.PostAsync(firebaseUrl, new StringContent(newData, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return Ok("Dato agregado correctamente");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatoFromFirebase(string id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var firebaseUrl = $"https://itldb-1d768-default-rtdb.firebaseio.com/ITL/{id}.json";

            HttpResponseMessage response = await httpClient.DeleteAsync(firebaseUrl);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Dato eliminado correctamente");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
