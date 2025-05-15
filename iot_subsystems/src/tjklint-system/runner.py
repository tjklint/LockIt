# File: src/example_system/runner.py
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

from tjklint_system.devices.motion import MotionSensor
from tjklint_system.devices.gps import GPSSensor
from common.devices.device_controller import DeviceController
from tjklint_system.system import TJKlintSystem
from tjklint_system.interfaces import TJKlintSystemInterface
from example_system.iot.azure_device_client import AzureDeviceClient

logging.basicConfig(level=logging.DEBUG)
logger = logging.getLogger(__name__)


def main() -> None:
    runtime_environment = dotenv_values(".env")["ENVIRONMENT"]
    sensors = [MotionSensor(), GPSSensor()]
    actuators = []  # Add actuators if needed
    device_controller = DeviceController(sensors=sensors, actuators=actuators)
    interface = TJKlintSystemInterface()
    iot_device_client = AzureDeviceClient()
    system = TJKlintSystem(device_controller, interface, iot_device_client)
    try:
        asyncio.run(system.loop())
    except StopAsyncIteration as e:
        logger.error(e)


if __name__ == "__main__":
    with contextlib.suppress(KeyboardInterrupt):
        main()
