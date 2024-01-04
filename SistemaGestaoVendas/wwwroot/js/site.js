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
        colNames: ['Nome', 'Descrição', 'Preço Unitário'],
        colModel: [
            { name: 'nome', index: 'nome', width: 300 },
            { name: 'descricao', index: 'descricao', width: 300 },
            { name: 'preco_unitario', index: 'preco_unitario', width: 100 }
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

