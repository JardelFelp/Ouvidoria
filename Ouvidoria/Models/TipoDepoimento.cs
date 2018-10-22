using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ouvidoria.Models
{
    [Table("TipoDepoimento")]
    public class TipoDepoimento
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Tipo { get; set; }

        public TipoDepoimento()
        { }

        public TipoDepoimento(string curso)
        {
            Tipo = curso;
        }
    }
}