using Ouvidoria.Models;
using Ouvidoria.Repository;
using Ouvidoria.Utils;
using Ouvidoria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ouvidoria.Service
{
    public class UsuarioService
    {
        public static void VerificaPerfis()
        {
            if (!TemPerfil())
            {
                Cadastrar();
            }
        }
        public static bool TemPerfil()
        {
            return UsuarioRepository.TemPerfil();
        }

        public static void Cadastrar()
        {
            UsuarioRepository.Cadastrar();
        }

        public static bool VerificaEmailValido(string email)
        {
            return UsuarioRepository.VerificaEmailValido(email);
        }

        public static void CadastraUsuario(CadastroUsuarioViewModel viewModel)
        {
            Usuario novoUsuario = new Usuario
            {
                Nome = viewModel.Nome,
                Senha = Hash.GerarHashMd5(viewModel.Senha),
                Email = viewModel.Email,
                Telefone = viewModel.Telefone,
                idCurso = Convert.ToInt32(viewModel.idCurso)
            };
            UsuarioRepository.CadastraUsuario(novoUsuario);
        }
    }
}