function loadTable() {
    $.ajax({
        type: "POST",
        url: "Clientes.aspx/GetClientes",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#table_body").empty();
            $(data.d).each(function () {
                var row = "<tr><td><a onclick='setId(" + this.Id + ")'>" + this.Id + "</a></td><td>" + this.Nome + "</td></tr>";
                $("#table_body").append(row);
            });
        }
    });
}
function setId(id) {
    $("#cp_principal_id").val(id);
}
$(document).ready(function () {
    loadTable();
});