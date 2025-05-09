from gpiozero import InputDevice
from time import sleep
from dataclasses import dataclass
from common.devices.actuator import Action, Command, Actuator
from common.devices.sensor import Sensor,Reading,Measurement
import logging

logger = logging.getLogger(__name__)

@dataclass
class DoorSensorActuator(Sensor):


    def __init__(self, device) -> None:
        self.device = InputDevice(5, pull_up=True)
        self.measurement = Measurement.DOOR

    def read_sensor(self) -> Reading:

        reading = Reading(str(self.device.is_active), self.measurement)
        logger.info(reading)

 




