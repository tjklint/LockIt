# File: src/example_system/iot/azure_device_client.py
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

import os
import json
from dotenv import dotenv_values

try:
    from azure.iot.device.aio import IoTHubDeviceClient
    from azure.iot.device import Message
except ImportError:
    IoTHubDeviceClient = None
    Message = None

from common.devices.sensor import Reading
from common.iot import IOTDeviceClient


class AzureDeviceClient(IOTDeviceClient):
    """IOT integrations with Azure Iot Hub."""

    def __init__(self):
        super().__init__()
        connection_string = dotenv_values(".env")["IOTHUB_DEVICE_CONNECTION_STRING"]
        self.device_client = IoTHubDeviceClient.create_from_connection_string(connection_string)

    async def connect(self) -> None:
        """Connects to IoTHub."""
        await self.device_client.connect()



    async def send_picture(self,output_path='output.jpg'):
        pass
    
    async def send_reading(self, reading: Reading) -> None:
        """Sends reading to IoTHub."""
        if self.client is None:
            await self.connect()
        msg = Message(
            json.dumps(
                {"value": reading.value, "measurement": reading.measurement.name}
            )
        )
        msg.custom_properties["measurement"] = reading.measurement.name
        await self.client.send_message(msg)

    async def send_readings(self, readings: list[Reading]) -> None:
        """Sends readings to IoTHub."""
        for reading in readings:
            payload = json.dumps({"measurement": reading.measurement.description, "value": reading.value})
            message = Message(payload)
            await self.device_client.send_message(message)
