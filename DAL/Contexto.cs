using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Parcial2_MiguelGondres.Entidades;

namespace Parcial2_MiguelGondres.DAL
{
    public class Contexto : DbContext
    {

        public DbSet<Llamadas> Llamadas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=DAL\DATA\parcial.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Llamadas>().HasData(new Llamadas { LlamadaId = 1, Descripcion = "Servicio de Guitarra" });
            modelBuilder.Entity<LlamadasDetalle>().HasData(new LlamadasDetalle { LlamadaDetalleId = 1, LlamadaId = 1, Problema = "No tengo Cuerdas de Guitarra", Solucion = "Comprar mas Cuerdas" });
        }
    }
}
