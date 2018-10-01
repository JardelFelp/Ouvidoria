$(document).ready(function () {
    const itens = $('#acessos li.op');

    if (itens.length) {
        for (let i = 0; i < itens.length; i++) {
            const url = $(itens[i]).attr('url');

            if (window.location.pathname.includes(url)) {
                $(itens[i]).addClass('active');
            } else {
                $(itens[i]).removeClass('active');
            }
        }
    }
});