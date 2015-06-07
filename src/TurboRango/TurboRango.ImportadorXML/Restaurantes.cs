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
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var inserirRestaurante = new SqlCommand(INSERT_SQL_RESTAURANTE, connection))
                {
                    inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria;
                    inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.Int).Value = InserirContato(restaurante.Contato);
                    inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.Int).Value = InserirLocalizacao(restaurante.Localizacao);

                    connection.Open();
                    int idCriado = Convert.ToInt32(inserirRestaurante.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", idCriado);
                }
            }
        }

        public void Remover(int id)
        {
             using (var connection = new SqlConnection(this.ConnectionString))
             {
                 using (var removerRestaurante = new SqlCommand(DELETE_SQL_RESTAURANTE, connection))
                 {
                     removerRestaurante.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                     
                     connection.Open();
                 }
             }
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
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var inserirContato = new SqlCommand(INSERT_SQL_CONTATO, connection))
                {
                    // http://stackoverflow.com/questions/4958379/what-is-the-difference-between-null-and-system-dbnull-value
                    // http://eduardopires.net.br/2012/08/c-sharp-iniciantes-syntactic-sugar/
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site ?? (object) DBNull.Value;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone ?? (object) DBNull.Value;

                    connection.Open();
                    int idCriado = Convert.ToInt32(inserirContato.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", idCriado);

                    return idCriado;
                }
            }
        }

        private int InserirLocalizacao(Localizacao localizacao)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var inserirLocalizacao = new SqlCommand(INSERT_SQL_LOCALIZACAO, connection))
                {
                    inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;

                    connection.Open();
                    int idCriado = Convert.ToInt32(inserirLocalizacao.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", idCriado);

                    return idCriado;
                }
            }
        }
    }
}
