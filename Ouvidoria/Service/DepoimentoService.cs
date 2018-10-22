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

        public static void VerificaDepoimentos()
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
            List<TipoDepoimento> eventos = new List<TipoDepoimento>()
            {
                new TipoDepoimento("Denuncia"),
                new TipoDepoimento("Elogio"),
                new TipoDepoimento("Reclamacao"),
                new TipoDepoimento("Sugestao"),
            };
            DepoimentoRepository.CadastraPadroes(eventos);
        }

        public static void CadastraDepoimento(Depoimento evento)
        {
            DepoimentoRepository.CadastraDepoimento(evento);
        }

        public static IEnumerable<Depoimento> RetornaDepoimentos(int id)
        {
            return DepoimentoRepository.RetornaDepoimentos(id);
        }

        public static string ValidaDepoimento(int? id, int usuario)
        {
            return DepoimentoRepository.ValidaDepoimento(id, usuario);
        }

        public static Depoimento EncontrarDepoimento(int? id)
        {
            return DepoimentoRepository.EncontrarDepoimento(id);
        }

        public static void EditarDepoimento(Depoimento evento)
        {
            DepoimentoRepository.EditarDepoimento(evento);
        }

        public static void ExcluirDepoimento(int id)
        {
            DepoimentoRepository.ExcluirDepoimento(id);
        }
    }
}