using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ouvidoria.Models
{
    [Table("Pergunta")]
    public class Pergunta
    {
        public int id { get; set; }

        [Required]
        public string Descricao { get; set; }

        public Tipo tipo { get; set; }

        public List<Opcao> Opcao { get; set; }

        public enum Tipo
        {
            Dissertativa,
            Objetiva
        }
    }
}