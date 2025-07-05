using Domain.Prueba.DTOs;
using Infrastructure.Prueba;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Prueba.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Prueba.Queries
{
    public interface IEstudianteQueries
    {
        Task<List<EstudianteDTOs>> ListarEstudiantes();
        Task<EstudianteDTOs> ListarEstudianteId(int id);
    }
    public class EstudianteQueries: IEstudianteQueries, IDisposable
    {
        private readonly Context _context = null;
        private readonly ILogger<IEstudianteQueries> _logger;
        private readonly IConfiguration _configuration;

        public EstudianteQueries(ILogger<EstudianteQueries> logger, IConfiguration configuration)
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

        public async Task<List<EstudianteDTOs>> ListarEstudiantes()
        {
            _logger.LogTrace("Iniciando metodo EstudianteQueries.EstudianteDTOs...");
            try
            {
                var estudiantes = await _context.EstudianteEs.ToListAsync();
                var listaEstudiantes = new List<EstudianteDTOs>();
                foreach (var item in estudiantes)
                {
                    var lista = new EstudianteDTOs
                    {
                        Id = item.id_Estudiante,
                        Nombre = item.s_nombre,
                        Documento = item.s_documento,
                        Correo = item.s_correo,
                    };

                    listaEstudiantes.Add(lista);    
                }

                return listaEstudiantes;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo EstudianteQueries.EstudianteDTOs...");
                throw;
            }
        }

        public async Task<EstudianteDTOs> ListarEstudianteId(int id)
        {
            _logger.LogTrace("Iniciando metodo EstudianteQueries.ListarEstudianteId...");
            try
            {
                var estudiantesE = await _context.EstudianteEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_Estudiante == id);

                var estudiantes = new EstudianteDTOs
                {
                    Id = estudiantesE.id_Estudiante,
                    Nombre = estudiantesE.s_nombre,
                    Documento = estudiantesE.s_documento,
                    Correo = estudiantesE.s_correo,
                };

                return estudiantes;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo EstudianteQueries.ListarEstudianteId...");
                throw;
            }
        }
    }
}
