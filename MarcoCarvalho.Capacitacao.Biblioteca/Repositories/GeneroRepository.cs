using System;       // Namespace necessário para : Exception
using System.Collections.Generic;   // Namespace necessário para : List
using MySql.Data.MySqlClient;       // Namespace necessário para : MySqlCommand, MySqlCommandDataReader
using MarcoCarvalho.Capacitacao.Biblioteca.Models;  // Namespace necessário para : Genero

namespace MarcoCarvalho.Capacitacao.Biblioteca.Repositories
{
    public class GeneroRepository : BaseRepository
    {
        // Método que retorna uma lisa de objetos "Genero"
        public List<Genero> FetchAll()
        {
            // Cria a lista de objetos "Genero"
            List<Genero> generos = new List<Genero>();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM generos";
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Varre os registros retornados da base de dados
                        while (dados.Read())
                        {
                            // Adiciona um novo "Genero" à lista previamente criada com os dados do registro
                            generos.Add(new Genero() { Id = dados.GetInt32("id"), Nome = dados.GetString("nome") });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança a exceção
                throw ex;
            }
            // Retorna a lista de objetos "Genero"
            return generos;
        }

        // Método que retorna um objeto "Genero" de acordo com o "id"
        public Genero GetGeneroById(int id)
        {
            // Cria o objeto a ser retornado
            Genero genero = new Genero();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM generos WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", id);
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Seta os campos do objeto a ser retornado com os dados da consulta
                        dados.Read();
                        genero.Id = dados.GetInt32("id");
                        genero.Nome = dados.GetString("nome");
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
            return genero;
        }

        // Método que insere os dados do objeto "Genero" na base de dados
        public Genero Insert(Genero genero)
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
                    command.CommandText = (genero.Id > 0) ?
                        "INSERT INTO generos VALUES (@id, @nome)" :
                        "INSERT INTO generos(nome) VALUES (@nome)";
                    // Adiciona os parâmetros ao comando a ser executado na base de dados
                    if (genero.Id > 0)
                        command.Parameters.AddWithValue("@id", genero.Id);
                    command.Parameters.AddWithValue("@nome", genero.Nome);
                    // Executa o comando na base de dados
                    command.ExecuteNonQuery();
                    // Caso o id não tenha sido informado, seta o id do objeto com o ultimo id adicionado
                    if (genero.Id == 0)
                        genero.Id = (int)command.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                // Lança exceção
                throw ex;
            }
            // Retorna o objeto
            return genero;
        }

        // Método que atualiza o objeto na base de dados
        public Genero Update(Genero genero)
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
                    command.CommandText = "SELECT * FROM generos WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", genero.Id);
                    // Executa o comando na base de dados populado o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        dados.Close();
                        // Cria um novo comando
                        MySqlCommand updateCommand = this.db.CreateCommand();
                        updateCommand.CommandText = "UPDATE generos SET nome = @nome WHERE id = @id";
                        // Adiciona os parâmetros ao comando a ser executado na base de dados
                        updateCommand.Parameters.AddWithValue("@id", genero.Id);
                        updateCommand.Parameters.AddWithValue("@nome", genero.Nome);
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
            return genero;
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
                    command.CommandText = "SELECT * FROM generos WHERE id = @id";
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
                        deleteCommand.CommandText = "DELETE FROM generos WHERE id = @id";
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