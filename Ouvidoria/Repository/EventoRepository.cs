using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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

        internal static SelectList BuscaEventoTipo()
        {
            using (var db = new OuvidoriaContext())
            {
                return new SelectList(db.EventoTipo, "id", "Tipo");
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

        internal static void CadastraEvento(Evento evento)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Evento.Add(evento);
                db.SaveChanges();
            }
        }

        internal static IEnumerable<Evento> RetornaEventos(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimentos = db.Evento
                                .Include(d => d.EventoTipo)
                                .ToList()
                                .Where(d => d.idUsuario == id);
                return depoimentos;
            }
        }

        internal static Evento EncontrarEvento(int? id)
        {
            using (var db = new OuvidoriaContext())
            {                
                return db.Evento.Find(id);
            }
        }

        internal static void EditarEvento(Evento evento)
        {
            using (var db = new OuvidoriaContext())
            {
                var eventoOriginal = db.Evento.FirstOrDefault(x => x.id == evento.id);
                eventoOriginal.idEventoTipo = evento.idEventoTipo;
                eventoOriginal.Titulo = evento.Titulo;
                eventoOriginal.Descricao = evento.Descricao;
                db.Entry(eventoOriginal).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal static void ExcluirEvento(int id)
        {
            using (var db = new OuvidoriaContext())
            {
                var evento = db.Evento.Find(id);
                db.Evento.Remove(evento);
                db.SaveChanges();
            }
        }

        internal static string ValidaDepoimento(int? id, int usuario)
        {
            using (var db = new OuvidoriaContext())
            {
                var evento = EncontrarEvento(id);
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