using System.ComponentModel.DataAnnotations;

namespace Ouvidoria.ViewModels
{
    public class LoginViewModel
    {
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o e-mail para Login")]
        [MaxLength(100, ErrorMessage = "O login deve ter no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }
    }
}