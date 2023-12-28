
function loadChart(sensorId, chartData) {
    var ctx = document.getElementById('chartImg' + sensorId);
    var myChart = new Chart(ctx, {
        type: 'line',
        data: chartData,
        options: {
            resposive: true,
            scales: {
                x: {
                    type: 'time',
                    time: {
                        unit: 'day'
                    }
                }
            }
        }
    });
}

