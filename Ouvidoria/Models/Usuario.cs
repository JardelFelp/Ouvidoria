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
        [Key]
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

        /*
         * [Required]
        [MaxLength(20, ErrorMessage = "Tamanho máximo de 20 caracteres")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo de 6 caracteres")]
        public string Senha { get; set; }
        */

        [ForeignKey("Curso")]
        public int idCurso { get; set; }

        public virtual Curso Curso { get; set; }

        [ForeignKey("UsuarioPerfil")]
        public int idUsuarioPerfil { get; set; }
        
        public virtual UsuarioPerfil UsuarioPerfil { get; set; }

    }
}