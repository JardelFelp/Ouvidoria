using System.Data.Entity;

namespace Ouvidoria.Models
{
    public class OuvidoriaContext : DbContext
    {
        public OuvidoriaContext() : base("Ouvidoria")
        {
            Database.Log = instrucao => System.Diagnostics.Debug.WriteLine(instrucao);
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoDepoimento> TipoDepoimento { get; set; }
        public DbSet<Depoimento> Depoimento { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Opcao> Opcao { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Questionario> Questionario { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
        public DbSet<Retorno> Retorno { get; set; }
    }
}
