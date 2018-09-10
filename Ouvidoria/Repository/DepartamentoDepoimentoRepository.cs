using Ouvidoria.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ouvidoria.Repository
{
    public class DepartamentoDepoimentoRepository
    {
        internal static void CadastraDepoimento(DepartamentoDepoimento depoimento)
        {
            using (var db = new OuvidoriaContext())
            {
                db.DepartamentoDepoimento.Add(depoimento);
                db.SaveChanges();
            }
        }

        internal static IEnumerable<DepartamentoDepoimento> RetornaFeedbacks(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimentos =
                                db.DepartamentoDepoimento
                                .Include(d => d.Departamento)
                                .ToList()
                                .Where(d => d.idUsuario == id);
                return depoimentos;
            }
        }
    }
}