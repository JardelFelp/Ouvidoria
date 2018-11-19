using Microsoft.AspNet.Identity;
using Ouvidoria.Filters;
using Ouvidoria.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    public class QuestionariosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        [Authorize]
        public ActionResult Index()
        {
            var user = db.Usuario.Find(Convert.ToInt32(User.Identity.GetUserId()));
            if (user.idUsuarioPerfil == 1)
            {
                
                var respondidos = db.Retorno.Where(x => x.idUsuario == user.id).Select(x => x.idQuestionario).ToArray();
                return View(db.Questionario
                              .Where(x => !respondidos.Contains(x.id)
                                       && x.DataFim > DateTime.Now
                                       && x.DataInicio < DateTime.Now)
                              .ToList());
            }
            return View(db.Questionario.ToList());
        }

        [AutorizacaoFiltro("2")]
        public ActionResult PreVisualizar(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            Questionario questionario = db.Questionario
                                          .Include(x => x.Pergunta.Select(y => y.Opcao))
                                          .FirstOrDefault(x => x.id == id);

            if (questionario == null)
            {
                TempData["Error"] = "Questionario nao encontrado";
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [AutorizacaoFiltro("2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "id,Titulo, Descricao, DataInicio, DataFim, Pergunta, Pergunta.Opcao")] Questionario questionario)
        {
            if (questionario.DataInicio >= questionario.DataFim)
            {
                ModelState.AddModelError("DataFim", "A data final deve ser maior que a data inicial");
            }
            else if (ModelState.IsValid)
            {
                db.Questionario.Add(questionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        [AutorizacaoFiltro("2")]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Questionario questionario = db.Questionario
                                          .Include(x => x.Pergunta.Select(y => y.Opcao))
                                          .FirstOrDefault(x => x.id == id);
            if (questionario == null)
            {
                TempData["Error"] = "Questionario nao encontrado";
                return RedirectToAction("Index");
            }

            if (db.Retorno.Any(x => x.idQuestionario == id))
            {
                TempData["Error"] = "Ja existem respostas para o questionario em questao, o que significa que o mesmo nao pode ser editado";
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        [AutorizacaoFiltro("2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,Titulo, Descricao, DataInicio, DataFim, Pergunta, Pergunta.Opcao")] Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                foreach (var pergunta in questionario.Pergunta)
                {
                    if (pergunta.id > 0)
                    {
                        if (pergunta.Opcao != null)
                        {
                            foreach (var opcao in pergunta.Opcao)
                            {
                                if (opcao.id > 0)
                                    db.Entry(opcao).State = EntityState.Modified;
                                else
                                    db.Entry(opcao).State = EntityState.Added;
                            }
                        }
                        db.Entry(pergunta).State = EntityState.Modified;
                    }
                    else
                        db.Entry(pergunta).State = EntityState.Added;
                }
                db.Entry(questionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            questionario = db.Questionario
                             .Include(x => x.Pergunta.Select(y => y.Opcao))
                             .FirstOrDefault(x => x.id == questionario.id);

            return View(questionario);
        }

        [AutorizacaoFiltro("1")]
        public ActionResult Responder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Questionario questionario = db.Questionario
                                      .Include(x => x.Pergunta.Select(y => y.Opcao))
                                      .FirstOrDefault(x => x.id == id);

            if (questionario == null)
            {
                TempData["Error"] = "Questionario nao encontrado";
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        [AutorizacaoFiltro("1")]
        [HttpPost]
        public ActionResult Responder([Bind(Include = "id, idQuestionario, Resposta")] Retorno retorno)
        {
            retorno.idUsuario = Convert.ToInt32(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Retorno.Add(retorno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Questionario questionario = db.Questionario
                                          .Include(x => x.Pergunta.Select(y => y.Opcao))
                                          .FirstOrDefault(x => x.id == retorno.idQuestionario);
            return View(questionario);
        }

        public ActionResult Respostas(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var retorno = db.Retorno
                          .Include(x => x.Questionario)
                          .Include(x => x.Usuario)
                          .Where(x => x.idQuestionario == id)
                          .ToList();

            if (retorno == null)
            {
                TempData["Error"] = "Respostas nao encontradas";
                return RedirectToAction("Index");
            }
            
            return View(retorno);
        }

        public ActionResult Resposta(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var retorno = db.Retorno
                          .Include(x => x.Questionario)
                          .Include(x => x.Usuario)
                          .Include(x => x.Resposta.Select(y => y.Pergunta))
                          .Include(x => x.Resposta.Select(y => y.Opcao))
                          .FirstOrDefault(x => x.id == id);
            if (retorno == null)
            {
                TempData["Error"] = "Respostas nao encontradas";
                return RedirectToAction("Index");
            }

            return View(retorno);
        }

        [AutorizacaoFiltro("2")]
        [HttpPost]
        public void RemoverOpcoes(int[] opcoes)
        {
            if (opcoes != null)
            {
                var options = (from opcao in db.Opcao
                               where opcoes.Contains(opcao.id)
                               select opcao).ToList();
                foreach (var opcao in options)
                    db.Entry(opcao).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        [AutorizacaoFiltro("2")]
        [HttpPost]
        public void RemoverPerguntas(int[] perguntas)
        {
            if (perguntas != null)
            {
                var questions = (from pergunta in db.Pergunta
                                 where perguntas.Contains(pergunta.id)
                                 select pergunta).ToList();
                foreach (var pergunta in questions)
                    db.Entry(pergunta).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

    }
}