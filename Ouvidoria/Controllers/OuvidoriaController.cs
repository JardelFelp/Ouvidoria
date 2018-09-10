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
            //ViewBag.idEventoTipo = DepartamentoService.RetornaDepartamentos(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Depoimento([Bind(Include = "id,Titulo,Descricao,idEventoTipo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                EventoService.CadastraEvento(evento);
                return RedirectToAction("MeusDepoimentos");
            }
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome", evento.idEventoTipo);
            //ViewBag.idEventoTipo = DepartamentoService.RetornaDepartamentos(evento.idEventoTipo);
            return View(evento);
        }
         
        public ActionResult Feedback()
        {
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            //DepartamentoService.RetornaDepartamentos(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback([Bind(Include = "id,Avaliacao,Comentario,idDepartamento")] DepartamentoDepoimento depoimento)
        {
            if (ModelState.IsValid)
            {
                depoimento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                DepartamentoDepoimentoService.CadastraDepoimento(depoimento);
                return RedirectToAction("MeusFeedbacks");
            }

            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            //ViewBag.idDepartamento = DepartamentoService.RetornaDepartamentos(null);
            return View(depoimento);
        }

        public ActionResult MeusFeedbacks()
        {
            var feedbacks = DepartamentoDepoimentoService.RetornaFeedbacks(Convert.ToInt32(User.Identity.GetUserId()));
            return View(feedbacks);
        }

        public ActionResult MeusDepoimentos()
        {
            var depoimentos = EventoService.RetornaEventos(Convert.ToInt32(User.Identity.GetUserId()));
            return View(depoimentos);
        }
        

        public ActionResult EditarDepoimento(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("MeusDepoimentos");
            }

            var retorno = EventoService.ValidaDepoimento(id, Convert.ToInt32(User.Identity.GetUserId()));

            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("MeusDepoimentos");
            }

            Evento evento = EventoService.EncontrarEvento(id);
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", id);
            //ViewBag.idEventoTipo = EventoTipoService.RetornaEventoTipo(evento.idEventoTipo);
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDepoimento([Bind(Include = "id,Titulo,Descricao,idEventoTipo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                EventoService.EditarEvento(evento);
                return RedirectToAction("MeusDepoimentos");
            }
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            //ViewBag.idEventoTipo = EventoTipoService.RetornaEventoTipo(evento.idEventoTipo);
            return View(evento);
        }

        public ActionResult ExcluirDepoimento(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("MeusDepoimentos");
            }
            var retorno = EventoService.ValidaDepoimento(id, Convert.ToInt32(User.Identity.GetUserId()));

            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("MeusDepoimentos");
            }
            Evento evento = EventoService.EncontrarEvento(id);
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            //ViewBag.idEventoTipo = EventoTipoService.RetornaEventoTipo(evento.idEventoTipo);
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirDepoimento(int id)
        {
            EventoService.ExcluirEvento(id);
            return RedirectToAction("MeusDepoimentos");
        }
    }
}