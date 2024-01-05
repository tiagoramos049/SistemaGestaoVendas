$(document).ready(function () {
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

function editarRegistro(id) {
    // Implement your logic for editing the record with the given id
    console.log('Editar Registro: ' + id);
}

function excluirRegistro(id) {
    // Implement your logic for deleting the record with the given id
    console.log('Excluir Registro: ' + id);
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