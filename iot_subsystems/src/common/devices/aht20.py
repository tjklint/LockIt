from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20
import time
from sensor import Sensor, Measurement, Reading


class TemperatureSensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.TEMPERATURE

    def read_sensor(self) -> Reading:
        temperature, _ = self.device.read()
        reading = Reading(temperature, self.measurement)
        return reading


class HumiditySensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.HUMIDITY

    def read_sensor(self) -> Reading:
        _, humidity = self.device.read()
        reading = Reading(humidity, self.measurement)
        return reading


def main():
    temp_sensor = TemperatureSensor()
    humidity_sensor = HumiditySensor()
    while True:
        temperature = temp_sensor.read_sensor()
        humidity = humidity_sensor.read_sensor()
        print("Temperature in Celsius is {:.2f} C".format(temperature.value))
        print("Relative Humidity is {:.2f} %".format(humidity.value))

        time.sleep(1)


if __name__ == "__main__":
    main()