using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Email { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Tamanho máximo de 15 caracteres")]
        public string Telefone { get; set; }

        /*[Required]
        [MaxLength(20, ErrorMessage = "Tamanho máximo de 20 caracteres")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo de 6 caracteres")]
        public string Senha { get; set; }*/

        public Cursos Curso { get; set; }

        [Required]
        public UsuarioPerfil Perfil { get; set; }

    }
}