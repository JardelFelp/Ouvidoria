using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Curso")]
    public class Curso
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Máximo de caracteres: 50")]
        public string Nome { get; set; }

        public Curso()
        { }

        public Curso(string nome)
        {
            Nome = nome;
        }
    }
}