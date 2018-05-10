using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    public class Reclamacao
    {
        public int id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "O título deve conter no máximo 50 caracteres")]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}