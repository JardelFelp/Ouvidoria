﻿using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Repository
{
    public class TipoDepoimentoRepository
    {
        internal static SelectList RetornaTipoDepoimento(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var eventoTipo =  new SelectList(db.TipoDepoimento, "id", "Tipo", id);
                return eventoTipo;
            }
        }

        internal static bool TemTipos()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.TipoDepoimento.Any();
            }
        }

        internal static void CadastraPadroes(List<TipoDepoimento> depoimentos)
        {
            using (var db = new OuvidoriaContext())
            {
                db.TipoDepoimento.AddRange(depoimentos);
                db.SaveChanges();
            }
        }
    }
}