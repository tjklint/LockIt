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

import asyncio
import json
from common.devices.sensor import Reading
from common.iot import IOTDeviceClient
from dotenv import dotenv_values
from azure.iot.device.aio import IoTHubDeviceClient
from azure.iot.device import Message, MethodResponse
import json


class AzureDeviceClient(IOTDeviceClient):
    """IOT integrations with Azure Iot Hub."""

    def __init__(self):
        super().__init__()
        connection_string = dotenv_values(".env")["IOTHUB_DEVICE_CONNECTION_STRING"]
        self.device_client = IoTHubDeviceClient.create_from_connection_string(connection_string)    
        self.control_actuator_callback = None

    async def connect(self) -> None:
        """Connects to IoTHub."""
        await self.device_client.connect()
        self.device_client.on_method_request_received = self.method_handler

    async def send_reading(self, reading: Reading) -> None:
        """Sends reading to IoTHub."""
        await self.device_client.send_message(reading.value)

    async def send_picture(self) -> None:
        pass

    async def send_readings(self, readings: list[Reading]) -> None:
        """Sends readings to IoTHub."""
        for reading in readings:
            payload = json.dumps({"measurement": reading.measurement.description, "value": reading.value})
            message = Message(payload)
            await self.device_client.send_message(message)


        for reading in readings:
            payload = json.dumps({"measurement": reading.measurement.description, "value": reading.value})
            await self.device_client.send_message(Message(payload))

    async def method_handler(self, method_request):
        if method_request.name == "toggle_lock":
            try:
                print(f"Received method request: {method_request.name}")
                data = json.loads(method_request.payload) if method_request.payload else {}
                value = data.get("value", 0)
                
                print(f"Callback assigned? {self.control_actuator_callback is not None}")

                if self.control_actuator_callback:
                    from common.devices.actuator import Action, Command
                    command = Command(Action.LOCK_TOGGLE, value)
                    result = self.control_actuator_callback(command)
                    print (f"Result of control actuator callback: {result}")
                    print (f"Command: {command}")

                response_payload = {"result": "Lock toggled", "value": value}
                method_response = MethodResponse.create_from_method_request(method_request, 200, response_payload)
            except Exception as e:
                method_response = MethodResponse.create_from_method_request(method_request, 500, {"error": str(e)})
        else:
            method_response = MethodResponse.create_from_method_request(method_request, 404, {"error": "Unknown method"})

        await self.device_client.send_method_response(method_response)
