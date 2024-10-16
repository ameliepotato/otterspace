import '@testing-library/jest-dom'
import { fireEvent, render, screen } from '@testing-library/react';
import App from './App';
import getMockSensors from './_mockSensorService';

test('add sensor works', async () => {
  render(<App />);
  var linkElement = screen.getByText(/add sensor/i);
  expect(linkElement).toBeInTheDocument();
  await fireEvent.click(linkElement);
  linkElement = document.getElementById("sensors");
  expect(linkElement).toBeInTheDocument();
  expect(linkElement.childNodes.length).toBeGreaterThan(0);
  var x = linkElement.childNodes[linkElement.childNodes.length-1].clientX;
  var y = linkElement.childNodes[linkElement.childNodes.length-1].getBoundingClientRect().top;
});


test('edit sensor works', async () => {
  var sensors=getMockSensors();
  render(<App sensors={sensors} />);
  var editBtn = screen.getByText(/Edit sensor/);
  expect(editBtn).toBeInTheDocument();
  expect(editBtn.disabled).toBe(true);
  var linkElement = document.getElementById('TwoIco');
  expect(linkElement).toBeInTheDocument();
  await fireEvent.click(linkElement.childNodes[0]);
  expect(editBtn).toBeInTheDocument();
  expect(editBtn.disabled).toBe(false);
  await fireEvent.click(editBtn);
  linkElement = document.getElementById("EditSensorModal");
  expect(linkElement).toBeInTheDocument();
});

test('save sensors works', () => {
  render(<App />);
  const linkElement = screen.getByText(/save sensors/i);
  expect(linkElement).toBeInTheDocument();
});

test('delete sensor works', async() => {
  var sensors=getMockSensors();
  render(<App sensors={sensors} />);
  const linkDeleteBtn = screen.getByText(/delete sensor/i);
  expect(linkDeleteBtn).toBeInTheDocument();

  var linkElement = document.getElementById('TwoIco');
  expect(linkElement).toBeInTheDocument();
  await fireEvent.click(linkElement.childNodes[0]);
  await fireEvent.click(linkDeleteBtn);
  linkElement = document.getElementById('sensorTwo');
  expect(linkElement).not.toBeInTheDocument();
});

test('select and deselect sensor works', async () => {
  var sensors=getMockSensors();
  render(<App sensors={sensors} />);
  var linkElement = document.getElementById("TwoIco");
  expect(linkElement).toBeInTheDocument();
  expect(linkElement.style.color).toBe('blue');
  await fireEvent.click(linkElement.childNodes[0]);
  linkElement = document.getElementById("TwoIco");
  expect(linkElement).toBeInTheDocument();
  expect(linkElement.style.color).toBe('red');
  linkElement = document.getElementById("ThreeIco");
  expect(linkElement).toBeInTheDocument();
  expect(linkElement.style.color).toBe('blue');
  linkElement = document.getElementById("sensors");
  expect(linkElement).toBeInTheDocument();
  await fireEvent.click(linkElement);
  linkElement = document.getElementById("TwoIco");
  expect(linkElement.style.color).toBe('blue');
});


