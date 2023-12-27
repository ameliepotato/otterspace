function onHistoryModal(sensorId) {
    var outputElement = document.getElementById('body' + sensorId);

    fetch('SensorHistory?sensorID=' + sensorId)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(data => {
            var parser = new DOMParser();
            var doc = parser.parseFromString(data, "text/html");

            var chartData = JSON.parse(doc.getElementById('newChartData').innerHTML);
            loadChart(sensorId, chartData);
            //outputElement.innerHTML = doc.querySelector('#historyEntries').innerHTML;
        })
        .catch(error => {
            console.error('Error:', error);
        });

    $('#historyModal' + sensorId).modal('show');
}