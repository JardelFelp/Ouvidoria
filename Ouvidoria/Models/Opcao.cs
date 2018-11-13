using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ouvidoria.Models
{
    [Table("Opcao")]
    public class Opcao
    {
        public int id { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}