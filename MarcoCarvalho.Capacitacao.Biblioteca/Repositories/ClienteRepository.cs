using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MarcoCarvalho.Capacitacao.Biblioteca.Models;

namespace MarcoCarvalho.Capacitacao.Biblioteca.Repositories
{
    public class ClienteRepository : BaseRepository
    {
        // Método que retorna uma lisa de objetos "Cliente"
        public List<Cliente> FetchAll()
        {
            // Cria a lista de objetos "Cliente"
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM clientes";
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Varre os registros retornados da base de dados
                        while (dados.Read())
                        {
                            // Adiciona um novo "Cliente" à lista previamente criada com os dados do registro
                            clientes.Add(new Cliente() { Id = dados.GetInt32("id"), Nome = dados.GetString("nome") });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança a exceção
                throw ex;
            }
            // Retorna a lista de objetos "Cliente"
            return clientes;
        }

        // Método que retorna um objeto "Cliente" de acordo com o "id"
        public Cliente GetClienteById(int id)
        {
            // Cria o objeto a ser retornado
            Cliente cliente = new Cliente();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM clientes WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", id);
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Seta os campos do objeto a ser retornado com os dados da consulta
                        dados.Read();
                        cliente.Id = dados.GetInt32("id");
                        cliente.Nome = dados.GetString("nome");
                    }
                    else
                    {
                        // Lança exceção
                        throw new Exception("Id Inválido");
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança exceção
                throw ex;
            }
            // Retorna o resultado da consulta
            return cliente;
        }

        // Método que insere os dados do objeto "Cliente" na base de dados
        public Cliente Insert(Cliente cliente)
        {
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = (cliente.Id > 0) ?
                        "INSERT INTO clientes VALUES (@id, @nome)" :
                        "INSERT INTO clientes(nome) VALUES (@nome)";
                    // Adiciona os parâmetros ao comando a ser executado na base de dados
                    if (cliente.Id > 0)
                        command.Parameters.AddWithValue("@id", cliente.Id);
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    // Executa o comando na base de dados
                    command.ExecuteNonQuery();
                    // Caso o id não tenha sido informado, seta o id do objeto com o ultimo id adicionado
                    if (cliente.Id == 0)
                        cliente.Id = (int)command.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                // Lança exceção
                throw ex;
            }
            // Retorna o objeto
            return cliente;
        }

        // Método que atualiza o objeto na base de dados
        public Cliente Update(Cliente cliente)
        {
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM clientes WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", cliente.Id);
                    // Executa o comando na base de dados populado o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        dados.Close();
                        // Cria um novo comando
                        MySqlCommand updateCommand = this.db.CreateCommand();
                        updateCommand.CommandText = "UPDATE clientes SET nome = @nome WHERE id = @id";
                        // Adiciona os parâmetros ao comando a ser executado na base de dados
                        updateCommand.Parameters.AddWithValue("@id", cliente.Id);
                        updateCommand.Parameters.AddWithValue("@nome", cliente.Nome);
                        // Executa o comando na base de dados
                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // Lança exceção
                        throw new Exception("Id Inválido");
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança Exceção
                throw ex;
            }
            // Retorna o objeto atualizado
            return cliente;
        }

        // Método que remove o registro de acordo com o id
        public void Delete(int id)
        {
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM clientes WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", id);
                    // Executa o comando na base de dados populado o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        dados.Close();
                        // Cria um novo comando
                        MySqlCommand deleteCommand = this.db.CreateCommand();
                        deleteCommand.CommandText = "DELETE FROM clientes WHERE id = @id";
                        // Adiciona o parâmetro id ao comando a ser executado na base de dados
                        deleteCommand.Parameters.AddWithValue("@id", id);
                        // Executa o comando na base de dados
                        deleteCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // Lança exceção
                        throw new Exception("Id inválido");
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança exceção
                throw ex;
            }
        }
    }
}