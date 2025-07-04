using Infrastructure.Prueba;
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
    }
}
