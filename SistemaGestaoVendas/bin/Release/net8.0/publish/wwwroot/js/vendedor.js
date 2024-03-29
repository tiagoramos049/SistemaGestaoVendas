﻿$(document).ready(function () {
    var maxRowNum = 100;

    var rowList = [];
    for (var i = 10; i <= maxRowNum; i += 10) {
        rowList.push(i);
    }

    $("#jqGridVendedor").jqGrid({
        url: '/Vendedor/GridData',
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Ações', 'Nome', 'Email', 'Senha'],
        colModel: [
            {
                name: 'acoes',
                index: 'acoes',
                width: 70,
                sortable: false,
                formatter: function (cellvalue, options, rowObject) {
                    var editIcon = '<span class="icon-edit" title="Editar" onclick="editarRegistro(' + rowObject.id + ')">&#9998;&nbsp;&nbsp;</span>';
                    var deleteIcon = '<span class="icon-delete" title="Excluir" onclick="excluirRegistro(' + rowObject.id + ')">&#128465;</span>';
                    return editIcon + deleteIcon;
                }
            },
            { name: 'nome', index: 'nome', width: 250 },
            { name: 'email', index: 'email', width: 400 },
            {
                name: 'senha',
                index: 'senha',
                width: 300,
                formatter: function (cellvalue, options, rowObject) {
                    return '******'; 
                }
            },
        ],
        pager: jQuery('#jqGridPager'),
        rowNum: 10,
        rowList: rowList,
        caption: 'Lista de Vendedores',
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
var vendedorId;

function editarRegistro(id) {
    $('#editarModal').modal('show');
    vendedorId = id;
    $.ajax({
        url: '/Vendedor/GetDataForEdit',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            if (data.success) {
                $('#nome').val(data.campo1);
                $('#email').val(data.campo2);
                $('#senha').val(data.campo3);
                
            } else {
                console.error('Vendedor não encontrado.');
            }
        },
        error: function (error) {
            console.error('Erro ao obter dados para edição: ' + error.responseText);
        }
    });
}

function salvarEdicao() {
    $.ajax({
        url: '/Vendedor/Update',
        type: 'POST',
        data: {
            id: vendedorId,
            campo1: $('#nome').val(),
            campo2: $('#email').val(),
            campo3: $('#senha').val(),
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
        url: '/Vendedor/Delete',
        type: 'POST',
        data: { id: id }, 
        success: function (response) {
            alert('Registro excluído com sucesso.');
            carregarDadosGrid();
        },
        error: function (error) {
            console.error('Erro ao excluir registro: ' + error.responseText);
        }
    });
}

$(document).ready(function () {
    $('#vendedorForm').submit(function (e) {
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
    $("#jqGridVendedor").trigger("reloadGrid");
}