﻿using Ouvidoria.Models;
using Ouvidoria.Utils;
using Ouvidoria.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    public class AutenticacaoController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(CadastroUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
                return View(viewModel);
            }

            if (db.Usuario.Count(u => u.Email == viewModel.Email) > 0)
            {
                ModelState.AddModelError("Email", "Esse email já está em uso");
                ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
                return View(viewModel);
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = viewModel.Nome,
                Senha = Hash.GerarHashMd5(viewModel.Senha),
                Email = viewModel.Email,
                Telefone = viewModel.Telefone,
                idCurso = Convert.ToInt32(viewModel.idCurso)
            };

            db.Usuario.Add(novoUsuario);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Login(string ReturnUrl)
        {
            var viewModel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var usuario = db.Usuario.FirstOrDefault(u => u.Email == viewModel.Email);

            if (usuario == null || usuario.Senha != Hash.GerarHashMd5(viewModel.Senha))
            {
                ModelState.AddModelError("Login", "Login ou senha incorreta");
                return View(viewModel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Email", usuario.Email)
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(viewModel.UrlRetorno) || Url.IsLocalUrl(viewModel.UrlRetorno))
                return Redirect(viewModel.UrlRetorno);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}