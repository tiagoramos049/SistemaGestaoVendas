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
        colNames: ['Ações', 'Data Emissão', 'Data Vencimento', 'Favorecido', 'Valor', 'Forma Pagamento', 'Banco', 'Centro De Custo', 'Categoria', 'Projeto', 'Numero Nota Fiscal', 'Valor Pago Nota Fiscal', 'Juros/Multa', 'Desconto', 'Código de Barra'],
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
                        var voltarIcon = '<span class="icon-voltar icon-borda" title="Reabrir Conta" onclick="reabrirConta(' + rowObject.id + ')">&nbsp;&nbsp;&#xd7;</span>';
                        editIcon = '<span class="icon-edit disabled-icon" title="Editar">&#9998;&nbsp;&nbsp;</span>';
                        deleteIcon = '<span class="icon-delete disabled-icon" title="Excluir">&#128465;&nbsp;&nbsp;</span>';
                        return editIcon + deleteIcon + baixadoIcon + voltarIcon;
                    } else {
                        editIcon = '<span class="icon-edit" title="Editar" onclick="editarRegistro(' + rowObject.id + ')">&#9998;&nbsp;&nbsp;</span>';
                        deleteIcon = '<span class="icon-delete" title="Excluir" onclick="excluirRegistro(' + rowObject.id + ')">&#128465;&nbsp;&nbsp;</span>';
                        var baixarIcon = '<button class="btn btn-success" title="Baixar" onclick="baixarConta(' + rowObject.id + ')"><span class="icon-redondo-verde">&#10003;</span></button>';
                        return editIcon + deleteIcon + baixarIcon;
                    }
                }
            },
            {
                name: 'dataEmissao',
                index: 'dataEmissao',
                width: 100,
                formatter: 'date',
                formatoptions: {
                    srcformat: 'Y-m-d',
                    newformat: 'd/m/Y'
                }
            },
            {
                name: 'dataVencimento',
                index: 'dataVencimento',
                width: 100,
                formatter: 'date',
                formatoptions: {
                    srcformat: 'Y-m-d',
                    newformat: 'd/m/Y'
                }
            },
            { name: 'favorecido', index: 'favorecido', width: 100 },
            {
                name: 'valor',
                index: 'valor',
                width: 70,
                formatter: 'currency',
                formatoptions: { prefix: 'R$ ', thousandsSeparator: '.', decimalPlaces: 2 }
            },
            { name: 'formaPagamento', index: 'formaPagamento', width: 100 },
            { name: 'banco', index: 'banco', width: 60 },

            { name: 'centroDeCusto', index: 'centroDeCusto', width: 70 },
            { name: 'categoria', index: 'categoria', width: 50 },
            { name: 'projeto', index: 'projeto', width: 50 },
            { name: 'numeroNotaFiscal', index: 'numeroNotaFiscal', width: 50 },
            { name: 'valorPagoNotaFiscal', index: 'valorPagoNotaFiscal', width: 80 },
            { name: 'jurosMulta', index: 'jurosMulta', width: 50 },
            { name: 'desconto', index: 'desconto', width: 50 },
            { name: 'codigoBarra', index: 'codigoBarra', width: 50 },
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

function crirRegistro() {
    $('#criarModal').modal('show');
    $.ajax({
        url: '/ContasAReceber/DetalhesConta',
        type: 'GET',
        success: function (data) {
            if (data.success) {
                $('#DataEmissao').val(data.dataEmissao);
                $('#DataVencimento').val(data.dataVencimento);
                $('#Favorecido').val(data.favorecido);
                $('#Valor').val(data.valor);
                $('#FormaPagamento').val(data.formaPagamento);
                $('#Banco').val(data.banco);
                $('CentroDeCusto').val(data.centroDeCusto);
                $('Categoria').val(data.categoria);
                $('Projeto').val(data.projeto);
                $('NumeroNotaFiscal').val(data.numeroNotaFiscal);
                $('ValorPagoNotaFiscal').val(data.valorPagoNotaFiscal);
                $('JurosMulta').val(data.jurosMulta);
                $('Desconto').val(data.desconto);
                $('CodigoBarra').val(data.codigoBarra);

            }
        },
        error: function (xhr, status, error) {
            console.error('Erro na requisição AJAX:', error);
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

function baixarConta(contasAReceberId) {
    $.ajax({
        url: '/ContasAReceber/BaixarConta', // Rota para a action que baixa a conta
        type: 'POST',
        data: { id: contasAReceberId }, // Envia o ID da conta a receber
        success: function (response) {
            if (response.success) {
                alert('Conta baixada com sucesso.');
                // Atualizar a interface para refletir que a conta foi baixada
                var rowId = response.contasAReceberId; // Suponha que sua resposta contenha o ID da conta baixada
                var rowData = response.rowData; // Suponha que sua resposta contenha os dados atualizados da linha
                $("#jqGridContasAReceber").jqGrid('setRowData', rowId, rowData);
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


function reabrirConta(contasAReceberId) {
    $.ajax({
        url: '/ContasAReceber/ReabrirConta', // Rota para a action que baixa a conta
        type: 'POST',
        data: { id: contasAReceberId }, // Envia o ID da conta a receber
        success: function (response) {
            if (response.success) {
                alert('Conta reaberta.');
                // Atualizar a interface para refletir que a conta foi baixada
                var rowId = response.contasAReceberId; // Suponha que sua resposta contenha o ID da conta baixada
                var rowData = response.rowData; // Suponha que sua resposta contenha os dados atualizados da linha
                $("#jqGridContasAReceber").jqGrid('setRowData', rowId, rowData);
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

