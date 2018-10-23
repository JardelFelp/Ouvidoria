google.charts.load('current', {packages: ['corechart', 'bar']});
google.charts.setOnLoadCallback(CarregaDados);

function CarregaDados() {
    $.ajax({
        url: '@Url.Action("getDepoimentosRespondidos", "Depoimentos")',
        dataType: "json",
        type: "GET",
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            toastr.error(err.message);
        },
        success: function (data) {
            GraficoPopulacional(data);
            return false;
        }
    });
return false;
}

function GraficoPopulacional(data) {
    console.log(data);

// Create the data table.
var data = new google.visualization.DataTable();
    data.addColumn('string', 'Topping');
    data.addColumn('number', 'Slices');
    data.addRows([
        /*
        ['Respondidos', data.respondidos],
        ['Não Respondidos', data.naoRespondidos],
        */
        ['Respondidos', 10],
        ['Não Respondidos', 3],
    ]);

    // Set chart options
    var options = {
    'title': 'Gráfico de Depoimentos Respondidos',
        'width': 400,
        'height': 300
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}