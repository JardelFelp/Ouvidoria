using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Retorno")]
    public class Retorno
    {
        public int id { get; set; }

        [ForeignKey("Questionario")]
        public int idQuestionario { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        public DateTime DataRetorno { get; set; } = DateTime.Now;

        public List<Resposta> Resposta { get; set; }

        public virtual Questionario Questionario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}