
function loadChart(sensorId) {
    var ctx = document.getElementById('chartImg' + sensorId);
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Label 1', 'Label 2', 'Label 3'],
            datasets: [{
                label: 'My Dataset',
                data: [10, 20, 30],
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1
            }]
        },
        options: {
            // additional configuration options
        }
    });
}