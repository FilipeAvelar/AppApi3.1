function Pesquisar()
{
    $.ajax({
        url: "/Cliente/Listar",
        type: 'POST',
        async: false,
        data: { tipoPessoa: $("#tipoPessoaFiltro").val() },
        success: function (retorno) {
            $("#divPesquisaClientes").html(retorno);
        }, error: function () {
            alert('Erro inesperado ao consultar clientes.');
        }
    });
}

function Remover(id) {
    $.ajax({
        url: "/Cliente/Excluir",
        type: 'POST',
        async: false,
        data: { ID : id },
        success: function (retorno) {
            $("#divPesquisaClientes").html(retorno);
        }, error: function (err) {
            alert('Erro inesperado ao Excluir clientes.');
        }
    });
}