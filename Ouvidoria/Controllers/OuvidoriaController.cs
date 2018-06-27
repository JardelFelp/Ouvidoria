using Microsoft.AspNet.Identity;
using Ouvidoria.Filters;
using Ouvidoria.Models;
using Ouvidoria.Service;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("1")]
    public class OuvidoriaController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Depoimento()
        {
            EventoService.VerificaEventos();
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Depoimento([Bind(Include = "id,Titulo,Descricao,idEventoTipo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                db.Evento.Add(evento);
                db.SaveChanges();
                return RedirectToAction("MeusDepoimentos");
            }

            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            return View(evento);
        }

        public ActionResult Feedback()
        {
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback([Bind(Include = "id,Avaliacao,Comentario,idDepartamento")] DepartamentoDepoimento depoimento)
        {
            if (ModelState.IsValid)
            {
                depoimento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                db.DepartamentoDepoimento.Add(depoimento);
                db.SaveChanges();
                return RedirectToAction("MeusFeedbacks");
            }

            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            return View(depoimento);
        }

        public ActionResult MeusFeedbacks()
        {
            return View(db.DepartamentoDepoimento
                        .Include(d => d.Departamento)
                        .ToList()
                        .Where(d => d.idUsuario == Convert.ToInt32(User.Identity.GetUserId())));

        }

        public ActionResult MeusDepoimentos()
        {
            return View(db.Evento
                        .Include(d => d.EventoTipo)
                        .ToList()
                        .Where(d => d.idUsuario == Convert.ToInt32(User.Identity.GetUserId())));
        }
        

        public ActionResult EditarDepoimento(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("MeusDepoimentos");
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null || evento.idUsuario != Convert.ToInt32(User.Identity.GetUserId()))
            {
                TempData["Error"] = "Depoimento nao encontrado";
                return RedirectToAction("MeusDepoimentos");
            }
            if (evento.Respondido == true)
            {
                TempData["Error"] = "Depoimento ja respondido";
                return RedirectToAction("MeusDepoimentos");
            }
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDepoimento([Bind(Include = "id,Titulo,Descricao,idEventoTipo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.Respondido = false;
                evento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                evento.Resposta = null;
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MeusDepoimentos");
            }
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            return View(evento);
        }

        public ActionResult ExcluirDepoimento(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            if (evento.idUsuario != Convert.ToInt32(User.Identity.GetUserId()) || evento.Respondido == true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirDepoimento(int id)
        {
            Evento evento = db.Evento.Find(id);
            db.Evento.Remove(evento);
            db.SaveChanges();
            return RedirectToAction("MeusDepoimentos");
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