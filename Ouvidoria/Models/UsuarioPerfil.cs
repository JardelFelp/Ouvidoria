using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    [Table("UsuarioPerfil")]
    public class UsuarioPerfil
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Perfil { get; set; }
    }
}