using Ouvidoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace Ouvidoria.Controllers
{
    public class QuestionariosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        public ActionResult Index()
        {
            return View(db.Questionario.ToList());
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "id,Titulo, Descricao, DataInicio, DataFim, Pergunta, Pergunta.Opcao")] Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                db.Questionario.Add(questionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

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
                TempData["Error"] = "Já existem respostas para o questionario em questao, o que significa que o mesmo nao pode ser editado";
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,Titulo, Descricao, DataInicio, DataFim, Perguntas, Perguntas.Opcoes")] Questionario questionario, int[] _perguntas, int[] _opcoes)
        {
            if (ModelState.IsValid)
            {
                if (_opcoes != null)
                    RemoverOpcoes(_opcoes);
                if (_perguntas != null)
                    RemoverPerguntas(_perguntas);

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
            return View(questionario);
        }

        [HttpPost]
        public void RemoverOpcoes(int[] opcoes)
        {
            var options = (from opcao in db.Opcao
                           where opcoes.Contains(opcao.id)
                           select opcao).ToList();
            foreach (var opcao in options)
                db.Entry(opcao).State = EntityState.Deleted;
            db.SaveChanges();
        }

        [HttpPost]
        public void RemoverPerguntas(int[] perguntas)
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