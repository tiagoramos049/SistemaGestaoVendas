$(document).ready(function () {
    var maxRowNum = 100;

    var rowList = [];
    for (var i = 10; i <= maxRowNum; i += 10) {
        rowList.push(i);
    }

    $("#jqGrid").jqGrid({
        url: '/Produto/GridData',
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Ações','Nome', 'Descrição', 'Preço Unitário','quantidade_estoque','unidade_medida','link_foto'],
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
            { name: 'descricao', index: 'descricao', width: 250 },
            { name: 'preco_unitario', index: 'preco_unitario', width: 100 },
            { name: 'quantidade_estoque', index: 'quantidade_estoque', width: 100 },
            { name: 'unidade_medida', index: 'unidade_medida', width: 150 },
            { name: 'link_foto', index: 'link_foto', width: 240 },
        ],
        pager: jQuery('#jqGridPager'),
        rowNum: 10,
        rowList: rowList,
        caption: 'Lista de Produtos',
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

    $('#editarModal').modal('show');
    $.ajax({
        url: '/Produto/GetDataForEdit',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            $('#campo1').val(data.campo1);
            $('#campo2').val(data.campo2);
        },
        error: function (error) {
            console.error('Erro ao obter dados para edição: ' + error.responseText);
        }
    });
}

function salvarEdicao() {
    console.log('Salvar Edição');
    $.ajax({
        url: '/Produto/Update',
        type: 'POST',
        data: {  },
        success: function (response) {
            console.log('Registro atualizado com sucesso.');
            $('#editarModal').modal('hide');
        },
        error: function (error) {
            console.error('Erro ao atualizar registro: ' + error.responseText);
        }
    });
}

function excluirRegistro(id) {
    console.log('Excluir Registro: ' + id);

    $.ajax({
        url: '/Produto/Delete', 
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

