using System;   // Necessário para : EventArgs, Exception
using System.Collections.Generic;   // Necessário para : List
using System.Web.Services;  // Necessário para : WebMethod
using System.Web.UI.WebControls;    // Necessário para : ListItem
using MarcoCarvalho.Capacitacao.Biblioteca.Models;  // Necessário para : Livro
using MarcoCarvalho.Capacitacao.Biblioteca.Repositories;    // Necessário para : LivroRepository

namespace MarcoCarvalho.Capacitacao.Biblioteca
{
    public partial class Livros : System.Web.UI.Page
    {
        // Define as propriedades estáticas que manipularão a base de dados
        protected static LivroRepository repository = new LivroRepository();
        protected static GeneroRepository generoRepository = new GeneroRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Popula o DropDownList genero
            if (!IsPostBack)
            {
                try
                {
                    foreach (var item in generoRepository.FetchAll())
                    {
                        genero.Items.Insert(0, new ListItem(item.Nome, item.Id.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

        // Método que limpa os campos do formulário
        protected void cleanForm()
        {
            id.Text = string.Empty;
            titulo.Text = string.Empty;
            genero.ClearSelection();
        }

        // Método estático que retorna os livros cadastrados na base de dados
        [WebMethod]
        public static List<Livro> GetLivros()
        {
            return repository.FetchAll();
        }

        // Manipuladores de Evento

        protected void btnLimparForm_Click(object sender, EventArgs e)
        {
            this.cleanForm();
            lblMessage.Text = string.Empty;
        }

        protected void btnAddLivro_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            try
            {
                int _id;
                Livro livro = repository.Insert(new Livro() { Id = int.TryParse(id.Text, out _id) ? _id : 0, Titulo = titulo.Text, IdGenero = int.Parse(genero.SelectedValue) });
                this.cleanForm();
                lblMessage.Text = "Livro Adicionado com Sucesso. ID: " + livro.Id;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnConsultarLivro_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Livro livro = repository.GetLivroById(_id);
                    titulo.Text = livro.Titulo;
                    genero.SelectedIndex = genero.Items.IndexOf(genero.Items.FindByValue(livro.IdGenero.ToString()));
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

        protected void btnAtualizarLivro_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Livro livro = repository.Update(new Livro() { Id = _id, Titulo = titulo.Text, IdGenero = int.Parse(genero.SelectedValue) });
                    lblMessage.Text = "Livro Editado com Sucesso. ID: " + livro.Id;
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

        protected void btnRemoverLivro_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    repository.Delete(_id);
                    lblMessage.Text = "Livro Exluído com Sucesso";
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