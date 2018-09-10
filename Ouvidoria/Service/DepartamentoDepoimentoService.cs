using Ouvidoria.Models;
using Ouvidoria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ouvidoria.Service
{
    public class DepartamentoDepoimentoService
    {
        public static void CadastraDepoimento(DepartamentoDepoimento depoimento)
        {
            DepartamentoDepoimentoRepository.CadastraDepoimento(depoimento);
        }

        public static IEnumerable<DepartamentoDepoimento> RetornaFeedbacks(int id)
        {
            return DepartamentoDepoimentoRepository.RetornaFeedbacks(id);
        }
    }
}