import * as React from 'react';
import Button from '@mui/material/Button';
import { Dialog, DialogContent, DialogContentText, DialogTitle, TextField, DialogActions } from '@mui/material';
import './SensorModal.css';

function SensorModal(props) {
    const [open, setOpen] = React.useState(false);
    const [sensorName, setSensorName] = React.useState("");
    const [positionX, setPositionX] = React.useState(-1);
    const [positionY, setPositionY] = React.useState(-1);

    const handleOpen = () => {
        setOpen(true);
        setSensorName(props.sensor.sensorId);
        setPositionX(props.sensor.positionX);
        setPositionY(props.sensor.positionY);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleSave = () => {
        if (props.saveCallback != null) {
            props.saveCallback({
                sensorId: sensorName,
                positionX: positionX,
                positionY: positionY
            });
        }
        else {
            console.log('nothing was saved for ' + sensorName);
        }
        setOpen(false);
    }

    return (
        <>
            <Button variant="contained" className='btn-Left' disabled={props.disabled} onClick={handleOpen}>
                Edit sensor
            </Button>
            {!props.disabled &&
                <Dialog open={open} onClose={handleClose} id="EditSensorModal">
                    <DialogTitle className='modal-dialog-title'>
                        Sensor {sensorName} </DialogTitle>
                    <DialogContent>
                        <DialogContentText>

                        </DialogContentText>
                        <TextField
                            label="Name:"
                            margin="dense"
                            id="sensorId"
                            variant="standard"
                            value={sensorName}
                            onChange={(event) => {
                                event.stopPropagation();
                                setSensorName(event.target.value);
                            }}
                        />
                        <br></br>
                        <TextField
                            label="Vertical:"
                            id='positionY'
                            type="number"
                            margin='dense'
                            value={positionY}
                            onChange={(event) => {
                                event.stopPropagation();
                                setPositionY(Number(event.target.value))
                            }}
                        />
                         <TextField
                            label="Horizontal:"
                            id='positionX'
                            type='number'
                            margin='dense'
                            value={positionX}
                            onChange={(event) => {
                                event.stopPropagation();
                                setPositionX(Number(event.target.value))
                            }}
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} variant='contained'>Cancel</Button>
                        <Button onClick={handleSave} variant='contained'>Save</Button>
                    </DialogActions>
                </Dialog>
            }
        </>
    );
}

export default SensorModal;