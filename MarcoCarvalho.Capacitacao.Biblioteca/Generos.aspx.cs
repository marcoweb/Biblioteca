using System;   // Necessário para : EventArgs, Exception
using System.Collections.Generic;   // Necessário para : List
using System.Web.Services;  // Necessário para : WebMethod
using MarcoCarvalho.Capacitacao.Biblioteca.Models; // Necessário para : Genero
using MarcoCarvalho.Capacitacao.Biblioteca.Repositories;    // Necessário para : GeneroRepository

namespace MarcoCarvalho.Capacitacao.Biblioteca
{
    public partial class Generos : System.Web.UI.Page
    {
        // Define a propriedade estática que manipulará a base de dados
        protected static GeneroRepository repository = new GeneroRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Método que limpa os campos do formulário
        protected void cleanForm()
        {
            id.Text = string.Empty;
            nome.Text = string.Empty;
        }
        // Método estático que retorna os gêneros cadastrados
        [WebMethod]
        public static List<Genero> GetGeneros()
        {
            return repository.FetchAll();
        }

        // Manipuladores de Evento

        protected void btnLimparForm_Click(object sender, EventArgs e)
        {
            this.cleanForm();
            lblMessage.Text = string.Empty;
        }

        protected void btnAddGenero_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            try
            {
                int _id;
                Genero genero = repository.Insert(new Genero() { Id = int.TryParse(id.Text, out _id) ? _id : 0, Nome = nome.Text });
                this.cleanForm();
                lblMessage.Text = "Gênero Adicionado com Sucesso. ID: " + genero.Id;
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnConsultarGenero_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Genero genero = repository.GetGeneroById(_id);
                    nome.Text = genero.Nome;
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

        protected void btnAtualizarGenero_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Genero genero = repository.Update(new Genero() { Id = _id, Nome = nome.Text });
                    lblMessage.Text = "Gênero Editado com Sucesso. ID: " + genero.Id;
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

        protected void btnRemoverGenero_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                   repository.Delete(_id);
                   lblMessage.Text = "Gênero Exluído com Sucesso";
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