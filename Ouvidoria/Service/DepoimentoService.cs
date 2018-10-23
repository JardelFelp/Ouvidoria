using Ouvidoria.Models;
using Ouvidoria.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ouvidoria.Service
{
    public class DepoimentoService
    {
        public static SelectList BuscaTipoDepoimento()
        {
            return DepoimentoRepository.BuscaTipoDepoimento();
        }

        public static void VerificaEventos()
        {
            if (!TemDepoimento())
            {
                CadastrarPadroes();
            }
        }

        public static bool TemDepoimento()
        {
            return DepoimentoRepository.TemDepoimento();
        }

        private static void CadastrarPadroes()
        {
            List<TipoDepoimento> depoimento = new List<TipoDepoimento>()
            {
                new TipoDepoimento("Denuncia"),
                new TipoDepoimento("Elogio"),
                new TipoDepoimento("Reclamacao"),
                new TipoDepoimento("Sugestao"),
            };
            DepoimentoRepository.CadastraPadroes(depoimento);
        }

        public static void CadastraDepoimento(Depoimento evento)
        {
            DepoimentoRepository.CadastraDepoimento(evento);
        }

        public static Depoimento RetornaDepoimento(int? id)
        {
            return DepoimentoRepository.RetornaDepoimento(id);
        }

        public static IEnumerable<Depoimento> RetornaDepoimentos(int id)
        {
            return DepoimentoRepository.RetornaDepoimentos(id);
        }

        public static string ValidaDepoimento(int? id)
        {
            return DepoimentoRepository.ValidaDepoimento(id);
        }

        public static string ValidaDepoimento(int? id, int usuario)
        {
            return DepoimentoRepository.ValidaDepoimento(id, usuario);
        }

        public static Depoimento EncontraDepoimento(int? id)
        {
            return DepoimentoRepository.EncontraDepoimento(id);
        }

        public static void EditaDepoimento(Depoimento evento)
        {
            DepoimentoRepository.EditaDepoimento(evento);
        }

        public static void ExcluiDepoimento(int id)
        {
            DepoimentoRepository.ExcluiDepoimento(id);
        }
    }
}