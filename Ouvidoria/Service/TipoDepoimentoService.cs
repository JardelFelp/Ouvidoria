using Ouvidoria.Models;
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
        public static void TemTipos()
        {
            if (!TipoDepoimentoRepository.TemTipos())
            {
                CadastraPadroes();
            }
        }

        public static SelectList RetornaTipoDepoimento(int id)
        {
            return TipoDepoimentoRepository.RetornaTipoDepoimento(id);
        }

        public static void CadastraPadroes()
        {
            List<TipoDepoimento> tipos = new List<TipoDepoimento>()
                {
                    new TipoDepoimento("Denúncia"),
                    new TipoDepoimento("Elogio"),
                    new TipoDepoimento("Reclamação"),
                    new TipoDepoimento("Sugestão")
                };
            TipoDepoimentoRepository.CadastraPadroes(tipos);
        }
    }
}