import React from 'react';
import DeviceThermostatIcon from '@mui/icons-material/DeviceThermostat';
import { Tooltip } from '@mui/material';

function Sensor(props) {
    return (
        <>
            <Tooltip title={
                <div onClick={(e) => { e.stopPropagation(); }}
                    style={{ color: props.selected ? "red" : "white", size: "32px", fontSize: "20px" }}>
                    {props.sensor.sensorId}
                    <br></br>
                    ({props.sensor.positionX}, {props.sensor.positionY})
                </div>}
                placement='top-end'
                arrow
                disableFocusListener disableTouchListener>
                <div id={props.sensor.sensorId + "Ico"} 
                     style={{ color: (props.selected ? 'red' : 'blue') }}>
                    <DeviceThermostatIcon
                        fontSize='large'
                        onClick={(x) => {
                            x.stopPropagation();
                            props.onSelection(x, props.sensor.sensorId);
                        }}>
                    </DeviceThermostatIcon>
                </div>
            </Tooltip>
        </>
    );
}

export default Sensor;
