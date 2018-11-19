$(document).ready(function () {
    updateEvents();
    var perguntas = [];
    var opcoes = [];

    $("#btn-add-pergunta").click(function (e) {
        e.preventDefault();
        updateEvents();
        var index = $("#div-Perguntas .linhas").length;
        var blocoPergunta = $("#template-pergunta").html().replace(/:index:/g, index);
        $("#div-Perguntas").append(blocoPergunta);
        updateEvents();
    });


    $("#div-Perguntas").on("click", ".btn-remover-pergunta", function (e) {
        e.preventDefault();
        updateEvents();

        var id = $(this).attr("data-id");

        if (id) {
            perguntas.push(parseInt(id));
            var tamanho = perguntas.length - 1;
            $("#arrayPerguntas").append('<input type="number" name="_perguntas[' + tamanho + ']" value="' + id + '" />');

            if (parseInt($(this).parent().parent().parent().attr('opcoes')) > 0) {
                $(this).parent().parent().parent().find('.opcoes').each(function () {
                    if ($(this).attr('data-id')) {
                        opcoes.push(parseInt($(this).attr('data-id')));              
                        var tamanho = opcoes.length - 1;
                        $("#arrayOpcoes").append('<input type="number" name="_opcoes[' + tamanho + ']" value="' + parseInt($(this).attr('data-id')) + '" />');
                    }
                });
            }
        }
            //$.post("/Clientes/RemoverTelefone?id=" + id);

        $(this).parent().parent().parent().remove();

        $("#div-Perguntas .linhas").each(function (indice, elemento) {
            $(this).attr("id", "Pergunta[" + indice + "]");
            $(this).find(".hid-id").attr("name", "Pergunta[" + indice + "].id");
            $(elemento).find(".txt-pergunta").attr("name", "Pergunta[" + indice + "].Descricao");
            $(elemento).find(".ddl-tipo").attr("name", "Pergunta[" + indice + "].tipo");
        });

        updateEvents();

        $(".opcao").each(function (indice, elemento) {
            var _id = $(this).parent().parent().parent().attr('id');
            var name = String($(elemento).attr("name"));
            $(this).find(".hid-id-opcao").attr("name", "Pergunta[" + id + "].id");
            name = name.replace(/Pergunta\[.*\].Opcao/gi, _id + ".Opcao");
            $(elemento).attr("name", name);
        });

        updateEvents();
    });


    $("#div-Perguntas").on("click", ".btn-remover-opcao", function (e) {
        e.preventDefault();
        console.log('oi');
        updateEvents();
        if ($(this).attr('data-id')) {
            opcoes.push(parseInt($(this).attr('data-id')));
            var tamanho = opcoes.length - 1;
            $("#arrayOpcoes").append('<input type="number" name="_opcoes[' + tamanho + ']" value="' + parseInt($(this).attr('data-id')) + '" />');
        }
        var id = $(this).parent().parent().parent().attr('id');
        var _id = $(this).parent().parent().parent();
        var indice = parseInt($(this).parent().parent().parent().attr("opcoes")) - 1;
        $(this).parent().parent().parent().attr("opcoes", indice);
        $(this).parent().parent().remove();

        $(_id).find(".opcoes").each(function (indice, elemento) {
            $(this).find(".hid-id-opcao").attr("name", id + ".Opcao[" + indice + "].id");
            $(elemento).find(".lbl-opcao").text("Opção " + indice + 1);
            $(elemento).find(".opcao").attr("name", id + ".Opcao[" + indice + "].Descricao");
        });
        updateEvents();
    });


    $("#div-Perguntas").on("click", ".btn-add-opcao", function (e) {
        e.preventDefault();
        var id = $(this).parent().parent().parent().attr('id');
        var indice = parseInt($(this).parent().parent().parent().attr("opcoes"));

        if (parseInt(indice) > 9) {
            return;
        }

        var template = $('#template-nova-opcao').html().replace(/:id:/gi, id);
        template = template.replace(/:indice:/gi, indice);
        template = template.replace(/:indices:/gi, indice + 1);
        $(this).parent().parent().parent().append(template);
        $(this).parent().parent().parent().attr("opcoes", indice + 1);
        e.preventDefault();
    });

    $("#btn-enviar").click(async function (e) {
        await remover();
        await $("#form").submit();
    });

    async function remover() {
        console.log("teste");
        if (opcoes.length != 0) {
            console.log(opcoes);
            await $.ajax({
                type: "POST",
                url: "/Questionarios/RemoverOpcoes",
                contentType: 'application/json',
                data: JSON.stringify({ opcoes: opcoes })
            });
        }
        
        if (perguntas.length != 0) {
            await $.ajax({
                type: "POST",
                url: "/Questionarios/RemoverPerguntas",
                contentType: 'application/json',
                data: JSON.stringify({ perguntas: perguntas })
            });
        }
    }
    
    function updateEvents() {
        $(".ddl-tipos, .ddl-tipo").off();

        $(".ddl-tipos, .ddl-tipo").change(function () {
            var id = $(this).parent().parent().parent().attr('id');
            var template = $('#template-opcao').html().replace(/:id:/g, id);

            if ($(this).val() == 1) {
                $(this).parent().parent().parent().append(template);
            }
            else if ($(this).val() == 0) {
                $(this).parent().parent().parent().find(".opcoes").each(function (indice, elemento) {
                    if ($(this).attr('data-id')) {
                        opcoes.push(parseInt($(this).attr('data-id')));
                        var tamanho = opcoes.length - 1;
                        $("#arrayOpcoes").append('<input type="number" name="_opcoes[' + tamanho + ']" value="' + parseInt($(this).attr('data-id')) + '" />');
                    }
                    $(this).remove();
                });
            }
        });
    }
});