using Ouvidoria.Models;
using Ouvidoria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Service
{
    public class DepartamentoService
    {
        public static bool TemDepartamento()
        {
            return DepartamentoRepository.TemDepartamento();
        }

        public static SelectList RetornaDepartamentos(int? id)
        {
            return DepartamentoRepository.RetornaDepartamentos(id);
        }

        public static List<Departamento> RetornaTodosDepartamentos()
        {
            return DepartamentoRepository.RetornaTodosDepartamentos();
        }

        public static string ValidaDepartamento(int? id)
        {
            return DepartamentoRepository.ValidaDepartamento(id);
        }

        public static Departamento RetornaDepartamento(int? id)
        {
            return DepartamentoRepository.RetornaDepartamento(id);
        }

        public static void CadastraDepartamento(Departamento departamento)
        {
            DepartamentoRepository.CadastraDepartamento(departamento);
        }

        public static void EditaDepartamento(Departamento departamento)
        {
            DepartamentoRepository.EditaDepartamento(departamento);
        }

        public static void ExcluiDepartamento(int id)
        {
            DepartamentoRepository.ExcluiDepartamento(id);
        }
    }
}