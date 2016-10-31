<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Livros.aspx.cs" Inherits="MarcoCarvalho.Capacitacao.Biblioteca.Livros" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_menu" runat="server">
    <div class="top-bar callout">
        <div class="top-bar-left">
            <ul class="menu">
                <li>
                    <asp:LinkButton ID="btnAddLivro" Text="Adicionar Livro" runat="server" OnClick="btnAddLivro_Click"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnConsultarLivro" Text="Consultar Livro" runat="server" OnClick="btnConsultarLivro_Click"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnAtualizarLivro" Text="Atualizar Livro" runat="server" OnClick="btnAtualizarLivro_Click"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnRemoverLivro" Text="Remover Livro" runat="server" OnClick="btnRemoverLivro_Click"></asp:LinkButton>
                </li>
            </ul>
        </div>
        <div class="top-bar-right">
            <ul class="menu">
                <li>
                    <asp:LinkButton ID="btnLimparForm" Text="Limpar Formulário" runat="server" OnClick="btnLimparForm_Click"></asp:LinkButton>
                </li>
                <li class="menu-text">Cadastros:</li>
                <li>
                    <asp:LinkButton ID="btnGeneros" Text="Gêneros" PostBackUrl="~/Generos.aspx" runat="server"></asp:LinkButton>
                </li>
                <li class="active">
                    <asp:LinkButton ID="btnLivros" Text="Livros" PostBackUrl="~/Livros.aspx" runat="server"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnClientes" Text="Clientes" PostBackUrl="~/Clientes.aspx" runat="server"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="btnLocacoes" Text="Locações" PostBackUrl="~/Locacoes.aspx" runat="server"></asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cp_principal" runat="server">
    <div class="row">
        <div class="large-12 column">
            <div class="row">
                <div class="large-12 text-center">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="large-2 columns">
                    <label>Id
                        <asp:TextBox ID="id" runat="server"></asp:TextBox>
                    </label>
                </div>
                <div class="large-6 columns">
                    <label>Título
                        <asp:TextBox ID="titulo" runat="server"></asp:TextBox>
                    </label>
                </div>
                <div class="large-4 columns">
                    <label>Gênero
                        <asp:DropDownList ID="genero" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </label>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="large-12 column">
            <table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Título</th>
                    </tr>
                </thead>
                <tbody id="table_body">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cp_script" runat="server">
    <script src="Scripts/livros.js"></script>
</asp:Content>
