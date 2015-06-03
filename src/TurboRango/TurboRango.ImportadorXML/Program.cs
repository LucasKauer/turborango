﻿using System;
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

            #region LINQ to XML
            const string nomeArquivo = "restaurantes.xml";
            // const === final (Java) eh uma constante

            var restaurantesXML = new RestaurantesXML(nomeArquivo);

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

            var connString = @"Data Source=.;Initial Catalog=TurboRango_dev;UID=sa;PWD=feevale";
            // Integrated Security=True; --> em casa!

            var acessoAoBanco = new CarinhaQueManipulaOBanco(connString);

            //F11 entra na depuracao
            acessoAoBanco.Inserir(new Contato
            {
                Site = "www.dogao.gif",
                Telefone = "55555555"
            });

            #endregion

            IEnumerable<Contato> contatos = acessoAoBanco.GetContatos();
        }
    }
}
