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
        public DbSet<EventoTipo> EventoTipo { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<DepartamentoDepoimento> DepartamentoDepoimento { get; set; }
    }
}