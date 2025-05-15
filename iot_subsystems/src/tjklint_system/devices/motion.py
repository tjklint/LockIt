import logging
from common.devices.sensor import Sensor, Measurement, Reading

logger = logging.getLogger(__name__)


class MockMotionSensorDevice:
    def __init__(self):
        self.state = False

    def read(self):
        # Toggle state for mock
        self.state = not self.state
        return int(self.state)


class MotionSensor(Sensor):
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
