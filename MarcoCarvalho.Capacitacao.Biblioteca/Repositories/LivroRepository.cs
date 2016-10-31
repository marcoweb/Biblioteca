using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MarcoCarvalho.Capacitacao.Biblioteca.Models;

namespace MarcoCarvalho.Capacitacao.Biblioteca.Repositories
{
    public class LivroRepository : BaseRepository
    {
        // Método que retorna uma lisa de objetos "Livro"
        public List<Livro> FetchAll()
        {
            // Cria a lista de objetos "Livro"
            List<Livro> livros = new List<Livro>();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM livros";
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Varre os registros retornados da base de dados
                        while (dados.Read())
                        {
                            // Adiciona um novo "Livro" à lista previamente criada com os dados do registro
                            livros.Add(new Livro() { Id = dados.GetInt32("id"), Titulo = dados.GetString("titulo"), IdGenero = dados.GetInt32("id_genero") });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança a exceção
                throw ex;
            }
            // Retorna a lista de objetos "Livro"
            return livros;
        }

        // Método que retorna um objeto "Livro" de acordo com o "id"
        public Livro GetLivroById(int id)
        {
            // Cria o objeto a ser retornado
            Livro livro = new Livro();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM livros WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", id);
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Seta os campos do objeto a ser retornado com os dados da consulta
                        dados.Read();
                        livro.Id = dados.GetInt32("id");
                        livro.Titulo = dados.GetString("titulo");
                        livro.IdGenero = dados.GetInt32("id_genero");
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
            return livro;
        }

        // Método que insere os dados do objeto "Livro" na base de dados
        public Livro Insert(Livro livro)
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
                    command.CommandText = (livro.Id > 0) ?
                        "INSERT INTO livros VALUES (@id, @titulo, @id_genero)" :
                        "INSERT INTO livros(titulo, id_genero) VALUES (@titulo, @id_genero)";
                    // Adiciona os parâmetros ao comando a ser executado na base de dados
                    if (livro.Id > 0)
                        command.Parameters.AddWithValue("@id", livro.Id);
                    command.Parameters.AddWithValue("@titulo", livro.Titulo);
                    command.Parameters.AddWithValue("@id_genero", livro.IdGenero);
                    // Executa o comando na base de dados
                    command.ExecuteNonQuery();
                    // Caso o id não tenha sido informado, seta o id do objeto com o ultimo id adicionado
                    if (livro.Id == 0)
                        livro.Id = (int)command.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                // Lança exceção
                throw ex;
            }
            // Retorna o objeto
            return livro;
        }

        // Método que atualiza o objeto na base de dados
        public Livro Update(Livro livro)
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
                    command.CommandText = "SELECT * FROM livros WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", livro.Id);
                    // Executa o comando na base de dados populado o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        dados.Close();
                        // Cria um novo comando
                        MySqlCommand updateCommand = this.db.CreateCommand();
                        updateCommand.CommandText = "UPDATE livros SET titulo = @titulo, id_genero = @id_genero WHERE id = @id";
                        // Adiciona os parâmetros ao comando a ser executado na base de dados
                        updateCommand.Parameters.AddWithValue("@id", livro.Id);
                        updateCommand.Parameters.AddWithValue("@titulo", livro.Titulo);
                        updateCommand.Parameters.AddWithValue("@id_genero", livro.IdGenero);
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
            return livro;
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
                    command.CommandText = "SELECT * FROM livros WHERE id = @id";
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
                        deleteCommand.CommandText = "DELETE FROM livros WHERE id = @id";
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