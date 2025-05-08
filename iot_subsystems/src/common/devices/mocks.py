from random import random

from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20


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

    def read(self) -> tuple[float, float,float,float,float]:
    
        return (random() * 100, random() * 100,random() * 100,random() * 100,random() * 100)
    
   