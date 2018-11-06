using Ouvidoria.Filters;
using Ouvidoria.Models;
using Ouvidoria.Service;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class DepoimentosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public List<Depoimento> GetDepoimentos()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.Depoimento.Include(e => e.TipoDepoimento).Include(e => e.Usuario).ToList();
            }
        }

        public ActionResult Responder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var retorno = DepoimentoService.ValidaDepoimento(id);

            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("Index");
            }

            var depoimento = DepoimentoService.RetornaDepoimento(id);
            ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo", depoimento.idTipoDepoimento);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", depoimento.idUsuario);
            return View(depoimento);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Responder([Bind(Include = "id,Titulo,Descricao,idTipoDepoimento,idUsuario, Resposta")] Depoimento evento)
        {
            if (ModelState.IsValid)
            {
                if(evento.Resposta == "" || evento.Resposta == null)
                {
                    var evento2 = db.Depoimento.Include(e => e.TipoDepoimento)
                              .Include(e => e.Usuario)
                              .SingleOrDefault(x => x.id == evento.id);
                    ModelState.AddModelError("Resposta", "Preencha a resposta");
                    ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo", evento.idTipoDepoimento);
                    ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
                    return View(evento2);
                }
                evento.Respondido = true;
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo", evento.idTipoDepoimento);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
            return View(evento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
