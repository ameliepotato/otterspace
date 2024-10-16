import '@testing-library/jest-dom';
import { render } from '@testing-library/react';
import Sensors from "./Sensors";
import getMockSensors from './_mockSensorService';

test('renders sensors', () => {
  render(<Sensors sensors={getMockSensors()} plan={ {src: './plan.jpg', width: 900, heigth: 900}}/>);
  var linkElement = document.getElementById("sensors");
  expect(linkElement).toBeInTheDocument();
  expect(linkElement.childNodes.length).toBe(4);
  linkElement = document.getElementById("sensorOne");
  expect(linkElement).toBeInTheDocument();
  linkElement = document.getElementById("sensorTwo");
  expect(linkElement).toBeInTheDocument();
  linkElement = document.getElementById("sensorThree");
  expect(linkElement).toBeInTheDocument();
  linkElement = document.getElementById("sensorFive");
  expect(linkElement).toBeInTheDocument();
});