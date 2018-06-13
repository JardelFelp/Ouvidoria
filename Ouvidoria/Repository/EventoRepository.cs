using Ouvidoria.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ouvidoria.Repository
{
    public class EventoRepository
    {
        internal static bool TemEvento()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.EventoTipo.Any();
            }
        }

        internal static void CadastraPadroes(List<EventoTipo> eventos)
        {
            using (var db = new OuvidoriaContext())
            {
                db.EventoTipo.AddRange(eventos);
                db.SaveChanges();
            }
        }
    }
}