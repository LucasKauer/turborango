using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class Restaurantes
    {
        private string ConnectionString { get; set; }

        readonly static string INSERT_SQL_RESTAURANTE = "INSERT INTO [dbo].[Restaurante] ([Capacidade], [Nome], [Categoria], [ContatoId], [LocalizacaoId]) VALUES (@Capacidade, @Nome, @Categoria, @ContatoId, @LocalizacaoId);";
        readonly static string INSERT_SQL_CONTATO = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES (@Site, @Telefone); SELECT @@IDENTITY";
        readonly static string INSERT_SQL_LOCALIZACAO = "INSERT INTO [dbo].[Localizacao] ([Bairro], [Logradouro], [Latitude], [Longitude]) VALUES (@Bairro , @Logradouro, @Latitude, @Longitude); SELECT @@IDENTITY";
        // readonly static string SELECT_SQL = "SELECT [Site],[Telefone] FROM [dbo].[Contato] (nolock)";

        public Restaurantes(string _connectionString)
        {
            ConnectionString = _connectionString;
        }

        internal void Inserir(Restaurante _restaurante)
        {
            using (var _connection = new SqlConnection(this.ConnectionString))
            {
                using (var _inserirRestaurante = new SqlCommand(INSERT_SQL_RESTAURANTE, _connection))
                {
                    _inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = _restaurante.Capacidade;
                    _inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = _restaurante.Nome;
                    _inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = _restaurante.Categoria;
                    _inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.Int).Value = InserirContato(_restaurante.Contato);
                    _inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.Int).Value = InserirLocalizacao(_restaurante.Localizacao);

                    _connection.Open();
                    int _idCriado = Convert.ToInt32(_inserirRestaurante.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", _idCriado);
                }

            }
        }

        private int InserirContato(Contato _contato)
        {
            using (var _connection = new SqlConnection(this.ConnectionString))
            {
                using (var _inserirContato = new SqlCommand(INSERT_SQL_CONTATO, _connection))
                {
                    _inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = _contato.Site;
                    _inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = _contato.Telefone;

                    _connection.Open();
                    int _idCriado = Convert.ToInt32(_inserirContato.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", _idCriado);

                    return _idCriado;
                }
            }
        }

        private int InserirLocalizacao(Localizacao _localizacao)
        {
            using (var _connection = new SqlConnection(this.ConnectionString))
            {
                using (var _inserirLocalizacao = new SqlCommand(INSERT_SQL_LOCALIZACAO, _connection))
                {
                    _inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = _localizacao.Bairro;
                    _inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = _localizacao.Logradouro;
                    _inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = _localizacao.Latitude;
                    _inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = _localizacao.Longitude;

                    _connection.Open();
                    int _idCriado = Convert.ToInt32(_inserirLocalizacao.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", _idCriado);

                    return _idCriado;
                }
            }
        }

    }
}
