using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Parcial2_MiguelGondres.Entidades;

namespace Parcial2_MiguelGondres.DAL
{
    public class Contexto : DbContext
    {

        public DbSet<Llamada> Llamada { get; set; }
        public DbSet<LlamadaDetalle> llamadaDetalles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(@"Data Source= DAL\DATA\RParcial2.db");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
