using Ouvidoria.Models;
using Ouvidoria.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ouvidoria.Service
{
    public class EventoService
    {
        public static SelectList BuscaEventoTipo()
        {
            return EventoRepository.BuscaEventoTipo();
        }

        public static void VerificaEventos()
        {
            if (!TemEvento())
            {
                CadastrarPadroes();
            }
        }

        public static bool TemEvento()
        {
            return EventoRepository.TemEvento();
        }

        private static void CadastrarPadroes()
        {
            List<EventoTipo> eventos = new List<EventoTipo>()
            {
                new EventoTipo("Denuncia"),
                new EventoTipo("Elogio"),
                new EventoTipo("Reclamacao"),
                new EventoTipo("Sugestao"),
            };
            EventoRepository.CadastraPadroes(eventos);
        }

        public static void CadastraEvento(Evento evento)
        {
            EventoRepository.CadastraEvento(evento);
        }

        public static IEnumerable<Evento> RetornaEventos(int id)
        {
            return EventoRepository.RetornaEventos(id);
        }

        public static string ValidaDepoimento(int? id, int usuario)
        {
            return EventoRepository.ValidaDepoimento(id, usuario);
        }

        public static Evento EncontrarEvento(int? id)
        {
            return EventoRepository.EncontrarEvento(id);
        }

        public static void EditarEvento(Evento evento)
        {
            EventoRepository.EditarEvento(evento);
        }

        public static void ExcluirEvento(int id)
        {
            EventoRepository.ExcluirEvento(id);
        }
    }
}