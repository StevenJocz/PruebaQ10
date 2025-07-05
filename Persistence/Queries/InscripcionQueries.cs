using Domain.Prueba.DTOs;
using Infrastructure.Prueba;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Prueba.Queries
{
    public interface IInscripcionQueries
    {
        Task<List<MateriaDTOs>> ListarInscripcionesXEstudiantes(int IdEstudiante);
    }

    public class InscripcionQueries: IInscripcionQueries, IDisposable
    {
        private readonly Context _context = null;
        private readonly ILogger<IInscripcionQueries> _logger;
        private readonly IConfiguration _configuration;

        public InscripcionQueries(ILogger<InscripcionQueries> logger, IConfiguration configuration)
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


        public async Task<List<MateriaDTOs>> ListarInscripcionesXEstudiantes(int IdEstudiante)
        {
            _logger.LogTrace("Iniciando metodo InscripcionQueries.ListarInscripcionesXEstudiantes...");
            try
            {
                var inscripciones = await (
                                            from i in _context.InscripcionEs
                                            join m in _context.MateriaEs on i.id_materia equals m.id_materia
                                            where i.id_Estudiante == IdEstudiante
                                            select new
                                            {
                                                i.id_Inscripcion,                    
                                                i.id_Estudiante,
                                                m.id_materia,
                                                m.s_nombre,
                                                m.s_codigo,
                                                m.i_creditos
                                            }
                                           ).ToListAsync();


                var listaMaterias = new List<MateriaDTOs>();

                if (inscripciones.Count > 0)
                {
                    foreach (var item in inscripciones)
                    {
                        var lista = new MateriaDTOs
                        {
                            id = item.id_Estudiante,
                            nombre = item.s_nombre,
                            codigo = item.s_codigo,
                            creditos = item.i_creditos,
                        };

                        listaMaterias.Add(lista);
                    }
                }
                

                return listaMaterias;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo InscripcionQueries.ListarInscripcionesXEstudiantes...");
                throw;
            }
        }
    }
}
