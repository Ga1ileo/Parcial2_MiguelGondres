using Microsoft.EntityFrameworkCore;
using Parcial2_MiguelGondres.DAL;
using Parcial2_MiguelGondres.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parcial2_MiguelGondres.BLL
{
    public class LlamadaDetalleBll
    {
        public static bool Guardar(Llamadas llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Llamadas.Add(llamada) != null)
                    paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Llamadas llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM LlamadaDetalle Where LlamadaId={llamada.LlamadaId}");
                foreach(var item in llamada.LlamadaDetalle)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(llamada).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Llamadas.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static Llamadas Buscar(int id)
        {
            Contexto db = new Contexto();
            Llamadas llamada = new Llamadas();

            try
            {
                llamada = db.Llamadas.Where(x => x.LlamadaId == id).
                    Include(o => o.LlamadaDetalle).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return llamada;
        }

        public static List<Llamadas> GetList(Expression<Func<Llamadas, bool>> llamada)
        {
            List<Llamadas> lista = new List<Llamadas>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Llamadas.Where(llamada).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return lista;
        }
    }
}
