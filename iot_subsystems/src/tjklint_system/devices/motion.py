import logging
from gpiozero import DigitalInputDevice
from random import randint
from common.devices.sensor import Sensor, Measurement, Reading

logger = logging.getLogger(__name__)

class MockMotionSensorDevice:
    def read(self):
        # Toggle state for mock
        return randint(0, 1)

class MotionSensor(Sensor):
    """Production implementation for Grove Adjustable PIR Motion Sensor."""
    device: DigitalInputDevice
    measurement: Measurement

    def __init__(self, device=None):
        if device is not None:
            self.device = device
        else:
            # GPIO12 is pin 12 (BCM numbering)
            self.device = DigitalInputDevice(12)
        self.measurement = Measurement.MOTION

    def read_sensor(self) -> Reading:
        value = int(self.device.value)
        reading = Reading(value=value, measurement=self.measurement)
        logger.info(reading)
        return reading

class MockMotionSensor(Sensor):
    """Mock implementation for development."""
    device: MockMotionSensorDevice
    measurement: Measurement

    def __init__(self, device=None):
        self.device = device or MockMotionSensorDevice()
        self.measurement = Measurement.MOTION

    def read_sensor(self) -> Reading:
        value = self.device.read()
        reading = Reading(value=value, measurement=self.measurement)
        logger.info(reading)
        return reading
