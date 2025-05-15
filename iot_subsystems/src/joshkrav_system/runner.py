# File: src/dylan_system/runner.py
# Project: final-project-upstream
# Creation date: 29 Apr 2025
# Author: michaelhaaf <michael.haaf@gmail.com>
# Modified By: N/A
# Changes made: N/A
# -----
# This software is intended for educational use by students and teachers in the
# the Computer Science department at John Abbott College.
# See the license disclaimer below and the project LICENSE file for more information.
# -----
# Copyright (C) 2025 michaelhaaf
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program. If not, see <https://www.gnu.org/licenses/>.

import asyncio
import contextlib
import logging

from dotenv import dotenv_values
<<<<<<<< HEAD:iot_subsystems/src/dylan_system/runner.py

from common.devices.device_controller import DeviceController
from dylan_system.devices.aht20 import HumiditySensor, MockGroveTemperatureHumidityAHT20, TemperatureSensor
from dylan_system.example_system import ExampleSystem
from dylan_system.interfaces import (
    ExampleSystemKeyboardInterface,
    ExampleSystemReterminalInterface,
)
from dylan_system.iot.azure_device_client import AzureDeviceClient

from dylan_system.devices.camera import MockCamera, CameraActuator
from common.devices.actuator import Action
========
from gpiozero import OutputDevice
from gpiozero.pins.mock import MockFactory
from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20
from joshkrav_system.devices.lock import LockActuator
from common.devices.device_controller import DeviceController
from common.devices.mocks import MockTMG39931, MockDoorSensor
from joshkrav_system.devices.tmg39931 import (
    ProximitySensor,
    RedColorSensor,
    BlueColorSensor,
    ProximitySensor,
    GreenColorSensor,
)
from common.devices.actuator import Action
from joshkrav_system.devices.aht20 import (
    HumiditySensor,
    MockGroveTemperatureHumidityAHT20,
    TemperatureSensor,
)
from joshkrav_system.devices.fan import FanActuator
from joshkrav_system.example_system import ExampleSystem
from joshkrav_system.interfaces import (
    ExampleSystemKeyboardInterface,
    ExampleSystemReterminalInterface,
)
from joshkrav_system.iot.azure_device_client import AzureDeviceClient
>>>>>>>> main:iot_subsystems/src/joshkrav_system/runner.py

logging.basicConfig(level=logging.DEBUG)
logger = logging.getLogger(__name__)

# https://www.gnu.org/licenses/gpl-3.0.html#howto
LICENSE_NOTICE = """
example_system: Copyright (C) 2025 michaelhaaf
This program comes with ABSOLUTELY NO WARRANTY.
This is free software, and you are welcome to redistribute it
under certain conditions; for details see LICENSE packaged with source code.
"""


def main() -> None:
    """Routine for running system from cli."""
    runtime_environment = dotenv_values(".env")["ENVIRONMENT"]
    if runtime_environment == "DEVELOPMENT":
        interface = ExampleSystemKeyboardInterface()
        camera = MockCamera()
        aht20 = MockGroveTemperatureHumidityAHT20()
<<<<<<<< HEAD:iot_subsystems/src/dylan_system/runner.py
    elif runtime_environment == "PRODUCTION":
        interface = ExampleSystemReterminalInterface()
        # todo
    else:
        raise ValueError

    sensors = [        
        TemperatureSensor(device=aht20),
        HumiditySensor(device=aht20),]
    actuators = [CameraActuator(camera, Action.TAKE_PICTURE)]
========
        luminosity_sensor = MockTMG39931()
        lock = OutputDevice(pin=16, pin_factory=MockFactory())
        door_sensor = MockDoorSensor()
    elif runtime_environment == "PRODUCTION":
        interface = ExampleSystemReterminalInterface()
        aht20 = GroveTemperatureHumidityAHT20(address=0x38, bus=4)
        lock = OutputDevice(pin=16)
    else:
        raise ValueError

    sensors = [
        TemperatureSensor(device=aht20),
        HumiditySensor(device=aht20),
        ProximitySensor(device=luminosity_sensor),
        RedColorSensor(device=luminosity_sensor),
        GreenColorSensor(device=luminosity_sensor),
        BlueColorSensor(device=luminosity_sensor),
        ProximitySensor(device=luminosity_sensor),
        MockDoorSensor(),
    ]
    actuators = [LockActuator(device=lock, action=Action.LOCK_TOGGLE)]
>>>>>>>> main:iot_subsystems/src/joshkrav_system/runner.py

    device_controller = DeviceController(sensors=sensors, actuators=actuators)
    system = ExampleSystem(
        device_controller=device_controller,
        interface=interface,
        iot_device_client=AzureDeviceClient(),
    )

    try:
        print(LICENSE_NOTICE)
        asyncio.run(system.loop())
    except StopAsyncIteration as e:
        logger.error(e)  # noqa: TRY400


if __name__ == "__main__":
    with contextlib.suppress(KeyboardInterrupt):
        main()
