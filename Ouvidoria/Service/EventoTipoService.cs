using Ouvidoria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Service
{
    public class EventoTipoService
    {
        public static SelectList RetornaEventoTipo(int id)
        {
            return EventoTipoRepository.RetornaEventoTipo(id);
        }
    }
}