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
        public int LlamadaId { get; set; }
        public string Problema { get; set; }
        public string Solucion { get; set; }

        public LlamadasDetalle()
        {
            LlamadaDetalleId = 0;
            LlamadaId = 0;
            Problema = string.Empty;
            Solucion = string.Empty;
        }

        public LlamadasDetalle(int llamadaid, string problema, string solucion)
        {
            LlamadaDetalleId = 0;
            LlamadaId = llamadaid;
            Problema = problema;
            Solucion = solucion;
        }

    }
}
