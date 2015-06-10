using System;
using System.Collections.Generic;
using System.Text;
using TurboRango.Dominio;
using System.Linq;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {

            const string nomeArquivo = "restaurantes.xml";
            var restaurantesXML = new RestaurantesXML(nomeArquivo);

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

            #region LINQ to XML
            /*const string nomeArquivo = "restaurantes.xml";
            // const === final (Java) eh uma constante
            var restaurantesXML = new RestaurantesXML(nomeArquivo);*/

            var nomes = restaurantesXML.ObterNomes();

            var ex1a = restaurantesXML.OrdenarPorNomeAsc();
            var ex1b = restaurantesXML.ObterSites();
            var ex1c = restaurantesXML.CapacidadeMedia();
            var ex1d = restaurantesXML.AgruparPorCategoria();
            var ex1e = restaurantesXML.ApenasComUmRestaurante();
            var ex1f = restaurantesXML.ApenasMaisPopulares();
            var ex1g = restaurantesXML.BairrosComMenosPizzarias();
            var ex1h = restaurantesXML.AgrupadosPorBairroPercentual();

            var todos = restaurantesXML.TodosRestaurantes();
            #endregion

            #region ADO.NET
            // var connString = @"Data Source=.;Initial Catalog=TurboRango_dev;UID=sa;PWD=feevale"; // --> feevale!
            var _connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TurboRango_dev;Integrated Security=True"; // --> em casa!

            var _acessoAoBanco = new CarinhaQueManipulaOBanco(_connString);

            // F11 entra na depuracao
            #endregion

            IEnumerable<Contato> _contatos = _acessoAoBanco.GetContatos();

            // TEMA: CRIANDO OBJETO DO TIPO RESTAURANTES
            var _restaurantes = new Restaurantes(_connString);

            const string _nomeArquivo = "restaurantes.xml";
            // const === final (Java) eh uma constante

            var _restaurantesXML = new RestaurantesXML(_nomeArquivo);
            List<Restaurante> _listaRestaurantes = _restaurantesXML.TodosRestaurantes().ToList();

            var listaComTodosRestaurantes = _restaurantes.Todos();
        }
    }
}