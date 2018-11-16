using Microsoft.AspNet.Identity;
using Ouvidoria.Filters;
using Ouvidoria.Models;
using Ouvidoria.Service;
using System;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("1")]
    public class OuvidoriaController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        [Authorize]
        public ActionResult Index()
        {
            var teste = User.Identity.GetType();
            return View();
        }

        public ActionResult Denuncia()
        {
            ViewBag.denuncias = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()));

            Depoimento evento = new Depoimento
            {
                idTipoDepoimento = 1,
                idUsuario = Convert.ToInt32(User.Identity.GetUserId())
            };

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Denuncia(Depoimento denuncia)
        {
            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(denuncia);
                return RedirectToAction("Index");
            }
            return View(denuncia);
        }


        public ActionResult Elogio()
        {
            ViewBag.registros = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()));

            Depoimento evento = new Depoimento
            {
                idTipoDepoimento = 2,
                idUsuario = Convert.ToInt32(User.Identity.GetUserId())
            };

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Elogio(Depoimento elogio)
        {
            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(elogio);
                return RedirectToAction("Index");
            }
            return View(elogio);
        }

        public ActionResult Reclamacao()
        {
            Depoimento evento = new Depoimento
            {
                idTipoDepoimento = 3,
                idUsuario = Convert.ToInt32(User.Identity.GetUserId())
            };

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reclamacao(Depoimento reclamacao)
        {
            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(reclamacao);
                return RedirectToAction("Index");
            }
            return View(reclamacao);
        }

        public ActionResult Sugestao()
        {
            Depoimento evento = new Depoimento
            {
                idTipoDepoimento = 4,
                idUsuario = Convert.ToInt32(User.Identity.GetUserId())
            };

            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sugestao(Depoimento sugestao)
        {
            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(sugestao);
                return RedirectToAction("Index");
            }
            return View(sugestao);
        }


        public ActionResult Depoimento()
        {
            DepoimentoService.VerificaDepoimentos();
            ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo");
            //ViewBag.idEventoTipo = DepartamentoService.RetornaDepartamentos(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Depoimento([Bind(Include = "id,Titulo,Descricao,idTipoDepoimento")] Depoimento evento)
        {
            if (ModelState.IsValid)
            {
                evento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                DepoimentoService.CadastraDepoimento(evento);
                return RedirectToAction("MeusDepoimentos");
            }
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome", evento.idTipoDepoimento);
            //ViewBag.idEventoTipo = DepartamentoService.RetornaDepartamentos(evento.idEventoTipo);
            return View(evento);
        }

        public ActionResult Feedback()
        {
            if (!DepartamentoService.TemDepartamento())
            {
                TempData["Error"] = "Nao ha departamentos cadastrados. Favor entrar em contato com um administrador.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            //DepartamentoService.RetornaDepartamentos(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback([Bind(Include = "id,Avaliacao,Comentario,idDepartamento")] Feedback depoimento)
        {
            if (ModelState.IsValid)
            {
                depoimento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                FeedbackService.CadastraDepoimento(depoimento);
                return RedirectToAction("MeusFeedbacks");
            }

            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            //ViewBag.idDepartamento = DepartamentoService.RetornaDepartamentos(null);
            return View(depoimento);
        }

        public ActionResult MeusFeedbacks()
        {
            var feedbacks = FeedbackService.RetornaFeedbacks(Convert.ToInt32(User.Identity.GetUserId()));
            return View(feedbacks);
        }

        public ActionResult MeusDepoimentos()
        {
            var depoimentos = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()));
            return View(depoimentos);
        }
        

        public ActionResult EditarDepoimento(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("MeusDepoimentos");
            }

            var retorno = DepoimentoService.ValidaDepoimento(id, Convert.ToInt32(User.Identity.GetUserId()));

            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("MeusDepoimentos");
            }

            Depoimento depoimento = DepoimentoService.EncontraDepoimento(id);
            ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo", id);
            return View(depoimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDepoimento([Bind(Include = "id,Titulo,Descricao,idTipoDepoimento")] Depoimento depoimento)
        {
            if (ModelState.IsValid)
            {
                DepoimentoService.EditaDepoimento(depoimento);
                return RedirectToAction("MeusDepoimentos");
            }
            ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo", depoimento.idTipoDepoimento);
            return View(depoimento);
        }

        public ActionResult ExcluirDepoimento(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("MeusDepoimentos");
            }
            var retorno = DepoimentoService.ValidaDepoimento(id, Convert.ToInt32(User.Identity.GetUserId()));

            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("MeusDepoimentos");
            }
            Depoimento depoimento = DepoimentoService.EncontraDepoimento(id);
            ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo", depoimento.idTipoDepoimento);
            return View(depoimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirDepoimento(int id)
        {
            DepoimentoService.ExcluiDepoimento(id);
            return RedirectToAction("MeusDepoimentos");
        }
    }
}