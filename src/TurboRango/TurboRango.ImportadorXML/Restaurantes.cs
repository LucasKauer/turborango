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
        readonly static string DELETE_SQL_CONTATO = "DELETE FROM [dbo].[Contato] WHERE Id = @Id";
        readonly static string DELETE_SQL_LOCALIZACAO = "DELETE FROM [dbo].[Restaurante] WHERE Id = @Id";
        // SELECT AUX
        readonly static string SELECT_SQL_CONTATO_AUX = "SELECT Id FROM Contato c WHERE EXISTS (SELECT ContatoId FROM Restaurante r WHERE Id = @Id and r.ContatoId = c.Id); SELECT @@IDENTITY";
        readonly static string SELECT_SQL_LOCALIZACAO_AUX = "SELECT Id FROM Localizacao l WHERE EXISTS (SELECT ContatoId FROM Restaurante r WHERE Id = @Id and r.LocalizacaoId = l.Id); SELECT @@IDENTITY";

        readonly static string SELECT_SQL_RESTAURANTE = "SELECT [Restaurante].[Capacidade], [Restaurante].[Nome], [Restaurante].[Categoria],"
            + " [Contato].[Site], [Contato].[Telefone],"
            + " [Localizacao].[Bairro], [Localizacao].[Logradouro], [Localizacao].[Latitude], [Localizacao].[Longitude]"
            + " FROM [dbo].[Restaurante]"
            + " INNER JOIN [dbo].[Contato] ON [dbo].[Contato].Id = [dbo].[Restaurante].ContatoId"
            + " INNER JOIN [dbo].[Localizacao] ON [dbo].[Localizacao].Id = [dbo].[Restaurante].LocalizacaoId";


        public Restaurantes(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Insere restaurante no banco de dados a partir de um objeto Restaurante
        /// </summary>
        /// <param name="restaurante">Restaurante a ser manipulado</param>
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

        /// <summary>
        /// Remove restaurante no banco de dados a partir de um id
        /// </summary>
        /// <param name="id">Id do restaurante a ser manipulado</param>
        internal void Remover(int id)
        {
             var idContato = BuscaContatoPassandoIdRestaurante(id);
             var idLocalizacao = BuscaLocalizacaoPassandoIdRestaurante(id);

             using (var connection = new SqlConnection(this.ConnectionString))
             {
                 connection.Open();

                 using (var removerRestaurante = new SqlCommand(DELETE_SQL_RESTAURANTE, connection))
                 {
                     removerRestaurante.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                     removerRestaurante.ExecuteNonQuery();
                 }

                 using (var removerContato = new SqlCommand(DELETE_SQL_CONTATO, connection))
                 {
                     removerContato.Parameters.Add("@Id", SqlDbType.NVarChar).Value = idContato;
                     removerContato.ExecuteNonQuery();
                 }

                 using (var removerLocalizacao = new SqlCommand(DELETE_SQL_LOCALIZACAO, connection))
                 {
                     removerLocalizacao.Parameters.Add("@Id", SqlDbType.NVarChar).Value = idLocalizacao;
                 }
             }
        }

        /// <summary>
        /// Busca e lista todos os restaurantes que estão no banco de dados
        /// </summary>
        /// <returns>Retorna uma lista de Restaurantes</returns>
        public IEnumerable<Restaurante> Todos()
        {
            var listaRestaurantes = new List<Restaurante>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var selecionarTodosRestaurantes = new SqlCommand(SELECT_SQL_RESTAURANTE, connection))
                {
                    connection.Open();

                    var reader = selecionarTodosRestaurantes.ExecuteReader();

                    while (reader.Read())
                    {
                        listaRestaurantes.Add(new Restaurante
                        {
                            Capacidade = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Categoria = (Categoria) Enum.Parse(typeof(Categoria), reader.GetString(2), ignoreCase: true),
                            Contato = new Contato
                            {
                                Site = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Telefone = reader.IsDBNull(4) ? null : reader.GetString(4)
                            },
                            Localizacao = new Localizacao
                            {
                                Bairro = reader.GetString(5),
                                Logradouro = reader.GetString(6),
                                Latitude = reader.GetDouble(7),
                                Longitude = reader.GetDouble(8),
                            }
                        });
                    }
                }
            }
            
            return listaRestaurantes;
        }

        /// <summary>
        /// Atualiza restaurante no banco de dados a partir de um id e de um Restaurante
        /// </summary>
        /// <param name="id">Id do restaurante a ser manipulado</param>
        /// <param name="restaurante">Restaurante atualizado</param>
        public void Atualizar(int id, Restaurante restaurante)
        {

        }

        /// <summary>
        /// Metodo privado que insere um Contato na tabela Contato no banco de dados.
        /// Auxilia na inserção de um restaurante na tabela Restaurante
        /// </summary>
        /// <param name="contato">Contato a ser manipulado</param>
        /// <returns>Retorna o Id do Contato inserido</returns>
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

        /// <summary>
        /// Metodo privado que insere uma Localizacao na tabela Localizacao no banco de dados.
        /// Auxilia na inserção de um restaurante na tabela Restaurante
        /// </summary>
        /// <param name="localizacao">Localizacao a ser manipulada</param>
        /// <returns>Retorna o Id da Localizacao inserida</returns>
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

        /// <summary>
        /// Seleciona o Contato a partir do Id do restaurante.
        /// </summary>
        /// <param name="id">Id do restaurante a ser manipulado</param>
        /// <returns></returns>
        private int BuscaContatoPassandoIdRestaurante(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var selecionarContato = new SqlCommand(SELECT_SQL_CONTATO_AUX, connection))
                {
                    selecionarContato.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;

                    connection.Open();

                    int idContato = Convert.ToInt32(selecionarContato.ExecuteScalar());

                    return idContato;
                }
            }
        }

        /// <summary>
        /// Seleciona a Localizacao a partir do Id do restaurante.
        /// </summary>
        /// <param name="id">Id do restaurante a ser manipulado</param>
        /// <returns></returns>
        private int BuscaLocalizacaoPassandoIdRestaurante(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var selecionarLocalizacao = new SqlCommand(SELECT_SQL_LOCALIZACAO_AUX, connection))
                {
                    selecionarLocalizacao.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;

                    connection.Open();

                    int idLocalizacao = Convert.ToInt32(selecionarLocalizacao.ExecuteScalar());

                    return idLocalizacao;
                }
            }
        }
    }
}
