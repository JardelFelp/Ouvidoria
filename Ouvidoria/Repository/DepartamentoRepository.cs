using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Ouvidoria.Repository
{
    public class DepartamentoRepository
    {
        internal static SelectList RetornaDepartamentos(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                if (id == null)
                    return new SelectList(db.Departamento, "id", "Nome");
                return new SelectList(db.Departamento, "id", "Nome", id);

            }
        }

        internal static List<Departamento> RetornaTodosDepartamentos()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Departamento.ToList();
            }
        }

        internal static Departamento RetornaDepartamento(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Departamento.Find(id);
            }
        }

        internal static void CadastraDepartamento(Departamento departamento)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Departamento.Add(departamento);
                db.SaveChanges();
            }
        }

        internal static void ExcluiDepartamento(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Departamento.Remove(RetornaDepartamento(id));
                db.SaveChanges();
            }
        }

        internal static void EditaDepartamento(Departamento departamento)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal static string ValidaDepartamento(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                if (RetornaDepartamento(id) == null)
                    return "Departamento não encontrado.";
                return "";
            }
        }
    }
}