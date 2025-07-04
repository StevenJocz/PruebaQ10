using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Prueba.Commands;
using Persistence.Prueba.Queries;

namespace PruebaQ10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
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
    }
}
