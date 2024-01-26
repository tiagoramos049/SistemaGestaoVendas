﻿$(document).ready(function () {
    var maxRowNum = 100;

    var rowList = [];
    for (var i = 10; i <= maxRowNum; i += 10) {
        rowList.push(i);
    }

    $("#jqGridContasAReceber").jqGrid({
        url: '/ContasAReceber/GridData',
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Ações', 'Data Emissão', 'Data Vencimento', 'Favorecido', 'Valor', 'Forma Pagamento','Banco'],
        colModel: [
            {
                name: 'acoes',
                index: 'acoes',
                width: 90,
                sortable: false,
                formatter: function (cellvalue, options, rowObject) {
                    var editIcon = '<span class="icon-edit" title="Editar" onclick="editarRegistro(' + rowObject.id + ')">&#9998;&nbsp;&nbsp;</span>';
                    var deleteIcon = '<span class="icon-delete" title="Excluir" onclick="excluirRegistro(' + rowObject.id + ')">&#128465;&nbsp;&nbsp</span>';
                    return editIcon + deleteIcon;
                }
            },
            { name: 'dataEmissao', index: 'dataEmissao', width: 250 },
            { name: 'dataVencimento', index: 'dataVencimento', width: 250 },
            { name: 'favorecido', index: 'favorecido', width: 100 },
            { name: 'valor', index: 'valor', width: 100 },
            { name: 'formaPagamento', index: 'formaPagamento', width: 150 },
            { name: 'banco', index: 'banco', width: 150 },
        ],

        pager: jQuery('#jqGridPager'),
        rowNum: 10,
        rowList: rowList,
        caption: 'Lista de Contas a Receber',
        pgbuttons: true,
        pginput: true,
        pgtext: "Página {0} de {1}",
        loadonce: false,
        jsonReader: { repeatitems: false },
        serializeGridData: function (postData) {
            return { page: postData.page, rows: postData.rows, sort: postData.sort, order: postData.order };
        }

    });
});

var contasAReceberId;

function editarRegistro(id) {
    $('#editarModal').modal('show');
    contasAReceberId = id;
    $.ajax({
        url: '/ContasAReceber/GetDataForEdit',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            if (data.success) {
                $('#DataEmissao').val(data.dataEmissao);
                $('#DataVencimento').val(data.dataVencimento);
                $('#Favorecido').val(data.favorecido);
                $('#Valor').val(data.valor);
                $('#FormaPagamento').val(data.formaPagamento);
                $('#Banco').val(data.banco);
            } else {
                console.error('Contas a Receber não encontrado.');
            }
        },
        error: function (error) {
            console.error('Erro ao obter dados para edição: ' + error.responseText);
        }
    });
}

function salvarEdicao() {
    $.ajax({
        url: '/ContasAReceber/Update',
        type: 'POST',
        data: {
            id: contasAReceberId,
            campo1: $('#dataEmissao').val(),
            campo2: $('#dataVencimento').val(),
            campo3: $('#favorecido').val(),
            campo4: $('#valor').val(),
            campo5: $('#formaPagamento').val(),
            campo6: $('#banco').val()
        },
        success: function (response) {
            alert('Registro atualizado com sucesso.');
            $('#editarModal').modal('hide');
            carregarDadosGrid();
        },
        error: function (error) {
            console.error('Erro ao atualizar registro: ' + error.responseText);
        }
    });
}

function excluirRegistro(id) {
    $.ajax({
        url: '/ContasAReceber/Delete',
        type: 'POST',
        data: { id: id },
        success: function (response) {
            alert('Registro excluído com sucesso.');
            carregarDadosGrid();
        },
        error: function (error) {
            alert('Erro ao excluir registro: ' + error.responseText);
        }
    });
}

$(document).ready(function () {
    $('#produtoForm').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            success: function (response) {
                $('#mensagem').html('<div class="alert alert-success">Produto salvo com sucesso!</div>');
            },
            error: function (error) {
                $('#mensagem').html('<div class="alert alert-danger">Erro ao salvar o produto.</div>');
            }
        });
    });
});

function fecharModal() {
    $('#editarModal').modal('hide');
}

function carregarDadosGrid() {
    $("#jqGridContasAReceber").trigger("reloadGrid");
}