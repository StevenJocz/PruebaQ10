using Domain.Prueba.DTOs;
using Infrastructure.Prueba;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Prueba.Commands
{
    public interface IInscripcionCommands
    {
        Task<RespuestasDTO> registrarInscripcion(InscripcionDTOs inscripcionDTOs);
    }
    public class InscripcionCommands: IInscripcionCommands, IDisposable
    {
        private readonly Context _context = null;
        private readonly ILogger<IInscripcionCommands> _logger;
        private readonly IConfiguration _configuration;

        public InscripcionCommands(ILogger<InscripcionCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("DataBase");
            _context = new Context(connectionString);
        }

        #region implementacion Disponse
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion


        public async Task<RespuestasDTO> registrarInscripcion(InscripcionDTOs inscripcionDTOs)
        {
            _logger.LogTrace("Iniciando metodo InscripcionCommands.registrarInscripcion...");

            try
            {
                var nuevaInscripcion = InscripcionDTOs.CrearE(inscripcionDTOs);

                await _context.InscripcionEs.AddAsync(nuevaInscripcion);
                await _context.SaveChangesAsync();

                if (nuevaInscripcion.id_Inscripcion != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        mensaje = "¡Inscripción registrada correctamente!"
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        mensaje = "¡No se logró registrar la inscripción!"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en el método InscripcionCommands.registrarInscripcion: ");
                throw;
            }
        }


    }
}
