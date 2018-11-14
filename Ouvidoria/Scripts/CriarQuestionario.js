$(function () {
    updateEvents();
    var qtdPerguntas = 1;

    $("#btn-add-pergunta").click(function (e) {
        e.preventDefault();

        var template = $('#template-pergunta').html().replace(/:qtdPerguntas:/gi, qtdPerguntas);

        $("#div-Perguntas").append(template);

        updateEvents();

        qtdPerguntas++;
    });

    $("#div-Perguntas").on("click", ".btn-remover-pergunta", function (e) {
        e.preventDefault();

        $(this).parent().parent().parent().parent().remove();

        qtdPerguntas--;

        $("#div-Perguntas .linhas").each(function (indice, elemento) {
            $(this).attr("id", "Pergunta[" + indice + "]");
            $(elemento).find(".txt-descricao").attr("name", "Pergunta[" + indice + "].Descricao");
            $(elemento).find(".ddl-tipo").attr("name", "Pergunta[" + indice + "].tipo");
        });

        updateEvents();

        $(".opcao").each(function (indice, elemento) {
            var id = $(this).parent().parent().parent().attr('id');
            var name = String($(elemento).attr("name"));
            name = name.replace(/Pergunta\[.*\].Opcao/g, id + ".Opcao");
            $(elemento).find(".opcao").attr("name", name);
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

    $("#div-Perguntas").on("click", ".btn-remover-opcao", function (e) {
        e.preventDefault();
        updateEvents();
        var id = $(this).parent().parent().parent().attr('id');
        var _id = $(this).parent().parent().parent();
        var indice = parseInt($(this).parent().parent().parent().attr("opcoes")) - 1;
        $(this).parent().parent().parent().attr("opcoes", indice);
        $(this).parent().parent().remove();
        console.log(_id);

        $(_id).find(".opcoes").each(function (indice, elemento) {
            console.log(elemento);
            $(elemento).find(".lbl-opcao").text("Opção " + (parseInt(indice) + 1));
            $(elemento).find(".opcao").attr("name", id + ".Opcao[" + indice + "].Texto");
        });

        updateEvents();
    });

    $("#btn-submit").click(function (e) {
        updateEvents();
        if (($("#Titulo").val() == null || $("#Titulo").val() == "") ||
            ($(".descricao").val() == null || $(".descricao").val() == "")) {
            $("#span-aviso").show();
            e.preventDefault();
            return;
        }

        $("#div-Perguntas").find(".txt-descricao").each(function () {
            if ($(this).val() == null || $(this).val() == "") {
                $("#span-aviso").show();
                e.preventDefault();
                return;
            }
        });

        $("#div-Perguntas").find(".opcao").each(function () {
            if ($(this).val() == null || $(this).val() == "") {
                $("#span-aviso").show();
                e.preventDefault();
                return;
            }
        });

    });


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
                    $(this).remove();
                });
            }

        });
    }
});