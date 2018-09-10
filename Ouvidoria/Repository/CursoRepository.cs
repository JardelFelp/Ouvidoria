using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ouvidoria.Repository
{
    public class CursoRepository
    {

        public static bool TemCurso()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Curso.Any();
            }
        }

        public static void Cadastrar(List<Curso> cursos)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Curso.AddRange(cursos);
                db.SaveChanges();
            }
        }

        internal static List<Curso> RetornaCursos()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Curso.ToList();
            }
        }

        internal static string ValidaCurso(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                var curso = db.Curso.Find(id);
                if (curso == null)
                    return "Curso nao encontrado";
                return "";
            }
        }

        internal static Curso RetornaCurso(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Curso.Find(id);
            }
        }

        internal static void CadastrarCurso(Curso curso)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Curso.Add(curso);
                db.SaveChanges();
            }
        }

        internal static void EditarCurso(Curso curso)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal static void DeletarCurso(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                Curso curso = db.Curso.Find(id);
                db.Curso.Remove(curso);
                db.SaveChanges();
            }
        }
    }
}