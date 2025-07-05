using Domain.Prueba.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public List<EstudianteDTOs> EstudiantesD { get; set; } = new();

        [BindProperty]
        public EstudianteDTOs EstudianteSeleccionado { get; set; } = new();

        [BindProperty]
        public InscripcionDTOs InscripcionNueva { get; set; } = new();

        public List<MateriaDTOs> MateriasDisponibles { get; set; } = new();

        public List<MateriaDTOs> MateriasInscritas { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            var baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl!)

            };
        }

        public async Task OnGetAsync(int? id = null, bool inscribir = false, bool editar = false)
        {
            //Obtener lista de estudiantes
            var response = await _httpClient.GetAsync("api/Estudiante/GetListarEstudiantes");
            if (response.IsSuccessStatusCode)
            {
                EstudiantesD = await response.Content.ReadFromJsonAsync<List<EstudianteDTOs>>() ?? new();
            }

            //  Obtener lista de materias
            var materiasResponse = await _httpClient.GetAsync("api/Materia/GetListarMaterias");
            if (materiasResponse.IsSuccessStatusCode)
            {
                MateriasDisponibles = await materiasResponse.Content.ReadFromJsonAsync<List<MateriaDTOs>>() ?? new();
            }

            //  Si hay ID, cargar estudiante e inscripciones
            if (id.HasValue)
            {
                var estResponse = await _httpClient.GetAsync($"api/Estudiante/GetListarEstudianteID?id={id.Value}");
                if (estResponse.IsSuccessStatusCode)
                {
                    EstudianteSeleccionado = await estResponse.Content.ReadFromJsonAsync<EstudianteDTOs>() ?? new();
                }

                var inscripcionesResponse = await _httpClient.GetAsync($"api/Inscripcion/GetListarInscripcionesXEstudiantes?IdEstudiante={id.Value}");
                if (inscripcionesResponse.IsSuccessStatusCode)
                {
                    MateriasInscritas = await inscripcionesResponse.Content.ReadFromJsonAsync<List<MateriaDTOs>>() ?? new();
                }
            }

            //  Abrir modal correspondiente
            if (inscribir)
            {
                ViewData["AbrirModalInscripcion"] = true;
            }
            else if (editar)
            {
                ViewData["AbrirModalEditar"] = true;
            }
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (EstudianteSeleccionado.Id > 0)
            {
                var response = await _httpClient.PutAsJsonAsync("api/Estudiante/PutEditar", EstudianteSeleccionado);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error actualizando estudiante");
                }
            }
            else
            {
                var response = await _httpClient.PostAsJsonAsync("api/Estudiante/PostRegistrar", EstudianteSeleccionado);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error registrando estudiante");
                }
            }

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostInscribirAsync()
        {
            _logger.LogInformation("==> Entrando a OnPostInscribirAsync");

            // Cargar lista de materias
            var materiasResponse = await _httpClient.GetAsync("api/Materia/GetListarMaterias");
            if (materiasResponse.IsSuccessStatusCode)
            {
                MateriasDisponibles = await materiasResponse.Content.ReadFromJsonAsync<List<MateriaDTOs>>() ?? new();
            }

            // Cargar estudiante
            var estudianteResponse = await _httpClient.GetAsync($"api/Estudiante/GetListarEstudianteID?id={InscripcionNueva.id_Estudiante}");
            if (estudianteResponse.IsSuccessStatusCode)
            {
                EstudianteSeleccionado = await estudianteResponse.Content.ReadFromJsonAsync<EstudianteDTOs>() ?? new();
            }

            // Cargar inscripciones del estudiante
            var inscripcionesResponse = await _httpClient.GetAsync(
                $"api/Inscripcion/GetListarInscripcionesXEstudiantes?IdEstudiante={InscripcionNueva.id_Estudiante}");

            var materiasActuales = new List<MateriaDTOs>();
            if (inscripcionesResponse.IsSuccessStatusCode)
                materiasActuales = await inscripcionesResponse.Content.ReadFromJsonAsync<List<MateriaDTOs>>() ?? new();

            MateriasInscritas = materiasActuales;

            var materiaNueva = MateriasDisponibles.FirstOrDefault(m => m.id == InscripcionNueva.id_materia);

            // Validación: materia no existe
            if (materiaNueva == null)
            {
                TempData["ErrorInscripcion"] = "La materia seleccionada no es válida.";
                ViewData["AbrirModalInscripcion"] = true;
                await CargarEstudiantes();
                return Page();
            }

            // Validación: materia ya inscrita
            if (materiasActuales.Any(m => m.id == materiaNueva.id))
            {
                TempData["ErrorInscripcion"] = "El estudiante ya está inscrito en esta materia.";
                ViewData["AbrirModalInscripcion"] = true;
                await CargarEstudiantes();
                return Page();
            }

            // Validación: no más de 3 materias pesadas
            int pesadasActuales = materiasActuales.Count(m => m.creditos > 4);
            bool nuevaEsPesada = materiaNueva.creditos > 4;

            if (nuevaEsPesada && pesadasActuales >= 3)
            {
                TempData["ErrorInscripcion"] = "No puedes inscribir más de 3 materias con más de 4 créditos.";
                ViewData["AbrirModalInscripcion"] = true;
                await CargarEstudiantes();
                return Page();
            }

            // Intentar registrar
            var response = await _httpClient.PostAsJsonAsync("api/Inscripcion/PostRegistrar", InscripcionNueva);

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorInscripcion"] = "Error al registrar la inscripción.";
                ViewData["AbrirModalInscripcion"] = true;
                await CargarEstudiantes();
                return Page();
            }

            return RedirectToPage("/Index", new { id = InscripcionNueva.id_Estudiante });
        }


        private async Task CargarEstudiantes()
        {
            var estudiantesResponse = await _httpClient.GetAsync("api/Estudiante/GetListarEstudiantes");
            if (estudiantesResponse.IsSuccessStatusCode)
            {
                EstudiantesD = await estudiantesResponse.Content.ReadFromJsonAsync<List<EstudianteDTOs>>() ?? new();
            }
        }
    }
}