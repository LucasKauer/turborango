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
    public class CarinhaQueManipulaOBanco
    {

        // readonly? https://msdn.microsoft.com/pt-br/library/acdd6hb7.aspx
        //  A readonly campo pode ser inicializado na declaração ou em um construtor
        readonly string _connectionString;
        // convenção para constantes => TUDO_MAIUSCULO_SEPARADO_POR_UNDERLINE
        // readonly static = para ser criado apenas uma vez (por classe, não por instância)
        readonly static string INSERT_SQL = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES (@Site, @Telefone); SELECT @@IDENTITY";
        readonly static string SELECT_SQL = "SELECT [Site],[Telefone] FROM [dbo].[Contato] (nolock)";

        public CarinhaQueManipulaOBanco(string connectionString)
        {
            this._connectionString = connectionString;
        }

        internal void Inserir(Contato contato)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                using (var inserirContato = new SqlCommand(INSERT_SQL, connection))
                {
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone;

                    connection.Open();
                    int idCriado = Convert.ToInt32(inserirContato.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", idCriado);
                }

                // string comandoSQL = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES  (@Site, @Telefone)";
                // using (var inserirContato = new SqlCommand(comandoSQL, connection))
                // {
                //      [...]
                // }
            }
        }

        internal IEnumerable<Contato> GetContatos()
        {
            var contatos = new List<Contato>();

            using (var connection = new SqlConnection(this._connectionString))
            {
                using (var lerContatos = new SqlCommand(SELECT_SQL, connection))
                {
                    connection.Open();

                    var reader = lerContatos.ExecuteReader();

                    while (reader.Read())
                    {
                        contatos.Add(new Contato
                        {
                            Site = reader.GetString(0),
                            Telefone = reader.GetString(1)
                        });
                    }
                }
            }

            return contatos;
        }

        // string comandoSQL = "SELECT [Site],[Telefone] FROM [dbo].[Contato] (nolock)";
        // using (var lerContatos = new SqlCommand(comandoSQL, connection))
        // {
        //     [...]
        //
        //     while (reader.Read())
        //     {
        //         string site = reader.GetString(0);
        //         string telefone = reader.GetString(1); // 0 e 1 indices retornados --> INDICE DO SELECT
        //     }
        //     int resultado = inserirContato.ExecuteNonQuery();
        // }
    }
}
