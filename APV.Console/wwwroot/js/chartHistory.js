
function loadChart(sensorId, chartData) {
    var ctx = document.getElementById('chartImg' + sensorId);
    var myChart = new Chart(ctx, {
        type: 'line',
        data: chartData,
        options: {
            // additional configuration options
        }
    });
}

