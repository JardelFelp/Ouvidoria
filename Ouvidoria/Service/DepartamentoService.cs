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
        public static SelectList RetornaDepartamentos(int? id)
        {
            return DepartamentoRepository.RetornaDepartamentos(id);
        }

        public static IEnumerable<Departamento> RetornaTodosDepartamentos()
        {
            return DepartamentoRepository.RetornaTodosDepartamentos();

        }

        internal static string ValidaDepartamento(int? id)
        {
            return DepartamentoRepository.ValidaDepartamento(id);
        }
    }
}