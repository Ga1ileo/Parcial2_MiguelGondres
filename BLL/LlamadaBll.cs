using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Parcial2_MiguelGondres.DAL;
using Parcial2_MiguelGondres.Entidades;


namespace Parcial2_MiguelGondres.BLL
{
    public class LlamadaBll
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
                llamada = db.Llamada.Find(id);
                    
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

        public static List<Llamada> GetList(Expression<Func<Llamada,bool>> llamada)
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
