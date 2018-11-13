using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("Resposta")]
    public class Resposta
    {
        public int id { get; set; }

        public string Conclusao { get; set; }

        [ForeignKey("Opcao")]
        public int? idOpcao { get; set; }

        [ForeignKey("Pergunta")]
        public int idPergunta { get; set; }

        public virtual Opcao Opcao { get; set; }

        public virtual Pergunta Pergunta { get; set; }
    }
}