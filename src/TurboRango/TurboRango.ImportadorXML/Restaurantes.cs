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

        readonly static string DELETE_SQL_RESTAURANTE = "DELETE FROM [dbo].[Restaurante] WHERE Id = @Id";

        public Restaurantes(string connectionString)
        {
            ConnectionString = connectionString;
        }

        internal void Inserir(Restaurante restaurante)
        {
            using (var _connection = new SqlConnection(this.ConnectionString))
            {
                using (var _inserirRestaurante = new SqlCommand(INSERT_SQL_RESTAURANTE, _connection))
                {
                    _inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    _inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    _inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria;
                    _inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.Int).Value = InserirContato(restaurante.Contato);
                    _inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.Int).Value = InserirLocalizacao(restaurante.Localizacao);

                    _connection.Open();
                    int _idCriado = Convert.ToInt32(_inserirRestaurante.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", _idCriado);
                }
            }
        }

        public void Remover(int id)
        {
            // using (var _connection = new SqlConnection(this.ConnectionString))
            // {
            //     using (var _removerRestaurante = new SqlCommand(DELETE_SQL_RESTAURANTE, _connection))
            //     {
            //         _removerRestaurante.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                     
            //         _connection.Open();
            //     }
            // }
        }

        public IEnumerable<Restaurante> Todos()
        {
            return null;
        }

        public void Atualizar(int id, Restaurante restaurante)
        {

        }

        private int InserirContato(Contato contato)
        {
            using (var _connection = new SqlConnection(this.ConnectionString))
            {
                using (var _inserirContato = new SqlCommand(INSERT_SQL_CONTATO, _connection))
                {
                    // http://stackoverflow.com/questions/4958379/what-is-the-difference-between-null-and-system-dbnull-value
                    // http://eduardopires.net.br/2012/08/c-sharp-iniciantes-syntactic-sugar/
                    _inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site ?? (object) DBNull.Value;
                    _inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone ?? (object) DBNull.Value;

                    _connection.Open();
                    int _idCriado = Convert.ToInt32(_inserirContato.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", _idCriado);

                    return _idCriado;
                }
            }
        }

        private int InserirLocalizacao(Localizacao localizacao)
        {
            using (var _connection = new SqlConnection(this.ConnectionString))
            {
                using (var _inserirLocalizacao = new SqlCommand(INSERT_SQL_LOCALIZACAO, _connection))
                {
                    _inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    _inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    _inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    _inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;

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
