using Domain.Prueba.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Prueba.Commands;
using Persistence.Prueba.Queries;

namespace PruebaQ10.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaCommands _materiaCommands;
        private readonly IMateriaQueries _materiaQueries;
        private readonly ILogger<MateriaController> _logger;

        public MateriaController(IMateriaCommands materiaCommands, IMateriaQueries materiaQueries, ILogger<MateriaController> logger)
        {
            _materiaCommands = materiaCommands;
            _materiaQueries = materiaQueries;
            _logger = logger;
        }

        [HttpPost("materias")]
        public async Task<IActionResult> registrarMateria([FromBody] MateriaDTOs materiaDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando MateriaController.registrarMateria...");
                var respuesta = await _materiaCommands.registrarMateria(materiaDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar MateriaController.registrarMateria...");
                throw;
            }
        }


        [HttpGet("materias")]
        public async Task<IActionResult> ListarMaterias()
        {
            _logger.LogInformation("Iniciando MateriaController.ListarMaterias...");
            try
            {
                var respuesta = await _materiaQueries.ListarMaterias();
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar MateriaController.ListarMaterias");
                throw;
            }
        }


        [HttpGet("materias/{1}")]
        public async Task<IActionResult> ListarMateriaId(int id)
        {
            _logger.LogInformation("Iniciando MateriaController.ListarMateriaId...");
            try
            {
                var respuesta = await _materiaQueries.ListarMateriaId(id);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar MateriaController.ListarMateriaId");
                throw;
            }
        }

        [HttpPut("materias/{1}")]
        public async Task<IActionResult> ActualizarMateria(int id, [FromBody] MateriaDTOs materiaDTOs)
        {
            {
                try
                {
                    _logger.LogInformation("Iniciando MateriaController.ActualizarMateria...");
                    var respuesta = await _materiaCommands.ActualizarMateria(materiaDTOs);
                    return Ok(respuesta);
                }
                catch (Exception)
                {
                    _logger.LogError("Error al iniciar MateriaController.ActualizarMateria...");
                    throw;
                }
            }
        }

    }
}
