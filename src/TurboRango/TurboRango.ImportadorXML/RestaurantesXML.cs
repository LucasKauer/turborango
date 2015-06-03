using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class RestaurantesXML
    {
        public string NomeArquivo { get; private set; }

        IEnumerable<XElement> restaurantes;
        
        // ctor + tab + tab --> cria construtor
        /// <summary>
        /// Constrói RestauranteXML a partir de um nome de arquivo
        /// </summary>
        /// <param name="nomeArquivo"> Nome do arquivo XML a ser manipulado</param>
        public RestaurantesXML(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            restaurantes = XDocument.Load(NomeArquivo).Descendants("restaurante");
			contato = XDocument.Load(NomeArquivo).Descendants("restaurante").Descendants("contato"));
        }

        #region Obter Nomes
        public IList<string> ObterNomes()
        {
            //var resultado = new List<string>();

            //var nodos = restaurantes;

            //foreach (var item in nodos)
            //{
            //    resultado.Add(item.Attribute("nome").Value);
            //}

            //return resultado;

            var res = restaurantes
                .Select(_ => new Restaurante
                {
                    Nome = _.Attribute("nome").Value,
                    Capacidade = Convert.ToInt32(_.Attribute("capacidade").Value)
                });

            return res.Where(x => x.Capacidade < 100).Select(x => x.Nome).OrderBy(x => x).ToList();

            // return (
            // return from n in restaurantes
                // orderby n.Attribute("nome").Value // descending
                // where Convert.ToInt32(n.Attribute("capacidade").Value) = 100;
                // select n.Attribute("nome").Value
            // ).ToList();

        }
		#endregion
		
        #region Capacidade Media
        public double CapacidadeMedia()
        {
            return (
                from n in restaurantes
                select Convert.ToInt32(n.Attribute("capacidade").Value)
            ).Average();
        }
        #endregion

        #region Capacidade Maxima
        public double CapacidadeMaxima()
        {
            return (
                from n in restaurantes
                select Convert.ToInt32(n.Attribute("capacidade").Value)
            ).Max();
        }
        #endregion

        public object AgruparPorCategoria()
        {
            var res = from n in restaurantes
                      group n by n.Attribute("categoria").Value into g
                      select new {
                          Categoria = g.Key,
                          Restaurante = g.ToList(),
                          SomatorioCapacidade = g.Sum(x => Convert.ToInt32(x.Attribute("capacidade").Value))
                      };

            return res;
			// throw new NotImplementedException();
        }
		
		// Tema
		
		#region Ordena Por Nome Asc
		public IList<string> OrdenarPorNomeAsc()
		{
			var res = restaurantes
                .Select(_ => new Restaurante
                {
                    Nome = _.Attribute("nome").Value ascending,
                    Capacidade = Convert.ToInt32(_.Attribute("capacidade").Value)
                });

            return res.ToList(); // res.Select(x => x.Nome).OrderBy(y => y).ToList();
			
		}
        #endregion
		
		#region Obter Sites

		public IList<string> ObterSites()
		{
			var res = contato
                .Select(_ => new Restaurante
                {
                    Site = _.Value
                });

            return res.ToList();
			
		}
        #endregion
    
		#region Apenas Com Um Restaurante 
		public IList<Categoria> ApenasComUmRestaurante()
		{
			
		}
		#endregion
	}
}
