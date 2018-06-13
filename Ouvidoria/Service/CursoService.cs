using Ouvidoria.Models;
using Ouvidoria.Repository;
using System.Collections.Generic;

namespace Ouvidoria.Service
{
    public class CursoService
    {
        public static void VerificaCurso()
        {
            if (!TemCurso())
            {
                Cadastrar();
            }
        }

        public static bool TemCurso()
        {
            return CursoRepository.TemCurso();
        }

        public static void Cadastrar()
        {
            List<Curso> cursos = new List<Curso>()
                {
                    new Curso("Administracao"),
                    new Curso("Direito"),
                    new Curso("Ontopsicologia"),
                    new Curso("Pedagogia"),
                    new Curso("Sistemas de Informacao")
                };
            CursoRepository.Cadastrar(cursos);
        }
    }
}