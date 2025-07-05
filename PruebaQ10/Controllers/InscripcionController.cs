using Domain.Prueba.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Prueba.Commands;
using Persistence.Prueba.Queries;

namespace PruebaQ10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionCommands _inscripcionCommands;
        private readonly IInscripcionQueries _inscripcionQueries;
        private readonly ILogger<InscripcionController> _logger;

        public InscripcionController(IInscripcionCommands inscripcionCommands, IInscripcionQueries inscripcionQueries, ILogger<InscripcionController> logger)
        {
            _inscripcionCommands = inscripcionCommands;
            _inscripcionQueries = inscripcionQueries;
            _logger = logger;
        }

        [HttpPost("PostRegistrar")]
        public async Task<IActionResult> registrarInscripcion(InscripcionDTOs inscripcionDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando InscripcionController.registrarInscripcion...");
                var respuesta = await _inscripcionCommands.registrarInscripcion(inscripcionDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionController.registrarInscripcion...");
                throw;
            }
        }


        [HttpGet("GetListarInscripcionesXEstudiantes")]
        public async Task<IActionResult> ListarInscripcionesXEstudiantes(int IdEstudiante)
        {
            _logger.LogInformation("Iniciando InscripcionController.ListarInscripciones...");
            try
            {
                var respuesta = await _inscripcionQueries.ListarInscripcionesXEstudiantes(IdEstudiante);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar InscripcionController.ListarInscripciones");
                throw;
            }
        }

    }
}
