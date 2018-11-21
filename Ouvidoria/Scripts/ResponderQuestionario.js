$(document).ready(function () {

    $("#btn-submit").click(function (e) {
        $(".resposta").each(function () {
            console.log("Teste");
            if ($(this).val() == null || $(this).val() == "") {
                var span = '<span class="field-validation-valid text-danger validacao">A resposta é obrigatória</span>';
                $(this).parent().find(".validacao").remove();
                $(this).parent().append(span);
                e.preventDefault();
            }
            else {
                $(this).parent().find(".validacao").remove();
            }
        });
    });
});