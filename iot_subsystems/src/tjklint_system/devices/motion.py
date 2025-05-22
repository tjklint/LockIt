import logging
import os
from random import randint
from common.devices.sensor import Sensor, Measurement, Reading

logger = logging.getLogger(__name__)

class MockMotionSensorDevice:
    def read(self):
        return randint(0, 1)

class MotionSensor(Sensor):
    """Production implementation for Grove Adjustable PIR Motion Sensor."""
    device: object
    measurement: Measurement

    def __init__(self, device=None):
        # Check for Raspberry Pi GPIO sysfs
        if not os.path.exists("/sys/class/gpio"):
            raise RuntimeError(
                "GPIO sysfs not found. Are you running on a Raspberry Pi? "
                "Use ENVIRONMENT=DEVELOPMENT for mock sensors."
            )
        if device is not None:
            self.device = device
        else:
            from gpiozero import DigitalInputDevice
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
