using Ouvidoria.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ouvidoria.Repository
{
    public class FeedbackRepository
    {
        internal static void CadastraDepoimento(Feedback depoimento)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Feedback.Add(depoimento);
                db.SaveChanges();
            }
        }

        internal static IEnumerable<Feedback> RetornaFeedbacks(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimentos =
                                db.Feedback
                                .Include(d => d.Departamento)
                                .ToList()
                                .Where(d => d.idUsuario == id);
                return depoimentos;
            }
        }
    }
}