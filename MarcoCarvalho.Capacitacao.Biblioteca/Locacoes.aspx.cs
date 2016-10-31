using System;   // Necessário para : EventArgs, Exception
using System.Collections.Generic;   // Necessário para : List
using System.Web.Services;  // Necessário para : WebMethod
using System.Web.UI.WebControls;    // Necessário para : ListItem
using MarcoCarvalho.Capacitacao.Biblioteca.Models;  // Necessário para : Locacao
using MarcoCarvalho.Capacitacao.Biblioteca.Repositories;    // Necessário para : LocacaoRepository

namespace MarcoCarvalho.Capacitacao.Biblioteca
{
    public partial class Locacoes : System.Web.UI.Page
    {
        // Define a propriedade estática que manipulará a base de dados
        protected static LocacaoRepository repository = new LocacaoRepository();
        protected static LivroRepository livroRepository = new LivroRepository();
        protected static ClienteRepository clienteRepository = new ClienteRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Pupula os DropDownLists cliente e livro 
            if (!IsPostBack)
            {
                try
                {
                    foreach (var item in clienteRepository.FetchAll())
                    {
                        cliente.Items.Insert(0, new ListItem(item.Nome, item.Id.ToString()));
                    }
                    foreach (var item in livroRepository.FetchAll())
                    {
                        livro.Items.Insert(0, new ListItem(item.Titulo, item.Id.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
                retirada.SelectedDate = DateTime.Today;
                retirada.VisibleDate = DateTime.Today;
            }
        }

        // Método que limpa os campos do formulário
        protected void cleanForm()
        {
            cliente.ClearSelection();
            livro.ClearSelection();
            retirada.SelectedDate = DateTime.Today;
            retirada.VisibleDate = DateTime.Today;
        }

        // Retorna as locações em aberto cadastradas na base de dados
        [WebMethod]
        public static List<Locacao> GetLocacoesEmAberto()
        {
            return repository.FetchAllOpened();
        }

        // Manipuladores de Evento

        protected void btnLimparForm_Click(object sender, EventArgs e)
        {
            this.cleanForm();
            //lblMessage.Text = string.Empty;
        }

        protected void btnAddLocacao_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            try
            {
                Locacao locacao = new Locacao() { IdCliente = int.Parse(cliente.SelectedValue), IdLivro = int.Parse(livro.SelectedValue), Retirada = retirada.SelectedDate.Date };
                if (devolucao.SelectedDate.Date != new DateTime().Date)
                    locacao.Devolucao = devolucao.SelectedDate;
                repository.Insert(locacao);
                this.cleanForm();
                lblMessage.Text = "Locação Adicionada com Sucesso. ID: " + locacao.Id;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnConsultarLocacao_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Locacao locacao = repository.GetLocacaoById(_id);
                    livro.SelectedIndex = livro.Items.IndexOf(livro.Items.FindByValue(locacao.IdLivro.ToString()));
                    cliente.SelectedIndex = cliente.Items.IndexOf(cliente.Items.FindByValue(locacao.IdCliente.ToString()));
                    retirada.SelectedDate = locacao.Retirada;
                    retirada.VisibleDate = retirada.SelectedDate;
                    if (locacao.Devolucao != null)
                    {
                        devolucao.SelectedDate = locacao.Devolucao;
                        devolucao.VisibleDate = devolucao.SelectedDate;
                    }
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

        protected void btnAtualizarLocacao_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    Locacao locacao = new Locacao() { Id = _id, IdCliente = int.Parse(cliente.SelectedValue), IdLivro = int.Parse(livro.SelectedValue), Retirada = retirada.SelectedDate };
                    if (devolucao.SelectedDate.Date != new DateTime().Date)
                        locacao.Devolucao = devolucao.SelectedDate;
                    repository.Update(locacao);
                    lblMessage.Text = "Locação Editada com Sucesso. ID: " + locacao.Id;
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

        protected void btnRemoverLocacao_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            int _id;
            if (int.TryParse(id.Text, out _id))
            {
                try
                {
                    repository.Delete(_id);
                    lblMessage.Text = "Locação Exluída com Sucesso";
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