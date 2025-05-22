from gpiozero import Button
from time import sleep
from dataclasses import dataclass
from common.devices.actuator import Action, Command, Actuator
from common.devices.sensor import Sensor, Reading, Measurement
import logging
import time
logger = logging.getLogger(__name__)


@dataclass
class DoorSensor(Sensor):
    def __init__(self,device: Button) -> None:
        self.device = device
        self.measurement = Measurement.DOOR

    def read_sensor(self) -> Reading:
        is_closed = self.device.is_pressed
        reading = Reading(value=str(is_closed), measurement=self.measurement)
        logger.info(reading)
        return reading

def main():
    button1 =Button(18)


    device = DoorSensor(button1)

   
    while True:
        reading = device.read_sensor()

        print(reading)
        time.sleep(1)


if __name__ == "__main__":
    main()
