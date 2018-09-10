using Ouvidoria.Models;
using Ouvidoria.Repository;
using System;
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

        public static List<Curso> RetornaCursos()
        {
            return CursoRepository.RetornaCursos();
        }

        public static string ValidaCurso(int? id)
        {
            return CursoRepository.ValidaCurso(id);
        }

        public static Curso RetornaCurso(int? id)
        {
            return CursoRepository.RetornaCurso(id);
        }

        public static void CadastrarCurso(Curso curso)
        {
            CursoRepository.CadastrarCurso(curso);
        }

        internal static void EditarCurso(Curso curso)
        {
            CursoRepository.EditarCurso(curso);
        }

        internal static void DeletarCurso(int id)
        {
            CursoRepository.DeletarCurso(id);
        }
    }
}