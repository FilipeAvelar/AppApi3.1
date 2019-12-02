const TIPO_PESSOA_JURIDICA = "Juridica";

$(document).ready(function () {
    AlterarCadastroPessoa($("#tipoPessoa").val());
    $("#CEP").mask("99.999-999");
    $("#PessoaDTO_CPF").mask("999.999.999-99");
    $("#PessoaDTO_CNPJ").mask("99.999.999/9999-99");
});

$("#tipoPessoa").change(function () {
    var tipoPessoa = $(this).val();
    AlterarCadastroPessoa(tipoPessoa);
});

function AlterarCadastroPessoa(tipoPessoa) {

    if (tipoPessoa == TIPO_PESSOA_JURIDICA) {
        $("#divCriarPessoaJuridica").removeClass("collapse");
        $("#divCriarPessoaFisica").addClass("collapse");
    } else {
        $("#divCriarPessoaJuridica").addClass("collapse");
        $("#divCriarPessoaFisica").removeClass("collapse");
    }
}

function AdicionarContato() {
    var indice = $("#tabelaContatos tr").length - 1;

    var linhaContato = '<tr>'
        + "<td asp-for=Contatos_" + indice + "__ID>"
        + '</td>'
        + "<td>"
        + "<input id=Contatos_" + indice + "__Telefone asp-for=Contatos_" + indice + "__Telefone name='Contatos[" + indice + "].Telefone' contenteditable='true' onkeypress='IncluirMascara(" + indice + ")' /> "
        + '</td>'
        + "<td>"
        + "<button type='button' class='btn btn-primary btnDelete' onClick='RemoverContato()'>Excluir</button>"
        + "</td>"
        + '</tr>';

    $("#tabelaContatos").append(linhaContato);
}

function RemoverContato()
{
    $("#tabelaContatos").on('click', '.btnDelete', function () {
        $(this).closest('tr').remove();
    });
}

function IncluirMascara(indice) {
    
    $("#Contatos_" + indice + "__Telefone").mask("(99)9999-9999").trigger('input');
}