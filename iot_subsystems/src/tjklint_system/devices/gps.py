import logging
import random

try:
    import serial
except ImportError:
    serial = None
import pynmea2
from common.devices.sensor import Sensor, Measurement, Reading

logger = logging.getLogger(__name__)


class GPSDeviceUART:
    """
    Reads NMEA sentences from Grove GPS (Air530) via UART on Raspberry Pi.
    Default UART: /dev/ttyS0 (TX=GPIO15, RX=GPIO14)
    """

    def __init__(self, port="/dev/ttyS0", baudrate=9600, timeout=1):
        if serial is None or not hasattr(serial, "Serial"):
            raise ImportError(
                "pyserial is not installed or not installed correctly. "
                "Install with: pip install pyserial"
            )
        try:
            self.ser = serial.Serial(port, baudrate=baudrate, timeout=timeout)
            self.ser.reset_input_buffer()
            self.ser.flush()
        except PermissionError as e:
            import getpass
            import os
            user = getpass.getuser()
            groups = os.popen(f"groups {user}").read()
            logger.error(
                f"Permission denied opening {port}. "
                f"Current user: {user}\n"
                f"Groups: {groups}\n"
                "Make sure your user is in the 'dialout' group and you have logged out and back in.\n"
                "If this still fails, try running with 'sudo' as a last resort."
            )
            raise
        except serial.SerialException as e:
            raise RuntimeError(f"Could not open serial port {port}: {e}") from e

    def read(self):
        """
        Reads from UART and parses NMEA for latitude and longitude.
        Returns (lat, lon) as floats, or (None, None) if not available.
        """
        while True:
            try:
                line = self.ser.readline().decode("utf-8")
            except UnicodeDecodeError:
                logger.warning("UnicodeDecodeError encountered")
                continue
            if "GGA" in line:
                try:
                    msg = pynmea2.parse(line)
                    if msg.latitude and msg.longitude:
                        return (msg.latitude, msg.longitude)
                except pynmea2.ParseError as e:
                    logger.warning(f"Parse error: {e}")
                    continue
            # Optionally, break after N attempts or timeout


class GPSSensor(Sensor):
    device: GPSDeviceUART
    measurement: Measurement

    def __init__(self, device=None):
        self.device = device or GPSDeviceUART()
        self.measurement = Measurement.GPS

    def read_sensor(self) -> Reading:
        lat, lon = self.device.read()
        value = {"lat": lat, "lon": lon}
        reading = Reading(value=value, measurement=self.measurement)
        logger.info(reading)
        return reading

class MockGPSDevice:
    """
    Simulates a GPS device by returning random latitude and longitude near Montreal.
    """

    def read(self):
        lat = round(random.uniform(45.4, 45.6), 6)
        lon = round(random.uniform(-73.7, -73.5), 6)
        logger.debug(f"[MOCK GPS] lat={lat}, lon={lon}")
        return lat, lon