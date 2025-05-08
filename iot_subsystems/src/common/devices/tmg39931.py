from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20
import time
from common.devices.mocks import MockTMG39931
from common.devices.sensor import Sensor, Measurement, Reading
import logging

logger = logging.getLogger(__name__)


class InfraRedSensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.INFRARED

    def read_sensor(self) -> Reading:
        infrared, _, _, _, _ = self.device.read()
        reading = Reading(infrared, self.measurement)
        logger.info(reading)
        return reading


class RedColorSensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.RED

    def read_sensor(self) -> Reading:
        _, red, _, _, _ = self.device.read()
        reading = Reading(red, self.measurement)
        logger.info(reading)
        return reading


class GreenColorSensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.GREEN

    def read_sensor(self) -> Reading:
        _, _, green, _, _ = self.device.read()
        reading = Reading(green, self.measurement)
        logger.info(reading)
        return reading


class BlueColorSensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.BLUE

    def read_sensor(self) -> Reading:
        _, _, _, blue, _ = self.device.read()
        reading = Reading(blue, self.measurement)
        logger.info(reading)
        return reading


class ProximitySensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.PROXIMITY

    def read_sensor(self) -> Reading:
        _, _, _, _, proximity = self.device.read()
        reading = Reading(proximity, self.measurement)
        logger.info(reading)
        return reading


def main():
    device = MockTMG39931()

    infrared_sensor = InfraRedSensor(device=device)
    red_sensor = RedColorSensor(device=device)
    green_sensor = GreenColorSensor(device=device)
    blue_sensor = BlueColorSensor(device=device)
    proximity_sensor = ProximitySensor(device=device)
    while True:
        infrared = infrared_sensor.read_sensor()
        red = red_sensor.read_sensor()
        green = green_sensor.read_sensor()
        blue = blue_sensor.read_sensor()
        proximity = proximity_sensor.read_sensor()
        print("Infrared is {:.2f} ".format(infrared.value))
        print("Red is {:.2f} ".format(red.value))
        print("Green is {:.2f} ".format(green.value))
        print("Blue is {:.2f} ".format(blue.value))
        print("Proximity is {:.2f} ".format(proximity.value))

        time.sleep(1)


if __name__ == "__main__":
    main()
