using Ouvidoria.Models;
using Ouvidoria.Utils;
using System.Linq;

namespace Ouvidoria.Repository
{
    public class UsuarioRepository //: RepositoryBase<Usuario>
    {
        public static bool TemPerfil()
        {
            using (var db = new OuvidoriaContext())
            {
                return db.UsuarioPerfil.Any();
            }
        }

        internal static void Cadastrar()
        {
            using (var db = new OuvidoriaContext())
            {
                UsuarioPerfil usuario = new UsuarioPerfil("Usuario");
                UsuarioPerfil administrador = new UsuarioPerfil("Administrador");
                db.UsuarioPerfil.Add(usuario);
                db.UsuarioPerfil.Add(administrador);
                db.SaveChanges();
                string senha = Hash.GerarHashMd5("administrador");
                Usuario admin = new Usuario("Admin", "ouvidoria@faculdadeam.edu.br", "(55) 3289-1139", senha);
                db.Usuario.Add(admin);
                db.SaveChanges();
            }
        }

        public static bool VerificaEmailValido(string email)
        {
            using (var db = new OuvidoriaContext())
            {
                return !(db.Usuario.Count(u => u.Email == email) > 0);
            }
        }

        public static void CadastraUsuario(Usuario novoUsuario)
        {
            using (var db = new OuvidoriaContext())
            {
                db.Usuario.Add(novoUsuario);
                db.SaveChanges();
            }
        }
    }
}