function loadTable() {
    $.ajax({
        type: "POST",
        url: "Locacoes.aspx/GetLocacoesEmAberto",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#table_body").empty();
            $(data.d).each(function () {
                var row = "<tr><td><a onclick='setId(" + this.Id + ")'>" + this.Id + "</a></td><td>" + this.Cliente.Nome + "</td><td>" + this.Livro.Titulo + "</td></tr>";
                $("#table_body").append(row);
            });
        },
        error: function () {
            alert("erro");
        }
    });
}
function setId(id) {
    $("#cp_principal_id").val(id);
}
$(document).ready(function () {
    loadTable();
});