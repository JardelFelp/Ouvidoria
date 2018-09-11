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

        internal static object RetornaTodosDepartamentos()
        {
            throw new NotImplementedException();
        }

        internal static void ValidaDepartamento(int? id)
        {
            throw new NotImplementedException();
        }
    }
}