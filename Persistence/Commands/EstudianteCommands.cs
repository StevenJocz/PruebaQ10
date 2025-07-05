using Domain.Prueba.DTOs;
using Infrastructure.Prueba;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Persistence.Prueba.Commands
{
    public interface IEstudianteCommands
    {
        Task<RespuestasDTO> registrarEstudiante(EstudianteDTOs estudianteDTOs);
        Task<RespuestasDTO> ActualizarEstudiante(EstudianteDTOs estudianteDTOs);
    }

    public class EstudianteCommands: IEstudianteCommands, IDisposable
    {
        private readonly Context _context = null;
        private readonly ILogger<IEstudianteCommands> _logger;
        private readonly IConfiguration _configuration;

        public EstudianteCommands(ILogger<EstudianteCommands> logger, IConfiguration configuration)
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


        public async Task<RespuestasDTO> registrarEstudiante(EstudianteDTOs estudianteDTOs)
        {
            _logger.LogTrace("Iniciando metodo EstudianteCommands.registrarEstudiante...");
            try
            {
                var newEstudiante = EstudianteDTOs.CrearE(estudianteDTOs);
                await _context.EstudianteEs.AddAsync(newEstudiante);
                await _context.SaveChangesAsync();

                if (newEstudiante.id_Estudiante != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        mensaje = "¡Estudiante registrado correctamente!"
                    };
                }else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        mensaje = "¡No se logro registrar el estudiante!"
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo EstudianteCommands.registrarEstudiante...");
                throw;
            }
        }


        public async Task<RespuestasDTO> ActualizarEstudiante(EstudianteDTOs estudianteDTOs)
        {
            _logger.LogTrace("Iniciando metodo EstudianteCommands.ActualizarEstudiante...");
            try
            {
                var existeEstudiante = await _context.EstudianteEs.FirstOrDefaultAsync(x => x.id_Estudiante == estudianteDTOs.Id);

                if (existeEstudiante != null)
                {
                    existeEstudiante.s_nombre = estudianteDTOs.Nombre;
                    existeEstudiante.s_documento = estudianteDTOs.Documento;
                    existeEstudiante.s_correo = estudianteDTOs.Correo;

                    _context.EstudianteEs.Update(existeEstudiante);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        mensaje = "¡Estudiante actualizado correctamente!"
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        mensaje = "¡No se logro actualizar el estudiante!"
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo EstudianteCommands.ActualizarEstudiante...");
                throw;
            }
        }
    }
}
