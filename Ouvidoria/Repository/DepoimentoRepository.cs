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

        internal static void CadastraDepoimento(Depoimento evento)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Depoimento.Add(evento);
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

        internal static Depoimento EncontrarDepoimento(int? id)
        {
            using (var db = new OuvidoriaContext())
            {                
                return db.Depoimento.Find(id);
            }
        }

        internal static void EditarDepoimento(Depoimento evento)
        {
            using (var db = new OuvidoriaContext())
            {
                var eventoOriginal = db.Depoimento.FirstOrDefault(x => x.id == evento.id);
                eventoOriginal.idTipoDepoimento = evento.idTipoDepoimento;
                eventoOriginal.Titulo = evento.Titulo;
                eventoOriginal.Descricao = evento.Descricao;
                db.Entry(eventoOriginal).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal static void ExcluirDepoimento(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var evento = db.Depoimento.Find(id);
                db.Depoimento.Remove(evento);
                db.SaveChanges();
            }
        }

        internal static string ValidaDepoimento(int? id, int usuario)
        {
            using (var db = new OuvidoriaContext())
            {
                var evento = EncontrarDepoimento(id);
                if (evento == null || evento.idUsuario != usuario)
                    return "Depoimento nao encontrado";
                if (evento.Respondido == true)
                    return "Depoimento ja respondido";
                else
                    return "";
            }
        }
    }
}