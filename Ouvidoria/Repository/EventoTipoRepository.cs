using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Repository
{
    public class EventoTipoRepository
    {
        internal static SelectList RetornaEventoTipo(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var eventoTipo =  new SelectList(db.EventoTipo, "id", "Tipo", id);
                return eventoTipo;
            }
        }
    }
}