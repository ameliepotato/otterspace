import './App.css';
import React from 'react';
import Button from '@mui/material/Button';
import { useState, useEffect } from 'react';
import Sensors from './Sensors';
import getMockSensors from './_mockSensorService';
import SensorModal from './SensorModal';
import getImageMeta from './getImageMeta';

function App() {
  const [sensors, setSensors] = useState(getMockSensors());
  const [selected, setSelected] = useState(null);
  const [dirty, setDirty] = useState(false);
  const [plan, setPlan] = useState({ src: '', width: 0, height: 0});
  
  function isSelected() {
    return selected != null && selected.length > 0;
  }

  function addNewSensor() {
    setDirty(true);
    var sensorPosition = document.getElementById("cursor");
    if(sensorPosition == null){
      sensorPosition = { offsetLeft: 0, offsetTop: 0};
    }
    var sensor = { sensorId: "NewSensor" + (new Date()).getTime(), positionX: sensorPosition.offsetLeft, positionY: sensorPosition.offsetTop };
    setSensors([...sensors, sensor]);
    setSelected(sensor.sensorId);
  }

  function saveSensors() {
    setDirty(false);
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

  function loadPlanImage(){
    getImageSize('./plan.jpg');
  }

  function getImageSize(image){
    getImageMeta(image,  (err, img) => {
        setPlan({src: img.src, width: img.naturalWidth, height: img.naturalHeight});
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
        <Button variant='contained' className='btn-left' onClick={loadPlanImage}>Change map</Button>
      </div>
      <br></br>
      <div onClick={onSelection}>
        <Sensors sensors={sensors} selected={selected} onSelection={onSelection} plan={plan}></Sensors>
      </div>
    </div>
  );
}
export default App;
