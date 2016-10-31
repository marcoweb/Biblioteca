using MySql.Data.MySqlClient;   // Namespace necessário para : MySQLConnection
using System.Configuration;
using System.Web.Configuration;     // Namespace necessário para : ConfigurationManager

namespace MarcoCarvalho.Capacitacao.Biblioteca.Repositories
{
    public abstract class BaseRepository
    {
        // Define a propriedade de conexão com a base de dados
        protected MySqlConnection db;
        
        public BaseRepository()
        {
            // Obtem as configurações da conexão do arquivo web.config e monta a string de conexão
            string connectionString = "Server=" + WebConfigurationManager.AppSettings["dbHost"];
            connectionString += ";Database=" + WebConfigurationManager.AppSettings["dbName"];
            connectionString += ";user=" + WebConfigurationManager.AppSettings["dbUser"];
            connectionString += ";password=" + WebConfigurationManager.AppSettings["dbPassword"];
            // Instancia o objeto de conexão
            this.db = new MySqlConnection(connectionString);
        }
    }
}