var vendaId;
function registrarVenda(idMoto, element) {
    vendaId = idMoto; // Armazenar o id da moto na variável global
    var idCliente = parseInt($(element).siblings('.clienteDropdown').val(), 10);
    if (isNaN(idCliente)) {
        alert('Por favor, selecione um cliente.');
        return;
    }

    var vendaData = {
        idMotos: vendaId, // Usar a variável global para o id da moto
        idCliente: idCliente // Converter para número
    };

    // Log para depuração
    console.log("Dados a serem enviados para o backend:", vendaData);

    $.ajax({
        type: "POST",
        url: '/Moto/RegistrarVenda',
        data: JSON.stringify(vendaData),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log("Resposta do backend:", response);
            if (response.success) {
                alert('Venda registrada com sucesso!');
                location.reload();
            } else {
                alert('Falha ao registrar a venda.');
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro na requisição:", status, error);
            console.error("Detalhes do erro:", xhr.responseText);
            alert('Erro na requisição.');
        }
    });
}
$(document).ready(function () {
    var maxRowNum = 100;

    var rowList = [];
    for (var i = 10; i <= maxRowNum; i += 10) {
        rowList.push(i);
    }

    var clientes = [];

    function carregarClientes(callback) {
        $.ajax({
            type: "GET",
            url: '/Moto/ObterClientes',
            async: false,
            success: function (data) {
                if (data.success) {
                    clientes = data.clientes;
                    console.log(clientes);  // Verificar se os dados dos clientes estão sendo carregados corretamente
                    if (callback) callback();  // Chamar o callback após carregar os clientes
                } else {
                    alert('Falha ao carregar os clientes.');
                }
            },
            error: function () {
                alert('Erro na requisição.');
            }
        });
    }

    // Inicialização do jqGrid
    function inicializarGrid() {
        $("#jqGridVendedor").jqGrid({
            url: '/Moto/GridData',
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Ações', 'Nome', 'Placa', 'Marca', 'Fabricação', 'Crv', 'Cor', 'Chassi', 'Cilindrada', 'ValorVenda', 'ValorCompra', 'DataCompra', 'Observações', 'Região'],
            colModel: [
                {
                    name: 'acoes',
                    index: 'acoes',
                    width: 800,
                    sortable: false,
                    formatter: function (cellvalue, options, rowObject) {
                        var editIcon = '<span class="icon-edit" title="Editar" onclick="editarRegistro(' + rowObject.id + ')">&#9998;&nbsp;&nbsp;</span>';
                        var deleteIcon = '<span class="icon-delete" title="Excluir" onclick="excluirRegistro(' + rowObject.id + ')">&#128465;</span>';

                        var dropdown = '<select class="clienteDropdown">';
                        dropdown += '<option value="">Selecione um cliente</option>';
                        $.each(clientes, function (index, cliente) {
                            dropdown += '<option value="' + cliente.idCliente + '">' + cliente.nome + '</option>';
                        });
                        dropdown += '</select>&nbsp &nbsp';
                        
                        var sellerIcon = '<span class="icon-sell" title="Vender" onclick="registrarVenda(' + rowObject.id + ', this)">💰</span>';
                        return editIcon + deleteIcon + dropdown + sellerIcon;
                    }
                },
                { name: 'nome', index: 'nome', width: 250 },
                { name: 'placa', index: 'placa', width: 300 },
                { name: 'marca', index: 'marca', width: 300 },
                { name: 'fabricacao', index: 'fabricacao', width: 250 },
                { name: 'crv', index: 'crv', width: 300 },
                { name: 'cor', index: 'cor', width: 300 },
                { name: 'chassi', index: 'chassi', width: 250 },
                { name: 'cilindrada', index: 'cilindrada', width: 300 },
                { name: 'valorVenda', index: 'valorVenda', width: 300 },
                { name: 'valorCompra', index: 'valorCompra', width: 250 },
                { name: 'dataCompra', index: 'dataCompra', width: 300 },
                { name: 'observacoes', index: 'observacoes', width: 250 },
                { name: 'regiao', index: 'regiao', width: 300 },
            ],
            pager: '#jqGridPager',
            rowNum: 10,
            rowList: rowList,
            caption: 'Lista de Motos',
            pgbuttons: true,
            pginput: true,
            pgtext: "Página {0} de {1}",
            loadonce: false,
            jsonReader: { repeatitems: false },
            height: '100%', // Altura total
            width: '100%', // Largura total
            autowidth: true,
            shrinkToFit: true,
            viewrecords: true,
        });
    }

    // Carregar clientes e inicializar o jqGrid
    carregarClientes(inicializarGrid);
});
    
var vendedorId;

function editarRegistro(id) {
    $('#editarModal').modal('show');
    vendedorId = id;
    $.ajax({
        url: '/Moto/GetDataForEdit',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            if (data.success) {
                $('#nome').val(data.campo1);
                $('#placa').val(data.campo2);
                $('#marca').val(data.campo3);

                $('#fabricacao').val(data.campo4);
                $('#crv').val(data.campo5);
                $('#cor').val(data.campo6);

                $('#chassi').val(data.campo7);
                $('#cilindrada').val(data.campo8);
                $('#valorVenda').val(data.campo9);

                $('#valorCompra').val(data.campo10);
                $('#dataVenda').val(data.campo11);
                $('#dataCompra').val(data.campo12);

                $('#observacoes').val(data.campo13);
                $('#regiao').val(data.campo14);
                $('#idCliente').val(data.campo15);
                $('#Estoque').val(data.campo15);
                
            } else {
                console.error('Moto não encontrada.');
            }
        },
        error: function (error) {
            console.error('Erro ao obter dados para edição: ' + error.responseText);
        }
    });
}

function salvarEdicao() {
    $.ajax({
        url: '/Moto/Update',
        type: 'POST',
        data: {
            id: vendedorId,
            campo1: $('#nome').val(),
            campo2: $('#placa').val(),
            campo3: $('#marca').val(),
            campo4: $('#fabricacao').val(),
            campo5: $('#crv').val(),
            campo6: $('#cor').val(),

            campo7: $('#chassi').val(),
            campo8: $('#cilindrada').val(),
            campo9: $('#valorVenda').val(),

            campo10: $('#valorCompra').val(),
            
            campo12: $('#dataCompra').val(),

            campo13: $('#observacoes').val(),
            campo14: $('#regiao').val(),
            campo15: $('#idCliente').val(),
            campo15: $('#Estoque').val(),
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
        url: '/Moto/Delete',
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
    $('#motoForm').submit(function (e) {
        e.preventDefault();

        var formData = $(this).serialize();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('#mensagem').html('<div class="alert alert-success">Moto salva com sucesso!</div>');
                    window.location.href = '/Checklist/Index?idMotos=' + response.idMoto;
                } else {
                    $('#mensagem').html('<div class="alert alert-danger">Erro ao salvar Moto.</div>');
                }
            },
            error: function (error) {
                $('#mensagem').html('<div class="alert alert-danger">Erro ao salvar Moto.</div>');
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



