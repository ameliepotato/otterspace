import '@testing-library/jest-dom';
import { fireEvent, render, screen } from '@testing-library/react';
import SensorModal from "./SensorModal";
import getMockSensors from './_mockSensorService';

test('edit dialog disabled works', async () => {
  var sensor = getMockSensors()[0];
  render(<SensorModal sensor={sensor} disabled={true} />);
  var btnEdit = screen.getByText(/Edit sensor/);
  expect(btnEdit).toBeInTheDocument();
  expect(btnEdit.disabled).toBe(true);
  var linkElement = document.getElementById("EditSensorModal");
  expect(linkElement).not.toBeInTheDocument();
});

test('edit dialog enabled works', async () => {
  var sensor = getMockSensors()[0];
  render(<SensorModal sensor={sensor} disabled={false} />);
  var btnEdit = screen.getByText(/Edit sensor/);
  expect(btnEdit).toBeInTheDocument();
  expect(btnEdit.disabled).toBe(false);
  await fireEvent.click(btnEdit);

  var linkElement = document.getElementById("EditSensorModal");
  expect(linkElement).toBeInTheDocument();
  linkElement = document.getElementById("sensorId");
  expect(linkElement).toBeInTheDocument();
  linkElement = document.getElementById("positionX");
  expect(linkElement).toBeInTheDocument();
  linkElement = document.getElementById("positionY");
  expect(linkElement).toBeInTheDocument();

  var btnExit = screen.getByText(/Cancel/);
  expect(btnExit).toBeInTheDocument();
  await fireEvent.click(btnExit);

  //linkElement = document.getElementById("sensorId");
  //expect(linkElement).not.toBeInTheDocument();
});


test('edit dialog callback works', async () => {
  var sensor = getMockSensors()[0];
  var called = false;
  var callback = () => { called = true; };

  render(<SensorModal sensor={sensor} disabled={false} saveCallback={callback} />);
  var btnEdit = screen.getByText(/Edit sensor/);
  expect(btnEdit).toBeInTheDocument();
  expect(btnEdit.disabled).toBe(false);
  await fireEvent.click(btnEdit);

  var btnExit = screen.getByText(/Save/);
  expect(btnExit).toBeInTheDocument();
  await fireEvent.click(btnExit);

  expect(called).toBe(true);
});


test('edit sensor properties works', async () => {
  var sensor = getMockSensors()[1];
  var callback = (x) => {
    expect(x.sensorId).toBe("TwoTest");
    expect(x.positionX).toBe(400);
    expect(x.positionY).toBe(135);//stays same 
  };

  render(<SensorModal sensor={sensor} disabled={false} saveCallback={callback} />);
  var btnEdit = screen.getByText(/Edit sensor/);
  expect(btnEdit).toBeInTheDocument();
  expect(btnEdit.disabled).toBe(false);
  await fireEvent.click(btnEdit);

  var linkElement = screen.getByLabelText(/Name:/);
  expect(linkElement).toBeInTheDocument();
  await fireEvent.change(linkElement, { target: { value: 'TwoTest' } });

  var linkElement2 = screen.getByLabelText(/Position X:/);
  expect(linkElement2).toBeInTheDocument();
  await fireEvent.change(linkElement2, { target: { value: 400 } });

  var btnExit = screen.getByText(/Save/);
  expect(btnExit).toBeInTheDocument();
  await fireEvent.click(btnExit);
});