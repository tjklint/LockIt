from random import random, choice

from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20
from common.devices.sensor import Sensor, Measurement, Reading
from gpiozero import Button
import logging

logger = logging.getLogger(__name__)


class MockGroveTemperatureHumidityAHT20(GroveTemperatureHumidityAHT20):
    """Mock implementation of the GroveTemperatureHumidityAHT20."""

    def __init__(self, address: int = 0x38, bus: int = 4) -> None:
        """Initializes the mock class."""
        self.address = address
        self.bus = bus

    def read(self) -> tuple[float, float]:
        """Returns a random reading between 0-100 for temperature and humidity."""
        return (random() * 100, random() * 100)


class MockTMG39931(GroveTemperatureHumidityAHT20):
    def __init__(self, address: int = 0x39, bus: int = 1) -> None:
        """Initializes the mock class."""
        self.address = address
        self.bus = bus

    def read(self) -> tuple[float, float, float, float, float]:
        return (random() * 100, random() * 100, random() * 100, random() * 100, random() * 100)


class MockDoorSensor(Sensor):
    def __init__(self) -> None:
        self.measurement = Measurement.DOOR

    def read_sensor(self) -> Reading:
        isClosed = choice([True, False])
        reading = Reading(value=str(isClosed), measurement=self.measurement)
        logger.info(reading)
        return reading
