using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Evento")]
    public class Evento
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Tamanho máximo de 4000 caracteres")]
        public string Descricao { get; set; }

        [Required]
        public EventoTipo Tipo { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}