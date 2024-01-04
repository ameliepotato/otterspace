import './App.css';
import React from 'react';
import Button from '@mui/material/Button';
import { useState } from 'react';
import Sensors from './Sensors';
import getMockSensors from './_mockSensorService';
import SensorModal from './SensorModal';

function App() {
  const [sensors, setSensors] = useState(getMockSensors());
  const [selected, setSelected] = useState(null);
  const [dirty, setDirty] = useState(false);

  function isSelected() {
    return selected != null && selected.length > 0;
  }

  function addNewSensor() {
    setDirty(true);
    var sensor = { sensorId: "NewSensor" + (new Date()).getTime(), positionX: 150, positionY: 150 };
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
    event.stopPropagation();
    setSelected(sensorId);
  }

  function saveEditedSensor(sensor) {
    setDirty(true);
    var remainingSensors = sensors.filter(x => x.sensorId !== selected && x.sensorId !== sensor.sensorId);
    setSensors([...remainingSensors, sensor]);
    setSelected(sensor.sensorId);
  }

  function renderSensorEditModal() {
    var selectedSensor = sensors.filter(s => s.sensorId === selected)[0];
    return (<>
      {<SensorModal disabled={!isSelected()} sensor={selectedSensor} saveCallback={saveEditedSensor} />}
    </>);
  }

  return (
    <div className="App">
      <div className="App-header">
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
        <Button variant='contained' className='btn-left'> Change floor plan</Button>
      </div>
      <br></br>
      <div onClick={onSelection}>
        <Sensors sensors={sensors} selected={selected} onSelection={onSelection}></Sensors>
      </div>
    </div>
  );
}
export default App;
