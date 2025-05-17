import logging

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
    Default UART: /dev/serial0 (TX=GPIO15, RX=GPIO14)
    """

    def __init__(self, port="/dev/serial0", baudrate=9600, timeout=1):
        if serial is None or not hasattr(serial, "Serial"):
            raise ImportError(
                "pyserial is not installed or not installed correctly. "
                "Install with: pip install pyserial"
            )
        self.ser = serial.Serial(port, baudrate=baudrate, timeout=timeout)

    def read(self):
        """
        Reads from UART and parses NMEA for latitude and longitude.
        Returns (lat, lon) as floats, or (None, None) if not available.
        """
        while True:
            line = self.ser.readline().decode(errors="ignore").strip()
            if line.startswith("$GNGGA") or line.startswith("$GPGGA"):
                try:
                    msg = pynmea2.parse(line)
                    if msg.latitude and msg.longitude:
                        return (msg.latitude, msg.longitude)
                except pynmea2.ParseError:
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

