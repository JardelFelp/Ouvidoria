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
            return View();
        }

        public ActionResult Denuncia()
        {
            var denuncias = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 1);
            return View(denuncias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Denuncia(Depoimento denuncia)
        {
            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(denuncia);
                TempData["Retorno"] = "Denuncia registrada com sucesso!";
            }
            else
                TempData["Retorno"] = "Por favor, verifique todos os campos!";
            var denuncias = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 1);
            return View(denuncias);
        }


        public ActionResult Elogio()
        {
            var elogios = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 2);
            return View(elogios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Elogio(Depoimento elogio)
        {
            elogio.idTipoDepoimento = 1;
            elogio.idUsuario = Convert.ToInt32(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(elogio);
                TempData["Retorno"] = "Elogio registrado com sucesso!";
            }
            else
                TempData["Retorno"] = "Por favor, verifique todos os campos!";
            var elogios = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 2);
            return View(elogios);
        }

        public ActionResult Reclamacao()
        {
            var reclamacoes = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 3);
            return View(reclamacoes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reclamacao(Depoimento reclamacao)
        {
            reclamacao.idTipoDepoimento = 3;
            reclamacao.idUsuario = Convert.ToInt32(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(reclamacao);
                TempData["Retorno"] = "Reclamacao registrado com sucesso!";
            }
            else
                TempData["Retorno"] = "Por favor, verifique todos os campos!";
            var reclamacoes = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 3);
            return View(reclamacoes);
        }

        public ActionResult Sugestao()
        {
            var sugestoes = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 4);
            return View(sugestoes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sugestao(Depoimento sugestao)
        {
            sugestao.idTipoDepoimento = 4;
            sugestao.idUsuario = Convert.ToInt32(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                DepoimentoService.CadastraDepoimento(sugestao);
                TempData["Retorno"] = "Sugestao registrado com sucesso!";
            }
            else
                TempData["Retorno"] = "Por favor, verifique todos os campos!";
            var sugestoes = DepoimentoService.RetornaDepoimentos(Convert.ToInt32(User.Identity.GetUserId()), 4);
            return View(sugestoes);
        }

        public ActionResult Feedback()
        {
            if (!DepartamentoService.TemDepartamento())
            {
                TempData["Error"] = "Nao ha departamentos cadastrados. Favor entrar em contato com um administrador.";
                return RedirectToAction("Index");
            }

            var feedbacks = FeedbackService.RetornaFeedbacks(Convert.ToInt32(User.Identity.GetUserId()));
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            return View(feedbacks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback([Bind(Include = "id,Avaliacao,Comentario,idDepartamento")] Feedback depoimento)
        {
            if (ModelState.IsValid)
            {
                depoimento.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
                FeedbackService.CadastraDepoimento(depoimento);
                TempData["Retorno"] = "Feedback registrado com sucesso!";
            }
            else
                TempData["Retorno"] = "Por favor, verifique todos os campos!";
            
            var feedbacks = FeedbackService.RetornaFeedbacks(Convert.ToInt32(User.Identity.GetUserId()));
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            return View(feedbacks);
        }

        public ActionResult EditarDepoimento(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var retorno = DepoimentoService.ValidaDepoimento(id, Convert.ToInt32(User.Identity.GetUserId()));

            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("Index");
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
                switch (depoimento.idTipoDepoimento)
                {
                    case 1:
                        return RedirectToAction("Denuncia");
                    case 2:
                        return RedirectToAction("Elogio");
                    case 3:
                        return RedirectToAction("Reclamacao");
                    case 4:
                        return RedirectToAction("Sugestao");
                    default:
                        return RedirectToAction("Index", "Home"); 
                }
            }
            ViewBag.idTipoDepoimento = new SelectList(db.TipoDepoimento, "id", "Tipo", depoimento.idTipoDepoimento);
            return View(depoimento);
        }

        public ActionResult ExcluirDepoimento(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var retorno = DepoimentoService.ValidaDepoimento(id, Convert.ToInt32(User.Identity.GetUserId()));

            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}