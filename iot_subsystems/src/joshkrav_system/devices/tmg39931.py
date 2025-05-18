from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20
import time
from common.devices.sensor import Sensor, Measurement, Reading
import logging
import smbus



logger = logging.getLogger(__name__)


class ProximitySensor(Sensor):
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
        logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        return reading


class GreenColorSensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.GREEN

    def read_sensor(self) -> Reading:
        _, _, green, _, _ = self.device.read()
        reading = Reading(green, self.measurement)
        logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        return reading


class BlueColorSensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.BLUE

    def read_sensor(self) -> Reading:
        _, _, _, blue, _ = self.device.read()
        reading = Reading(blue, self.measurement)
        logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        return reading


class ProximitySensor(Sensor):
    def __init__(self, device) -> None:
        self.device = device
        self.measurement = Measurement.PROXIMITY

    def read_sensor(self) -> Reading:
        _, _, _, _, proximity = self.device.read()
        reading = Reading(proximity, self.measurement)
        logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        return reading

class TMG39931():
    def __init__(self, address: int = 0x39) -> None:
        """Initializes the mock class."""
        self.address = address
        self.bus = smbus.SMBus(1)

    def read(self) -> tuple[float,float,float,float,float]:
                # TMG39931 address, 0x39(57)
        # Select Enable register, 0x80(128)
        #		0x0F(15)	Power ON, ALS enable, Proximity enable, Wait disable
        self.bus.write_byte_data(0x39, 0x80, 0x0F)
        # TMG39931 address, 0x39(57)
        # Select ADC integration time register, 0x81(129)
        #		0x00(00)	ATIME : 712ms, Max count = 65535 cycles
        self.bus.write_byte_data(0x39, 0x81, 0x00)
        # TMG39931 address, 0x39(57)
        # Select Wait time register, 0x83(131)
        #		0xFF(255)	WTIME : 2.78ms
        self.bus.write_byte_data(0x39, 0x83, 0xFF)
        # TMG39931 address, 0x39(57)
        # Select Control register, 0x8F(143)
        #		0x00(00)	AGAIN is 1x
        self.bus.write_byte_data(0x39, 0x8F, 0x00)

        data = self.bus.read_i2c_block_data(0x39, 0x94, 9)
        cData = data[1] * 256.0 + data[0]
        red = data[3] * 256.0 + data[2]
        green = data[5] * 256.0 + data[4]
        blue = data[7] * 256.0 + data[6]
        proximity = data[8]


        
        return cData,red,green,blue,proximity
        #reading = Reading(proximity, Measurement.PROXIMITY)
        #logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        #reading = Reading(blue, Measurement.BLUE)
        #logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        #reading = Reading(green, Measurement.GREEN)
        #logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        #reading = Reading(red, Measurement.RED)
        #logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        #reading = Reading(cData, Measurement.INFRARED)
        #logger.info(f"{reading.value},{reading.measurement.unit},{reading.measurement.description}")
        # Output data to screen
        



def main():
    device = TMG39931()

    infrared_sensor = ProximitySensor(device=device)
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
