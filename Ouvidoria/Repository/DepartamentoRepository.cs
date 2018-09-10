using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}