using System;   // Necessário para : EventArgs, Array, Exception
using System.IO;    // Necessário para : StreamReader
using System.Collections.Generic;   // Necessário para : List
using System.Linq;  // Necessário para o mpetodo de extensão : Count [Array]
using MySql.Data.MySqlClient;   // Necessário para : MySqlConnection, MySqlCommand, MySqlDataReader
using System.Web.Configuration; // Necessário para : WebConfigurationManager

namespace MarcoCarvalho.Capacitacao.Biblioteca
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected string sqlCode;
        protected List<string> tables;
        protected string[] confirmTables = { "generos", "livros", "clientes", "locacoes" };
        
        // Método utilizado para Habilitar ou Desabilitar dos controles do formulário
        protected void enableDisableControls()
        {
            btnExecute.Enabled = !btnExecute.Enabled;
            btnGeneros.Enabled = !btnGeneros.Enabled;
            btnLivros.Enabled = !btnLivros.Enabled;
            btnClientes.Enabled = !btnClientes.Enabled;
            btnLocacoes.Enabled = !btnLocacoes.Enabled;
            btnExecute.CssClass = (btnExecute.CssClass.Equals("button")) ? "button disabled" : "button";
        }
        
        // Retorna as tabelas da base de dados
        protected List<string> getTables()
        {
            List<string> tables = new List<string>();
            // Obtem as configurações da conexão do arquivo web.config e monta a string de conexão
            string connectionString = "Server=" + WebConfigurationManager.AppSettings["dbHost"];
            connectionString += ";Database=" + WebConfigurationManager.AppSettings["dbName"];
            connectionString += ";user=" + WebConfigurationManager.AppSettings["dbUser"];
            connectionString += ";password=" + WebConfigurationManager.AppSettings["dbPassword"];
            // Instancia o objeto de conexão
            using (MySqlConnection db = new MySqlConnection(connectionString))
            {
                db.Open();
                MySqlCommand command = db.CreateCommand();
                command.CommandText = "SHOW TABLES";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
            }
            
            return tables;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var appDataPath = Server.MapPath("~/App_Data/");
            // Verifica se o diretório "App_Data" existe
            if (!Directory.Exists(appDataPath))
            {
                // Cria o diretório caso não exista
                Directory.CreateDirectory(appDataPath);
            }

            // Lê o arquivo com script SQL e armazena na propriedade sqlCode
            using (StreamReader sr = new StreamReader(Path.Combine(appDataPath, "create_database.sql")))
            {
                this.sqlCode = sr.ReadToEnd();
            }
            // Verifica se a base de dados e suas tabelas já foram criadas
            try
            {
                this.tables = this.getTables();
                int verificadas = 0;
                if (this.tables.Count > 0)
                {
                    foreach (var table in this.tables)
                        if (Array.Exists(this.confirmTables, element => element.Equals(table)))
                            verificadas++;
                }
                if (verificadas == confirmTables.Count())
                    lblMessage.Text = "Base de Dados Já Criada";
                else
                    this.enableDisableControls();
            }
            catch (Exception ex)
            {
                this.enableDisableControls();
            }
        }

        // Manipulador do evento Click do botão btnExecutar
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtem as configurações da conexão do arquivo web.config e monta a string de conexão
                string connectionString = "Server=" + WebConfigurationManager.AppSettings["dbHost"];
                connectionString += ";user=" + WebConfigurationManager.AppSettings["dbUser"];
                connectionString += ";password=" + WebConfigurationManager.AppSettings["dbPassword"];
                // Instancia o objeto de conexão
                using (MySqlConnection db = new MySqlConnection(connectionString))
                {
                    // Cria o banco de dados
                    db.Open();
                    MySqlCommand command = db.CreateCommand();
                    command.CommandText = "CREATE DATABASE " + WebConfigurationManager.AppSettings["dbName"];
                    command.ExecuteNonQuery();
                    connectionString += ";Database=" + WebConfigurationManager.AppSettings["dbName"];
                    using (MySqlConnection db2 = new MySqlConnection(connectionString))
                    {
                        // Executa o script SQL criando as Tabelas no banco de dados
                        db2.Open();
                        command = db2.CreateCommand();
                        command.CommandText = this.sqlCode;
                        command.ExecuteNonQuery();
                    }
                }
                lblMessage.Text = "Base de Dados Criada com Sucesso";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                this.enableDisableControls();
            }
        }
    }
}