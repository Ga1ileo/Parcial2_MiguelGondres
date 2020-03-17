using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parcial2_MiguelGondres.Entidades
{
    public class LlamadaDetalle
    {
        [Key]
        public int LlamadaDetalleId { get; set; }
        public int LlamadaId { get; set; }
        public string Problemas { get; set; }
        public string Solucion { get; set; }

        public LlamadaDetalle()
        {
            LlamadaDetalleId = 0;
            LlamadaId = 0;
            Problemas = string.Empty;
            Solucion = string.Empty;
        }

        public LlamadaDetalle(int LlamadaId, string problema, string solucion)
        {
            LlamadaDetalleId = 0;
            LlamadaId = 0;
            Problemas = problema;
            Solucion = solucion;
        }

    }
}
