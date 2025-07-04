using Domain.Prueba.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Prueba
{
    public class Context: DbContext
    {
        private readonly string _connection;
        private readonly short _commandTimeOut;

        public Context(string connection)
        {
            _connection = connection;
            _commandTimeOut = 60;
        }

        public Context(string connection, short commandTimeOut)
        {
            _connection = connection;
            _commandTimeOut = commandTimeOut;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection, sqlServerOptions => sqlServerOptions.CommandTimeout(_commandTimeOut));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<EstudianteE> EstudianteEs { get; set; }
        public virtual DbSet<MateriaE> MateriaEs { get; set; }
        public virtual DbSet<InscripcionE> InscripcionEs { get; set; }
    }

}
