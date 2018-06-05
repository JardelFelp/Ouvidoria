using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ouvidoria.Models
{
    [Table("Evento")]
    public class Evento
    {

        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Tamanho máximo de 4000 caracteres")]
        public string Descricao { get; set; }

        [MaxLength(4000, ErrorMessage = "Tamanho máximo de 4000 caracteres")]
        public string Resposta { get; set; }

        public bool Respondido { get; set; }

        [ForeignKey("EventoTipo")]
        public int idEventoTipo { get; set; }

        public virtual EventoTipo EventoTipo { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}