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
    public interface IMateriaQueries
    {
        Task<List<MateriaDTOs>> ListarMaterias();
        Task<MateriaDTOs> ListarMateriaId(int id);
    }
    public class MateriaQueries: IMateriaQueries, IDisposable
    {
        private readonly Context _context = null;
        private readonly ILogger<IMateriaQueries> _logger;
        private readonly IConfiguration _configuration;

        public MateriaQueries(ILogger<MateriaQueries> logger, IConfiguration configuration)
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

        public async Task<List<MateriaDTOs>> ListarMaterias()
        {
            _logger.LogTrace("Iniciando metodo MateriaQueries.MateriaDTOs...");
            try
            {
                var materias = await _context.MateriaEs.ToListAsync();
                var listaMaterias = new List<MateriaDTOs>();
                foreach (var item in materias)
                {
                    var lista = new MateriaDTOs
                    {
                        id = item.id_materia,
                        nombre = item.s_nombre,
                        codigo = item.s_codigo,
                        creditos = item.i_creditos,
                    };

                    listaMaterias.Add(lista);
                }

                return listaMaterias;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo MateriaQueries.MateriaDTOs...");
                throw;
            }
        }

        public async Task<MateriaDTOs> ListarMateriaId(int id)
        {
            _logger.LogTrace("Iniciando metodo MateriaQueries.MateriaQueries...");
            try
            {
                var materiaE = await _context.MateriaEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_materia == id);

                var materia = new MateriaDTOs
                {
                    id = materiaE.id_materia,
                    nombre = materiaE.s_nombre,
                    codigo = materiaE.s_codigo,
                    creditos = materiaE.i_creditos,
                };

                return materia;
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo MateriaQueries.MateriaQueries...");
                throw;
            }
        }
    }
}
