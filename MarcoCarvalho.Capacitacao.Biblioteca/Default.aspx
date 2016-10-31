<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MarcoCarvalho.Capacitacao.Biblioteca.Default1" %>
<asp:Content ID="cp_style" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/highlight.js/9.7.0/styles/default.min.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_menu" runat="server">
    <div class="top-bar callout">
        <div class="top-bar-right">
            <ul class="menu">
                <li class="menu-text">Cadastros:</li>
                <li>
                    <asp:LinkButton ID="btnGeneros" Text="Gêneros" PostBackUrl="~/Generos.aspx" runat="server"></asp:LinkButton>
                </li>
                <li>
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
        <div class="large-12 columns">
            <p>Aplicativo simples de biblioteca, criado como projeto final para a capacitação de ASP.Net Intermediário. Para executar as rotinas de cadastro deve-se primeiro criar a base de dados seguindo o procedimento a seguir:</p>
        </div>
    </div>
    <div class="row">
        <div class="large-6 columns">
            <h3>Base de dados</h3>
            <p>Para executar automaticamente o script de geração da base de dados, certifique-se de ter editado as configurações no arquivo "Web.config" e clique no botão abaixo:</p>
            <div class="row">
                <div class="large-12 columns text-center">
                    <asp:LinkButton Enabled="false" ID="btnExecute" Text="Criar base de dados" CssClass="button disabled" runat="server" OnClick="btnExecute_Click"></asp:LinkButton>
                </div>
            </div>
            <p class="callout">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </p>
        </div>
        <div class="large-6 columns">
            <h3>Script a ser executado na base de dados</h3>
            <pre>
                <code><%= sqlCode %></code>
            </pre>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cp_script" runat="server">
    <script src="//cdnjs.cloudflare.com/ajax/libs/highlight.js/9.7.0/highlight.min.js"></script>
    <script src="Scripts/default.js"></script>
</asp:Content>
