using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.ImportadorXML
{
    public class Restaurantes
    {
        private string ConnectionString { get; set; }
        
        // readonly static string INSERT_SQL = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES (@Site, @Telefone); SELECT @@IDENTITY";
        // readonly static string SELECT_SQL = "SELECT [Site],[Telefone] FROM [dbo].[Contato] (nolock)";

        public Restaurantes(string _connectionString)
        {
            ConnectionString = _connectionString;
        }
    }
}
