﻿$(document).ready(function () {
    var maxRowNum = 100;

    var rowList = [];
    for (var i = 10; i <= maxRowNum; i += 10) {
        rowList.push(i);
    }

    $("#jqGridContasAPagar").jqGrid({
        url: '/ContasAPagar/GridData',
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Ações', 'Data Emissão', 'Data Vencimento', 'Favorecido', 'Valor', 'Forma Pagamento','Banco'],
        colModel: [
            {
                name: 'acoes',
                index: 'acoes',
                width: 120,
                sortable: false,
                formatter: function (cellvalue, options, rowObject) {
                    var editIcon, deleteIcon;
                    if (rowObject.contaBaixada) {
                        var baixadoIcon = '<span class="icon-baixado" title="Conta Baixada">&#10003;&nbsp;&nbsp;</span>';
                        editIcon = '<span class="icon-edit disabled-icon" title="Editar">&#9998;&nbsp;&nbsp;</span>';
                        deleteIcon = '<span class="icon-delete disabled-icon" title="Excluir">&#128465;&nbsp;&nbsp;</span>';
                        return editIcon + deleteIcon + baixadoIcon;
                    } else {
                        editIcon = '<span class="icon-edit" title="Editar" onclick="editarRegistro(' + rowObject.id + ')">&#9998;&nbsp;&nbsp;</span>';
                        deleteIcon = '<span class="icon-delete" title="Excluir" onclick="excluirRegistro(' + rowObject.id + ')">&#128465;&nbsp;&nbsp;</span>';
                        var baixarIcon = '<button class="btn btn-success" title="Baixar" onclick="baixarConta(' + rowObject.id + ')"><i class="fa fa-download"></i></button>';
                        return editIcon + deleteIcon + baixarIcon;
                    }
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
        caption: 'Lista de Contas a Pagar',
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

var contasAPagarId;

function editarRegistro(id) {
    $('#editarModal').modal('show');
    contasAPagarId = id;
    $.ajax({
        url: '/ContasAPagar/GetDataForEdit',
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
                console.error('Contas a Pagar não encontrada.');
            }
        },
        error: function (error) {
            console.error('Erro ao obter dados para edição: ' + error.responseText);
        }
    });
}

function salvarEdicao() {
    $.ajax({
        url: '/ContasAPagar/Update',
        type: 'POST',
        data: {
            id: contasAPagarId,
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
        url: '/ContasAPagar/Delete',
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
    $("#jqGridContasAPagar").trigger("reloadGrid");
}

function baixarConta(contasAPagarId) {
    $.ajax({
        url: '/ContasAPagar/BaixarConta', // Rota para a action que baixa a conta
        type: 'POST',
        data: { id: contasAPagarId }, // Envia o ID da conta a receber
        success: function (response) {
            if (response.success) {
                alert('Conta baixada com sucesso.');
                // Atualizar a interface para refletir que a conta foi baixada
                var rowId = response.contasAPagarId; // Obtém o ID da conta baixada da resposta
                var rowData = response.rowData; // Obtém os dados atualizados da linha
                $("#jqGridContasAPagar").jqGrid('setRowData', rowId, rowData);
            } else {
                alert('Erro ao baixar conta: ' + response.message);
            }
            carregarDadosGrid();
        },
        error: function (error) {
            console.error('Erro ao baixar conta: ' + error.responseText);
        }
    });
}
