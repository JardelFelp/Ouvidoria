using Ouvidoria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Service
{
    public class TipoDepoimentoService
    {
        public static SelectList RetornaTipoDepoimento(int id)
        {
            return TipoDepoimentoRepository.RetornaTipoDepoimento(id);
        }
    }
}