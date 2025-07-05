using Domain.Prueba.DTOs;
using Domain.Prueba.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class IMateriasModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public List<MateriaDTOs> MateriasD { get; set; } = new();

        [BindProperty]
        public MateriaDTOs MateriaSeleccionada { get; set; } = new();

       

        public IMateriasModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            var baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl!)
            };
        }

        public async Task OnGetAsync(int? id = null)
        {
            var response = await _httpClient.GetAsync("api/Materia/GetListarMaterias");
            if (response.IsSuccessStatusCode)
            {
                var materias = await response.Content.ReadFromJsonAsync<List<MateriaDTOs>>();
                MateriasD = materias ?? new List<MateriaDTOs>();
            }

            

            if (id.HasValue)
            {
                var estResponse = await _httpClient.GetAsync($"api/Materia/GetListarMateriaID?id={id.Value}");
                if (estResponse.IsSuccessStatusCode)
                {
                    var materia = await estResponse.Content.ReadFromJsonAsync<MateriaDTOs>();
                    MateriaSeleccionada = materia ?? new MateriaDTOs();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (MateriaSeleccionada.id > 0)
            {
                // PUT - actualizar materia
                var response = await _httpClient.PutAsJsonAsync("api/Materia/PutEditar", MateriaSeleccionada);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error al actualizar la materia");
                }
            }
            else
            {
                // POST - registrar materia
                var response = await _httpClient.PostAsJsonAsync("api/Materia/PostRegistrar", MateriaSeleccionada);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error registrar la materia");
                }
            }

            return RedirectToPage("/Materias");
        }

        
    }
}