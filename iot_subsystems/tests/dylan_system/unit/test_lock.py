# File: tests/dylan_system/unit/test_fan.py
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

from common.devices.actuator import Action, Command
from dylan_system.devices.fan import FanActuator
from common.devices.lock import LockActuator


def test_control_actuator_return_true_off_to_on(lock_actuator: LockActuator):
    lock_on = Command(Action.LOCK_TOGGLE, 1)
    assert lock_actuator.control_actuator(lock_on)
    assert lock_actuator.device.value == 1


def test_control_actuator_return_true_on_to_off(lock_actuator: LockActuator):
    lock_on = Command(Action.LOCK_TOGGLE, 1)
    lock_off = Command(Action.LOCK_TOGGLE, 0)
    lock_actuator.control_actuator(lock_on)
    assert lock_actuator.control_actuator(lock_off)
    assert lock_actuator.device.value == 0


def test_control_actuator_return_false_on_to_on(lock_actuator: LockActuator):
    lock_on = Command(Action.LOCK_TOGGLE, 1)
    lock_actuator.control_actuator(lock_on)
    assert not lock_actuator.control_actuator(lock_on)
    assert lock_actuator.device.value == 1


def test_control_actuator_return_false_off_to_off(lock_actuator: LockActuator):
    lock_off = Command(Action.LOCK_TOGGLE, 0)
    lock_actuator.control_actuator(lock_off)
    assert not lock_actuator.control_actuator(lock_off)
    assert lock_actuator.device.value == 0
