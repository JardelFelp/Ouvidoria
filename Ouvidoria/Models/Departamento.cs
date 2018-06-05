using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        public int id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Máximo de caracteres: 50")]
        public string Nome { get; set; }
    }
}