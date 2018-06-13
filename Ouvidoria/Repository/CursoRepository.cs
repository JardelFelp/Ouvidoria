using Ouvidoria.Models;
using System.Collections.Generic;
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
    }
}