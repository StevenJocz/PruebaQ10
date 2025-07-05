using Domain.Prueba.DTOs;
using Domain.Prueba.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Prueba.Commands;
using Persistence.Prueba.Queries;
using System;

namespace PruebaQ10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : Controller
    {
        private readonly IEstudianteCommands _estudianteCommands;
        private readonly IEstudianteQueries _estudianteQueries;
        private readonly ILogger<EstudianteController> _logger;

        public EstudianteController(IEstudianteCommands estudianteCommands, IEstudianteQueries estudianteQueries, ILogger<EstudianteController> logger)
        {
            _estudianteCommands = estudianteCommands;
            _estudianteQueries = estudianteQueries;
            _logger = logger;
        }

        [HttpPost("PostRegistrar")]
        public async Task<IActionResult> registrarEstudiante(EstudianteDTOs estudiante)
        {
            try
            {
                _logger.LogInformation("Iniciando EstudianteController.registrarEstudiante...");
                var respuesta = await _estudianteCommands.registrarEstudiante(estudiante);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EstudianteController.registrarEstudiante...");
                throw;
            }
        }

        [HttpPut("PutEditar")]
        public async Task<IActionResult> ActualizarEstudiante(EstudianteDTOs estudiante)
        {
            {
                try
                {
                    _logger.LogInformation("Iniciando EstudianteController.ActualizarEstudiante...");
                    var respuesta = await _estudianteCommands.ActualizarEstudiante(estudiante);
                    return Ok(respuesta);
                }
                catch (Exception)
                {
                    _logger.LogError("Error al iniciar EstudianteController.ActualizarEstudiante...");
                    throw;
                }
            }
        }


        [HttpGet("GetListarEstudiantes")]
        public async Task<IActionResult> ListarEstudiantes()
        {
            _logger.LogInformation("Iniciando EstudianteController.ListarEstudiantes...");
            try
            {
                var respuesta = await _estudianteQueries.ListarEstudiantes();
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EstudianteController.ListarEstudiantes");
                throw;
            }
        }


        [HttpGet("GetListarEstudianteID")]
        public async Task<IActionResult> ListarEstudianteId(int id)
        {
            _logger.LogInformation("Iniciando EstudianteController.ListarEstudianteId...");
            try
            {
                var respuesta = await _estudianteQueries.ListarEstudianteId(id);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar EstudianteController.ListarEstudianteId");
                throw;
            }
        }
    }
}
