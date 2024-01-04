<script>
    $(document).ready(function () {
        $("#grid").jqGrid({
            url: '/Produto/GetProdutos', // URL para buscar dados do backend
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Id', 'Nome', 'Descrição', 'Preço Unitário'],
            colModel: [
                { name: 'Id', key: true, hidden: true },
                { name: 'Nome', width: 100 },
                { name: 'Descricao', width: 200 },
                { name: 'Preco_Unitario', width: 80 }
            ],
            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [5, 10, 20, 50],
            viewrecords: true,
            width: '100%',
            height: '100%'
        });
    });
</script>