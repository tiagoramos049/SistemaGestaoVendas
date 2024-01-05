$(document).ready(function () {
    var maxRowNum = 100;

    var rowList = [];
    for (var i = 10; i <= maxRowNum; i += 10) {
        rowList.push(i);
    }

    $("#jqGridCliente").jqGrid({
        url: '/Cliente/GridData',
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Ações','Nome', 'Cpf_Cnpj', 'Email', 'Senha'],
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
            { name: 'cpf_cnpj', index: 'cpf_cnpj', width: 250 },
            { name: 'email', index: 'email', width: 200 },
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
        caption: 'Lista de Clientes',
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

function editarRegistro(id) {
    console.log('Editar Registro: ' + id);
    $.ajax({
        url: '/Cliente/Update', 
        type: 'POST',
        data: { id: id }, 
        success: function (response) {
            console.log('Registro atualizado com sucesso.');
        },
        error: function (error) {
            console.error('Erro ao atualizar registro: ' + error.responseText);
        }
    });
}

function excluirRegistro(id) {
    console.log('Excluir Registro: ' + id);

    $.ajax({
        url: 'Cliente/Delete',
        type: 'POST',
        data: { id: id },
        success: function (response) {
            console.log('Registro excluído com sucesso.');
        },
        error: function (error) {
            console.error('Erro ao excluir registro: ' + error.responseText);
        }
    });
}

$(document).ready(function () {
    $('#clienteForm').submit(function (e) {
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