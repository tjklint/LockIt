from gpiozero import Button
from time import sleep
from dataclasses import dataclass
from common.devices.actuator import Action, Command, Actuator
from common.devices.sensor import Sensor,Reading,Measurement
import logging

logger = logging.getLogger(__name__)

@dataclass
class DoorSensor(Sensor):

  def init(self) -> None:
        self.measurement = Measurement.DOOR

  def read_sensor(self) -> Reading:
        
        is_closed = self.device.is_pressed
        reading = Reading(value=str(is_closed), measurement=self.measurement)
        logger.info(reading)
        return reading



 




