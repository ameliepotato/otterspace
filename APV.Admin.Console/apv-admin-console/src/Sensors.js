import React, { useState } from 'react';
import Sensor from './Sensor';
import { HiArrowDown, HiArrowRight, HiCursorClick } from 'react-icons/hi';

function Sensors(props) {
    const [cursorPosition, setCursorPosition] = useState({ x: 0, y: 0 });
    function onCursor(event) {
        setCursorPosition({ x: event.nativeEvent.offsetX, y: event.nativeEvent.offsetY });
    }
    return (
        <>
            <div>
            <div style={{
                    color: 'blue'
                }}>
                    <i>
                        {props.plan.height}px
                        <HiArrowDown></HiArrowDown>

                    </i>
                </div>

                <div id="sensors"
                    style={{
                        backgroundImage: `url(${props.plan.src})`,
                        backgroundRepeat: 'no-repeat',
                        backgroundSize: 'cover',
                        width: props.plan.width + 'px',
                        height: props.plan.height + 'px',
                        position: 'relative'
                    }}
                    onClick={(x) => {
                        x.stopPropagation();
                        onCursor(x);
                        props.onSelection(x, null);
                    }}>
                    {(cursorPosition.x>0 || cursorPosition.y>0) && 
                    <h1 id="cursor"
                        style={{
                            position: 'absolute',
                            left: cursorPosition.x,
                            top: cursorPosition.y,
                            color: 'red',
                            fontSize: '15px'
                        }}
                        onClick={()=>{}}
                    >
                        {cursorPosition.y}
                        <HiCursorClick color='red'>
                        </HiCursorClick>
                        {cursorPosition.x}
                    </h1>}
                    {props.sensors.map((sensor, index) => (
                        <div key={index} id={"sensor" + sensor.sensorId}
                            style={{
                                position: 'absolute',
                                left: sensor.positionX,
                                top: sensor.positionY,
                                float: 'left'
                            }}>
                            <Sensor sensor={sensor} selected={sensor.sensorId === props.selected} onSelection={props.onSelection}></Sensor>
                        </div>))}
                </div>
                <div style={{
                    color: 'blue'
                }}>
                    <i>
                        {props.plan.width}px
                        <HiArrowRight></HiArrowRight>
                    </i>
                </div>
            </div>
        </>
    );
}

export default Sensors;
