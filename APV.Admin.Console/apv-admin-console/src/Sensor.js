import React from 'react';
import { HiAdjustments } from "react-icons/hi";
import { Tooltip } from '@mui/material';

function Sensor(props) {
    return (
        <>
            <Tooltip title={
                <label style={{ color: props.selected?"red":"white", size: "32px", fontSize: "24px"}}>
                    {props.sensor.sensorId}
                    <br></br>
                    ({props.sensor.positionX}, {props.sensor.positionY})
                </label>}
                placement='right'
                arrow>
                <label>
                    <HiAdjustments id={props.sensor.sensorId + "Ico"}
                        fill={props.selected ? 'red' : 'blue'} size="64px"
                        onClick={(x) => {
                            x.stopPropagation();
                            props.onSelection(x, props.sensor.sensorId);
                        }}>
                    </HiAdjustments>
                </label>
            </Tooltip>
        </>
    );
}

export default Sensor;
