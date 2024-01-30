import './App.css';
import React from 'react';
import Button from '@mui/material/Button';
import { useState, useEffect } from 'react';
import axios from 'axios';
import Sensors from './Sensors';
import SensorModal from './SensorModal';

function App() {
  const [sensors, setSensors] = useState([]);
  const [selected, setSelected] = useState(null);
  const [dirty, setDirty] = useState(false);
  const [plan, setPlan] = useState({ src: '', width: 0, height: 0 });

  function isSelected() {
    return selected != null && selected.length > 0;
  }


  function getSensorsAPV() {
    axios({
      method: 'get',
      url: process.env.REACT_APP_APVSERVICE_SENSORS + "GetSensors"
    })
      .then(function (response) {
        var sensorList = [];
        response.data.forEach(element => {
          sensorList.push({
            sensorId: element.Id,
            positionX: element.Position.Item1,
            positionY: element.Position.Item2
          });
        });
        setSensors(sensorList);
      });
  }

  function saveSensorsAPV() {
    var sensorList = [];
    sensors.forEach(element => {
      sensorList.push({
        Position: {
          Item1: element.positionX,
          Item2: element.positionY
        }
      });
    });
    axios.post(process.env.REACT_APP_APVSERVICE_SENSORS + "SaveSensors",
      { sensors: JSON.stringify(sensorList) },
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
      .then((response) => {
        console.log(response);
      }, (error) => {
        console.log(error);
      });
  }


  function addNewSensor() {
    setDirty(true);
    var sensorPosition = document.getElementById("cursor");
    if (sensorPosition == null) {
      sensorPosition = { offsetLeft: 0, offsetTop: 0 };
    }
    var sensor = { sensorId: "NewSensor" + (new Date()).getTime(), positionX: sensorPosition.offsetLeft, positionY: sensorPosition.offsetTop };
    setSensors([...sensors, sensor]);
    setSelected(sensor.sensorId);
  }

  async function saveSensors() {
    setDirty(false);
    saveSensorsAPV();
    console.log('Saved ' + sensors.length + ' sensors');
  }

  function deleteSensor() {
    setDirty(true);
    var remainingSensors = sensors.filter((s) => s.sensorId !== selected);
    setSensors(remainingSensors);
    setSelected(null);
  }

  function onSelection(event, sensorId) {
    setSelected(sensorId);
  }

  function saveEditedSensor(sensor) {
    setDirty(true);
    var remainingSensors = sensors.filter(x => x.sensorId !== selected && x.sensorId !== sensor.sensorId);
    setSensors([...remainingSensors, sensor]);
    setSelected(sensor.sensorId);
  }

  async function loadPlanImage() {
    var result = await axios.get(
      process.env.REACT_APP_APVSERVICE_SENSORS + "GetPlan"
    );
    setPlan({ src: 'data:image/jpeg;base64,' + result.data, width: 842, height: 569 });
  }

  function updatePlanImage(event) {
    axios.post(process.env.REACT_APP_APVSERVICE_SENSORS + "SavePlan",
      { plan: event.target.files[0] },
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
      .then((response) => {
        loadPlanImage();
        console.log(response);
      }, (error) => {
        console.log(error);
      });
  }

  function renderSensorEditModal() {
    var selectedSensor = sensors.filter(s => s.sensorId === selected)[0];
    return (<>
      {<SensorModal disabled={!isSelected()} sensor={selectedSensor} saveCallback={saveEditedSensor} />}
    </>);
  }

  useEffect(() => {
    loadPlanImage();
    getSensorsAPV();
  }, []);

  return (
    <div className="App">
      <div className="App-header" >
      </div>
      <div className='App-buttons-bar'>
        <Button disabled={isSelected()} variant='contained' onClick={addNewSensor}>  Add sensor</Button>
        <label className='btn-left'></label>
        {renderSensorEditModal()}
        <label className='btn-left'></label>
        <Button disabled={!isSelected()} variant='contained' className='btn-left' onClick={deleteSensor}>  Delete sensor</Button>
        <label className='btn-left'></label>
        <Button disabled={!dirty} variant='contained' className='btn-left' onClick={saveSensors}> Save sensors</Button>
        <label className='btn-left'></label>
        <input accept="image/*" id="icon-button-file" type="file" style={{ display: 'none' }}  onChange={updatePlanImage} />
        <label htmlFor="icon-button-file">
          <Button variant='contained' className='btn-left'component="span">Change map</Button>
        </label>
      </div>
      <br></br>
      <div onClick={onSelection}>
        <Sensors sensors={sensors} selected={selected} onSelection={onSelection} plan={plan}></Sensors>
      </div>
    </div>
  );
}
export default App;
