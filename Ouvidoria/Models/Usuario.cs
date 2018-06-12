using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ouvidoria.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Telefone { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Tamanho mínimo de 6 caracteres")]
        public string Senha { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("Curso")]
        public int? idCurso { get; set; }

        public virtual Curso Curso { get; set; }

        [ForeignKey("UsuarioPerfil")]
        public int idUsuarioPerfil { get; set; }

        public virtual UsuarioPerfil UsuarioPerfil { get; set; }

        public Usuario()
        {
            Ativo = true;
            idUsuarioPerfil = 1;
        }

        public Usuario(string nome, string email, string telefone, string senha)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Senha = senha;
            Ativo = true;
            idUsuarioPerfil = 2;
            idCurso = null;
        }
    }
}