using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Exemplos
            /* Restaurante restaurante = new Restaurante();
            Console.WriteLine(restaurante.Capacidade.HasValue ? 
                restaurante.Capacidade.Value.ToString() :
                "oi");

            restaurante.Nome = string.Empty + " ";

            Console.WriteLine(restaurante.Nome ?? "Nulo!!");

            Console.WriteLine( !string.IsNullOrEmpty(restaurante.Nome.Trim()) ? "tem valor" : "não tem valor" );

            var oQueEuGosto = "bacon";
            var texto = string.Format("Eu gosto de {0}", oQueEuGosto);
            // new c# (6) var texto = string.Format("Eu gosto de \{oQueEuGosto}");
            // StringBuilder pedreiro = new StringBuilder()/
            // pedreiro.AppendFormat("Eu gosto de {0}", oQueEuGosto)/
            // pedreiro.Append("!!!");

            object obj = new object();
            // int a = Convert.ToInt32(obj);

            // int convertido;
            // bool conseguiu = Int32.TryPaser("1", out convertido);

            // int a = (int)obj; --> tirar o dotValue
            int? a = obj as int?;
            int res = 12 + a.Value;

            Console.WriteLine(texto);*/
            #endregion

            const string nomeArquivo = "restaurantes.xml";
            // const === final (Java) eh uma constante

            var restaurantesXML = new RestaurantesXML(nomeArquivo);

            var nomes = restaurantesXML.ObterNomes();

            var capacidadeMedia = restaurantesXML.CapacidadeMedia;
            var capacidadeMaxima = restaurantesXML.CapacidadeMaxima;
        }
    }
}
