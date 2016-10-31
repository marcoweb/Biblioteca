<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="MarcoCarvalho.Capacitacao.Biblioteca.Clientes" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_menu" runat="server">
    <div class="top-bar callout">
        <div class="top-bar-left">
            <ul class="menu">
                <li><asp:LinkButton ID="btnAddCliente" Text="Adicionar Cliente" runat="server" OnClick="btnAddCliente_Click"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnConsultarCliente" Text="Consultar Cliente" runat="server" OnClick="btnConsultarCliente_Click"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnAtualizarCliente" Text="Atualizar Cliente" runat="server" OnClick="btnAtualizarCliente_Click"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnRemoverCliente" Text="Remover Cliente" runat="server" OnClick="btnRemoverCliente_Click"></asp:LinkButton></li>
            </ul>
        </div>
        <div class="top-bar-right">
            <ul class="menu">
                <li><asp:LinkButton ID="btnLimparForm" Text="Limpar Formulário" runat="server" OnClick="btnLimparForm_Click"></asp:LinkButton></li>
                <li class="menu-text">Cadastros:</li>
                <li><asp:LinkButton ID="btnGeneros" Text="Gêneros" PostBackUrl="~/Generos.aspx" runat="server"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnLivros" Text="Livros" PostBackUrl="~/Livros.aspx" runat="server"></asp:LinkButton></li>
                <li class="active"><asp:LinkButton ID="btnClientes" Text="Clientes" PostBackUrl="~/Clientes.aspx" runat="server"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnLocacoes" Text="Locações" PostBackUrl="~/Locacoes.aspx" runat="server"></asp:LinkButton></li>
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
                <div class="large-10 columns">
                    <label>Nome
                        <asp:TextBox ID="nome" runat="server"></asp:TextBox>
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
                        <th>Nome</th>
                    </tr>
                </thead>
                <tbody id="table_body">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cp_script" runat="server">
    <script src="Scripts/clientes.js"></script>
</asp:Content>
