using Infrastructure.Prueba;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Persistence.Prueba.Commands
{
    public interface IEstudianteCommands
    {

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



    }
}
