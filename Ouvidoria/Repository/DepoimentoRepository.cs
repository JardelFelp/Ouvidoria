using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Ouvidoria.Repository
{
    public class DepoimentoRepository
    {
        internal static bool TemDepoimento()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.TipoDepoimento.Any();
            }
        }

        internal static SelectList BuscaTipoDepoimento()
        {
            using (var db = new OuvidoriaContext())
            {
                return new SelectList(db.TipoDepoimento, "id", "Tipo");
            }
        }

        internal static void CadastraPadroes(List<TipoDepoimento> eventos)
        {
            using (var db = new OuvidoriaContext())
            {
                db.TipoDepoimento.AddRange(eventos);
                db.SaveChanges();
            }
        }

        internal static void CadastraDepoimento(Depoimento depoimento)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Depoimento.Add(depoimento);
                db.SaveChanges();
            }
        }

        internal static IEnumerable<Depoimento> RetornaDepoimentos(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimentos = db.Depoimento
                                .Include(d => d.TipoDepoimento)
                                .ToList()
                                .Where(d => d.idUsuario == id);
                return depoimentos;
            }
        }

        internal static Depoimento RetornaDepoimento(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Depoimento
                       .Include(e => e.TipoDepoimento)
                       .Include(e => e.Usuario)
                       .SingleOrDefault(x => x.id == id);
            }
        }

        internal static Depoimento EncontraDepoimento(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Depoimento.Find(id);
            }
        }

        internal static void EditaDepoimento(Depoimento depoimento)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimentoOriginal = db.Depoimento.FirstOrDefault(x => x.id == depoimento.id);
                depoimentoOriginal.idTipoDepoimento = depoimento.idTipoDepoimento;
                depoimentoOriginal.Titulo = depoimento.Titulo;
                depoimentoOriginal.Descricao = depoimento.Descricao;
                db.Entry(depoimentoOriginal).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal static void ExcluiDepoimento(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimento = db.Depoimento.Find(id);
                db.Depoimento.Remove(depoimento);
                db.SaveChanges();
            }
        }

        internal static string ValidaDepoimento(int? id)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimento = EncontraDepoimento(id);
                if (depoimento == null)
                    return "Depoimento nao encontrado";
                if (depoimento.Respondido == true)
                    return "Depoimento ja respondido";
                else
                    return "";
            }
        }

        internal static string ValidaDepoimento(int? id, int usuario)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimento = EncontraDepoimento(id);
                if (depoimento == null || depoimento.idUsuario != usuario)
                    return "Depoimento nao encontrado";
                if (depoimento.Respondido == true)
                    return "Depoimento ja respondido";
                else
                    return "";
            }
        }
    }
}