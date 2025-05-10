# File: tests/example_system/unit/test_aht20.py
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

from joshkrav_system.devices.aht20 import HumiditySensor, TemperatureSensor


def test_temperature_sensor_read_sensor_returns_reading(
    temperature_sensor: TemperatureSensor,
):
    reading = temperature_sensor.read_sensor()
    assert isinstance(reading.value, float)
    assert reading.measurement == temperature_sensor.measurement


def test_humidity_sensor_read_sensor_returns_reading(humidity_sensor: HumiditySensor):
    reading = humidity_sensor.read_sensor()
    assert isinstance(reading.value, float)
    assert reading.measurement == humidity_sensor.measurement
