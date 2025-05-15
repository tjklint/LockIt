import asyncio
import logging
from common.devices.actuator import Command
from common.devices.device_controller import DeviceController
from common.devices.sensor import Measurement
from common.interfaces import Interface
from common.iot import IOTDeviceClient

logger = logging.getLogger(__name__)

class TJKlintSystem:
    device_controller: DeviceController
    interface: Interface
    iot_device_client: IOTDeviceClient

    is_collecting_readings: bool = True
    telemetry_interval: float = 5
    loop_stop = asyncio.Event()

    def __init__(self, device_controller, interface, iot_device_client):
        self.device_controller = device_controller
        self.interface = interface
        self.iot_device_client = iot_device_client

    def process_command(self, command: Command) -> bool:
        return self.device_controller.control_actuator(command)

    def send_motion(self):
        readings = self.device_controller.read_sensor(Measurement.MOTION)
        for reading in readings:
            asyncio.create_task(self.iot_device_client.send_reading(reading))

    def send_gps(self):
        readings = self.device_controller.read_sensor(Measurement.GPS)
        for reading in readings:
            asyncio.create_task(self.iot_device_client.send_reading(reading))

    def send_custom(self):
        # Implement custom F3 behavior if needed
        logger.info("F3 pressed: No custom action implemented.")

    async def keep_loop_alive_until_cancelled(self) -> None:
        await self.loop_stop.wait()
        raise asyncio.CancelledError

    def end_loop(self) -> None:
        self.loop_stop.set()

    async def collect_readings(self) -> None:
        while self.is_collecting_readings:
            readings = self.device_controller.read_sensors()
            await self.iot_device_client.send_readings(readings)
            await asyncio.sleep(self.telemetry_interval)

    async def loop(self) -> None:
        self.interface.register_callback("control_actuator", self.process_command)
        self.interface.register_callback("send_motion", self.send_motion)
        self.interface.register_callback("send_gps", self.send_gps)
        self.interface.register_callback("send_custom", self.send_custom)
        self.interface.register_callback("end_event_loop", self.end_loop)
        self.interface.register_callback("trigger_gps", self.trigger_gps)
        try:
            async with asyncio.TaskGroup() as tg:
                tg.create_task(self.collect_readings())
                tg.create_task(self.interface.event_loop())
                await self.keep_loop_alive_until_cancelled()
        except asyncio.CancelledError:
            logger.info("Script exiting.")

    def trigger_gps(self):
        readings = self.device_controller.read_sensor(Measurement.GPS)
        for reading in readings:
            asyncio.create_task(self.iot_device_client.send_reading(reading))
