import logging
from random import uniform
from common.devices.sensor import Sensor, Measurement, Reading

logger = logging.getLogger(__name__)

class MockGPSDevice:
    def read(self):
        lat = round(uniform(-90, 90), 6)
        lon = round(uniform(-180, 180), 6)
        return lat, lon

class GPSSensor(Sensor):
    device: MockGPSDevice
    measurement: Measurement

    def __init__(self, device=None):
        self.device = device or MockGPSDevice()
        self.measurement = Measurement.GPS

    def read_sensor(self) -> Reading:
        lat, lon = self.device.read()
        value = {"lat": lat, "lon": lon}
        reading = Reading(value=value, measurement=self.measurement)
        logger.info(reading)
        return reading
