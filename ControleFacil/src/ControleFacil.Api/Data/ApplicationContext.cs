using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ControleFacil.Api.Data.Mappings;

namespace ControleFacil.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<NaturezaDeLancamento> NaturezaDeLancamento { get; set; }

        public DbSet<Apagar> Apagar { get; set; }

        public DbSet<Areceber> Areceber { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new UsuarioMap());
            modelbuilder.ApplyConfiguration(new NaturezaDeLancamentoMap());
            modelbuilder.ApplyConfiguration(new ApagarMap());
            modelbuilder.ApplyConfiguration(new AreceberMap());
        }
    }
}