using Persistence.Prueba.Commands;
using Persistence.Prueba.Queries;

namespace PruebaQ10.Aplication
{
    public static class StartupSetup
    {
        public static IServiceCollection AddStartupSetup(this IServiceCollection service, IConfiguration configuration)
        {
            // Commands Persistance
            service.AddTransient<IEstudianteCommands, EstudianteCommands>();
            service.AddTransient<IMateriaCommands, MateriaCommands>();
            service.AddTransient<IInscripcionCommands, InscripcionCommands>();

            // Queries Persistance
            service.AddTransient<IEstudianteQueries, EstudianteQueries>();
            service.AddTransient<IMateriaQueries, MateriaQueries>();
            service.AddTransient<IInscripcionQueries, InscripcionQueries>();

            return service;
        }
    }
}
