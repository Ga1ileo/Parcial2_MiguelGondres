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
        public static bool Guardar(Llamada llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Llamada.Add(llamada) != null)
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

        public static bool Modificar(Llamada llamada)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM LlamadaDetalle Where LlamadaId={llamada.LlamadaId}");
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
                var eliminar = db.Llamada.Find(id);
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

        public static Llamada Buscar(int id)
        {
            Contexto db = new Contexto();
            Llamada llamada = new Llamada();

            try
            {
                llamada = db.Llamada.Include(x => x.LlamadaDetalle)
                    .Where(p => p.LlamadaId == id)
                    .SingleOrDefault();
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

        public static List<Llamada> GetList(Expression<Func<Llamada, bool>> llamada)
        {
            List<Llamada> lista = new List<Llamada>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Llamada.Where(llamada).ToList();
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
