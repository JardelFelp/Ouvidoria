using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ouvidoria.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage = "Informe o seu nome")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o seu email")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Telefone { get; set; }

        public int idCurso { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [MaxLength(20, ErrorMessage = "A senha deve ter no máximo 20 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [MaxLength(20, ErrorMessage = "A senha deve ter no máximo 20 caracteres")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não correspondem")]
        public string ConfirmaSenha { get; set; }
    }
}