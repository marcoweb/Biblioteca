using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MarcoCarvalho.Capacitacao.Biblioteca.Models;

namespace MarcoCarvalho.Capacitacao.Biblioteca.Repositories
{
    public class LocacaoRepository : BaseRepository
    {
        // Método que retorna uma lisa de objetos "Locacao"
        public List<Locacao> FetchAll()
        {
            // Cria a lista de objetos "Locacao"
            List<Locacao> locacoes = new List<Locacao>();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM locacoes";
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Varre os registros retornados da base de dados
                        while (dados.Read())
                        {
                            // Adiciona um novo "Locacao" à lista previamente criada com os dados do registro
                            locacoes.Add(new Locacao() { Id = dados.GetInt32("id"), IdLivro = dados.GetInt32("id_livro"), IdCliente = dados.GetInt32("id_cliente"), Retirada = dados.GetDateTime("retirada"), Devolucao = !dados.IsDBNull(dados.GetOrdinal("devolucao")) ? dados.GetDateTime("devolucao") : new DateTime()});
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança a exceção
                throw ex;
            }
            // Retorna a lista de objetos "Locacao"
            return locacoes;
        }

        // Método que retorna uma lisa de objetos "Locacao"
        public List<Locacao> FetchAllOpened()
        {
            // Cria a lista de objetos "Locacao"
            List<Locacao> locacoes = new List<Locacao>();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM locacoes WHERE devolucao IS NULL";
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Varre os registros retornados da base de dados
                        while (dados.Read())
                        {
                            // Adiciona um novo "Locacao" à lista previamente criada com os dados do registro
                            locacoes.Add(new Locacao() { Id = dados.GetInt32("id"), IdLivro = dados.GetInt32("id_livro"), IdCliente = dados.GetInt32("id_cliente"), Retirada = dados.GetDateTime("retirada") });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lança a exceção
                throw ex;
            }
            // Retorna a lista de objetos "Locacao"
            return locacoes;
        }

        // Método que retorna um objeto "Locacao" de acordo com o "id"
        public Locacao GetLocacaoById(int id)
        {
            // Cria o objeto a ser retornado
            Locacao locacao = new Locacao();
            try
            {
                // Declara a utilização do objeto de conexão garantindo o fechamento ao sair de escopo
                using (db)
                {
                    // Abre a conexão com a base de dados
                    this.db.Open();
                    // Cria um novo comando
                    MySqlCommand command = this.db.CreateCommand();
                    command.CommandText = "SELECT * FROM locacoes WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", id);
                    // Executa o comando na base de dados populando o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        // Seta os campos do objeto a ser retornado com os dados da consulta
                        dados.Read();
                        locacao.Id = dados.GetInt32("id");
                        locacao.IdCliente = dados.GetInt32("id_cliente");
                        locacao.IdLivro = dados.GetInt32("id_livro");
                        locacao.Retirada = dados.GetDateTime("retirada");
                        if(!dados.IsDBNull(dados.GetOrdinal("devolucao")))
                            locacao.Devolucao = dados.GetDateTime("devolucao");
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
            return locacao;
        }

        // Método que insere os dados do objeto "Locacao" na base de dados
        public Locacao Insert(Locacao locacao)
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
                    command.CommandText = (locacao.Id > 0) ?
                        "INSERT INTO locacoes VALUES(id, id_cliente, id_livro, retirada) (@id, @id_cliente, @id_livro, @retirada)" :
                        "INSERT INTO locacoes(id_livro, id_cliente, retirada) VALUES (@id_livro, @id_cliente, @retirada)";
                    // Adiciona os parâmetros ao comando a ser executado na base de dados
                    if (locacao.Id > 0)
                        command.Parameters.AddWithValue("@id", locacao.Id);
                    command.Parameters.AddWithValue("@id_livro", locacao.IdLivro);
                    command.Parameters.AddWithValue("@id_cliente", locacao.IdCliente);
                    command.Parameters.AddWithValue("@retirada", locacao.Retirada);
                    // Executa o comando na base de dados
                    command.ExecuteNonQuery();
                    // Caso o id não tenha sido informado, seta o id do objeto com o ultimo id adicionado
                    if (locacao.Id == 0)
                        locacao.Id = (int)command.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                // Lança exceção
                throw ex;
            }
            // Retorna o objeto
            return locacao;
        }

        // Método que atualiza o objeto na base de dados
        public Locacao Update(Locacao locacao)
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
                    command.CommandText = "SELECT * FROM locacoes WHERE id = @id";
                    // Adiciona o parâmetro id ao comando a ser executado na base de dados
                    command.Parameters.AddWithValue("@id", locacao.Id);
                    // Executa o comando na base de dados populado o DataReader
                    MySqlDataReader dados = command.ExecuteReader();
                    // Verifica se foi retornado algum registro
                    if (dados.HasRows)
                    {
                        dados.Close();
                        // Cria um novo comando
                        MySqlCommand updateCommand = this.db.CreateCommand();
                        updateCommand.CommandText = "UPDATE locacoes SET id_cliente = @id_cliente, id_livro = @id_livro, retirada = @retirada"+ (locacao.Devolucao != null && locacao.Devolucao != new DateTime() ? ", devolucao = @devolucao" : "") +" WHERE id = @id";
                        // Adiciona os parâmetros ao comando a ser executado na base de dados
                        updateCommand.Parameters.AddWithValue("@id", locacao.Id);
                        updateCommand.Parameters.AddWithValue("@id_cliente", locacao.IdCliente);
                        updateCommand.Parameters.AddWithValue("@id_livro", locacao.IdLivro);
                        updateCommand.Parameters.AddWithValue("@retirada", locacao.Retirada);
                        if(locacao.Devolucao != null && locacao.Devolucao != new DateTime())
                            updateCommand.Parameters.AddWithValue("@devolucao", locacao.Devolucao);
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
            return locacao;
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
                    command.CommandText = "SELECT * FROM locacoes WHERE id = @id";
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
                        deleteCommand.CommandText = "DELETE FROM locacoes WHERE id = @id";
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