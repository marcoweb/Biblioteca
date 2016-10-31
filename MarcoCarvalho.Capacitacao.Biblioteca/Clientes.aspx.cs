using System;   // Necessário para : EventArgs, Exception
using System.Collections.Generic;   // Necessário para : List
using System.Web.Services;  // Necessário para : WebMethod
using MarcoCarvalho.Capacitacao.Biblioteca.Models;  // Necessário para : Cliente
using MarcoCarvalho.Capacitacao.Biblioteca.Repositories;    // Necessário para : ClienteRepository

namespace MarcoCarvalho.Capacitacao.Biblioteca
{
    public partial class Clientes : System.Web.UI.Page
    {
        // Define a propriedade estática que manipulará a base de dados
        protected static ClienteRepository repository = new ClienteRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Método que limpa os campos do formulário
        protected void cleanForm()
        {
            id.Text = string.Empty;
            nome.Text = string.Empty;
        }

        // Método estático que retorna os clientes cadastrados na base de dados
        [WebMethod]
        public static List<Cliente> GetClientes()
        {
            return repository.FetchAll();
        }

        // Manipuladores de Evento

        protected void btnLimparForm_Click(object sender, EventArgs e)
        {
            this.cleanForm();
            lblMessage.Text = string.Empty;
        }

        protected void btnAddCliente_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            try
            {
                int _id;
                Cliente cliente = repository.Insert(new Cliente() { Id = int.TryParse(id.Text, out _id) ? _id : 0, Nome = nome.Text });
                this.cleanForm();
                lblMessage.Text = "Cliente Adicionado com Sucesso. ID: " + cliente.Id;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnConsultarCliente_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Cliente cliente = repository.GetClienteById(_id);
                    nome.Text = cliente.Nome;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Id inválido";
            }
        }

        protected void btnAtualizarCliente_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Cliente cliente = repository.Update(new Cliente() { Id = _id, Nome = nome.Text });
                    lblMessage.Text = "Gênero Editado com Sucesso. ID: " + cliente.Id;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Id inválido";
            }
        }

        protected void btnRemoverCliente_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    repository.Delete(_id);
                    lblMessage.Text = "Cliente Exluído com Sucesso";
                    this.cleanForm();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Id inválido";
            }
        }
    }
}