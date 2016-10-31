<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Locacoes.aspx.cs" Inherits="MarcoCarvalho.Capacitacao.Biblioteca.Locacoes" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_menu" runat="server">
    <div class="top-bar callout">
        <div class="top-bar-left">
            <ul class="menu">
                <li><asp:LinkButton ID="btnAddLocacao" Text="Adicionar Locação" runat="server" OnClick="btnAddLocacao_Click"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnConsultarLocacao" Text="Consultar Locação" runat="server" OnClick="btnConsultarLocacao_Click"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnAtualizarLocacao" Text="Atualizar Locação" runat="server" OnClick="btnAtualizarLocacao_Click"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnRemoverLocacao" Text="Remover Locação" runat="server" OnClick="btnRemoverLocacao_Click"></asp:LinkButton></li>
            </ul>
        </div>
        <div class="top-bar-right">
            <ul class="menu">
                <li><asp:LinkButton ID="btnLimparForm" Text="Limpar Formulário" runat="server" OnClick="btnLimparForm_Click"></asp:LinkButton></li>
                <li class="menu-text">Cadastros:</li>
                <li><asp:LinkButton ID="btnGeneros" Text="Gêneros" PostBackUrl="~/Generos.aspx" runat="server"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnLivros" Text="Livros" PostBackUrl="~/Livros.aspx" runat="server"></asp:LinkButton></li>
                <li><asp:LinkButton ID="btnClientes" Text="Clientes" PostBackUrl="~/Clientes.aspx" runat="server"></asp:LinkButton></li>
                <li class="active"><asp:LinkButton ID="btnLocacoes" Text="Locações" PostBackUrl="~/Locacoes.aspx" runat="server"></asp:LinkButton></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cp_principal" runat="server">
    <div class="row">
        <div class="large-12 column text-center">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="large-4 column">
            <div class="row">
                <div class="large-4 columns">
                    <label>Id
                        <asp:TextBox ID="id" runat="server"></asp:TextBox>
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label>Cliente
                        <asp:DropDownList ID="cliente" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label>Livro
                        <asp:DropDownList ID="livro" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label>Retirada
                        <asp:Calendar ID="retirada" runat="server"></asp:Calendar>
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label>Devolução
                        <asp:Calendar ID="devolucao" runat="server"></asp:Calendar>
                    </label>
                </div>
            </div>
        </div>
        <div class="large-8 column">
            <div class="row">
                <div class="large-12 column">
                    <h3>Locações em Aberto</h3>
                </div>
            </div>
            <div class="row">
                <div class="large-12 column">
                    <table>
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Cliente</th>
                                <th>Livro</th>
                            </tr>
                        </thead>
                        <tbody id="table_body">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cp_script" runat="server">
    <script src="Scripts/locacoes.js"></script>
</asp:Content>
