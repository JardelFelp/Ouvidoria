﻿using Ouvidoria.Models;
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

        internal static List<int> GetDepoimentosRegistrados()
        {
            using (var db = new OuvidoriaContext())
            {
                var feedbacks = new List<int>();
                feedbacks.Add(db.Depoimento.Where(x => x.idTipoDepoimento == 1).Count());
                feedbacks.Add(db.Depoimento.Where(x => x.idTipoDepoimento == 2).Count());
                feedbacks.Add(db.Depoimento.Where(x => x.idTipoDepoimento == 3).Count());
                feedbacks.Add(db.Depoimento.Where(x => x.idTipoDepoimento == 4).Count());
                return feedbacks;
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

        internal static IEnumerable<Depoimento> RetornaDepoimentos(int id, int tipo)
        {
            using (var db = new OuvidoriaContext())
            {
                var depoimentos = db.Depoimento
                                .Include(d => d.TipoDepoimento)
                                .ToList()
                                .Where(d => d.idUsuario == id &&
                                            d.idTipoDepoimento == tipo);
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
        
        internal static int GetDepoimentosRespondidos()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Depoimento.Where(x => x.Respondido == true).Count();
            }
        }

        internal static int GetDepoimentosNaoRespondidos()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Depoimento.Where(x => x.Respondido == false).Count();
            }
        }

        internal static void ExcluirDepoimento(int id)
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