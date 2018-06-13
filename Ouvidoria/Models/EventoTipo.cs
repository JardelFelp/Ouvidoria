using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("EventoTipo")]
    public class EventoTipo
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Tipo { get; set; }

        public EventoTipo()
        { }

        public EventoTipo(string curso)
        {
            Tipo = curso;
        }
    }
}