using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parcial2_MiguelGondres.Entidades
{
    public class LlamadasDetalle
    {
        [Key]
        public int LlamadaDetalleId { get; set; }
        public string Problemas { get; set; }
        public string Solucion { get; set; }

        public LlamadasDetalle()
        {
            LlamadaDetalleId = 0;
            Problemas = string.Empty;
            Solucion = string.Empty;
        }

        public LlamadasDetalle(int llamadaId, string problema, string solucion)
        {
            LlamadaDetalleId = 0;
            Problemas = problema;
            Solucion = solucion;
        }

    }
}
