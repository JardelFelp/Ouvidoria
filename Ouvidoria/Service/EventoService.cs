using Ouvidoria.Models;
using Ouvidoria.Repository;
using System.Collections.Generic;

namespace Ouvidoria.Service
{
    public class EventoService
    {
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

    }
}