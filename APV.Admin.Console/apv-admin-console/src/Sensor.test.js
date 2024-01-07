import '@testing-library/jest-dom';
import { fireEvent, render, screen } from '@testing-library/react';
import Sensor from "./Sensor";
import getMockSensors from './_mockSensorService';

test('renders unselected sensor', async () => {
  render(<Sensor sensor={getMockSensors()[0]} selected={false}/>);
  var linkElement = document.getElementById("OneIco");
  expect(linkElement).toBeInTheDocument();
  expect(linkElement.style.color).toBe('blue');
});

test('renders selected sensor', async () => {
  render(<Sensor sensor={getMockSensors()[0]} selected={true}/>);
  var linkElement = document.getElementById("OneIco");
  expect(linkElement).toBeInTheDocument();
  expect(linkElement.style.color).toBe('red');
});