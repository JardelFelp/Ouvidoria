using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public int id { get; set; }

        public int Avaliacao { get; set; }

        [MaxLength(200, ErrorMessage = "Máximo de 200 caracteres")]
        public string Comentario { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Departamento")]
        public int idDepartamento { get; set; }

        public virtual Departamento Departamento { get; set; }
    }
}