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
    }
}
