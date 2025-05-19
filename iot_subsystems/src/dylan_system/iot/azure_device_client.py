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

from common.devices.sensor import Reading
from common.iot import IOTDeviceClient
from dotenv import dotenv_values
from azure.iot.device.aio import IoTHubDeviceClient
from azure.iot.device import Message
import json
import os
from os.path import basename
import aiohttp

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
        if not os.path.exists(output_path):
            print("image not found")
            return
        
        blob_info = await self.device_client.get_storage_info_for_blob(basename(output_path))


        sas_url = (
            f"https://{blob_info['hostName']}/{blob_info['containerName']}/"
            f"{blob_info['blobName']}{blob_info['sasToken']}"
        )

        try:
            async with aiohttp.ClientSession() as session:
                with open(output_path, 'rb') as image_file:
                    headers = {"x-ms-blob-type": "BlockBlob"}
                    async with session.put(sas_url, data=image_file, headers=headers) as response:
                        success = response.status == 201
                        print(f"Upload response status: {response.status}")
        except Exception as e:
            success = False
            print(f"Upload failed with exception: {e}")

        await self.device_client.notify_blob_upload_status(
            correlation_id=blob_info["correlationId"],
            is_success=success,
            status_code=response.status if success else 500,
            status_description="Upload complete" if success else "Upload failed"
        )
        if success:
            message = {
                "imageUploaded": True,
                "blobName": blob_info["blobName"],
                "sasUrl": sas_url
            }
            await self.device_client.send_message(json.dumps(message))



    async def send_reading(self, reading: Reading) -> None:
        """Sends reading to IoTHub."""
        await self.device_client.send_message(reading.value)

    async def send_readings(self, readings: list[Reading]) -> None:
        """Sends readings to IoTHub."""
        for reading in readings:
            payload = json.dumps({"measurement": reading.measurement.description, "value": reading.value})
            message = Message(payload)
            await self.device_client.send_message(message)
