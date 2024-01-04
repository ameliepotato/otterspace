import React from 'react';
import planjpg from './plan.jpg';
import Sensor from './Sensor';

function Sensors(props) {
    return (
        <>
            <div id="sensors"
                style={{
                    backgroundImage: `url(${planjpg})`,
                    backgroundRepeat: 'no-repeat',
                    backgroundSize: 'cover',
                    width: '842px',
                    height: '569px'
                }}
                onClick={(x) => {
                    x.stopPropagation();
                    props.onSelection(x, null);
                }}>
                {props.sensors.map((sensor, index) => (
                    <div key={index} id={"sensor" + sensor.sensorId}
                        style={{
                            position: 'relative',
                            left: sensor.positionX,
                            top: sensor.positionY,
                            float: 'left'
                        }}>
                        <Sensor sensor={sensor} selected={sensor.sensorId === props.selected} onSelection={props.onSelection}></Sensor>
                    </div>))}
            </div>
        </>
    );
}

export default Sensors;
