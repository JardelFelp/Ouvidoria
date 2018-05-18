using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ouvidoria.Models
{
    public class OuvidoriaContext : DbContext
    {
        public OuvidoriaContext():base("Ouvidoria")
        {
            Database.Log = instrucao => System.Diagnostics.Debug.WriteLine(instrucao);
        }

        public DbSet<Evento> Evento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}