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

namespace Persistence.Prueba.Commands
{
    public interface IMateriaCommands
    {
        Task<RespuestasDTO> registrarMateria(MateriaDTOs materiaDTOs);
        Task<RespuestasDTO> ActualizarMateria(MateriaDTOs materiaDTOs);
    }
    public class MateriaCommands: IMateriaCommands, IDisposable
    {
        private readonly Context _context = null;
        private readonly ILogger<IMateriaCommands> _logger;
        private readonly IConfiguration _configuration;

        public MateriaCommands(ILogger<MateriaCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("BaseDatos");
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

        public async Task<RespuestasDTO> registrarMateria(MateriaDTOs materiaDTOs)
        {
            _logger.LogTrace("Iniciando metodo MateriaCommands.registrarMateria...");
            try
            {
                var newMateria = materiaDTOs.CrearE(materiaDTOs);
                await _context.MateriaEs.AddAsync(newMateria);
                await _context.SaveChangesAsync();

                if (newMateria.id_materia != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        mensaje = "¡Materia registrada correctamente!"
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        mensaje = "¡No se logro registrar la materia!"
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo MateriaCommands.registrarMateria...");
                throw;
            }
        }

        public async Task<RespuestasDTO> ActualizarMateria(MateriaDTOs materiaDTOs)
        {
            _logger.LogTrace("Iniciando metodo MateriaCommands.ActualizarMateria...");
            try
            {
                var existeMateria = await _context.MateriaEs.FirstOrDefaultAsync(x => x.id_materia == materiaDTOs.id);

                if (existeMateria != null)
                {
                    existeMateria.s_nombre = materiaDTOs.nombre;
                    existeMateria.s_codigo = materiaDTOs.codigo;
                    existeMateria.i_creditos = materiaDTOs.creditos;

                    _context.MateriaEs.Update(existeMateria);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        mensaje = "¡Materia actualizada correctamente!"
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        mensaje = "¡No se logro actualizar la materia!"
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo MateriaCommands.ActualizarMateria...");
                throw;
            }
        }
    }
}
