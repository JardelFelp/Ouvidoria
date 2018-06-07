using System.Security.Cryptography;
using System.Text;

namespace Ouvidoria.Utils
{
    public class Hash
    {
        public static string GerarHashMd5(string senha)
        {
            MD5 md5Hash = MD5.Create();
            // Converte a Senha para array de bytes
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            // Cria um StringBuilder para recompor a string.
            StringBuilder texto = new StringBuilder();

            // Formata cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                texto.Append(data[i].ToString("x2"));
            }

            return texto.ToString();
        }
    }
}