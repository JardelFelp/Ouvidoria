using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Questionario")]
    public class Questionario
    {
        public int id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        public List<Pergunta> Pergunta  { get; set; }
    }
}