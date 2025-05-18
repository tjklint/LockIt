# File: src/example_system/interfaces/__init__.py
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

import logging

from common.devices.actuator import Action, Command
from common.interfaces import Interface

logger = logging.getLogger(__name__)


class TJKlintSystemInterface(Interface):
    """Custom interface for tjklint-system."""

    def key_press(self, key: str) -> None:
        if key.upper() == "F1":
            command = Command(Action.MOTION_TOGGLE, 1)
            command = Command(Action.TAKE_PICTURE, 1)
            command = Command(Action.LOCK_TOGGLE, 1)
            self.callbacks["control_actuator"](command)
        elif key.upper() == "F2":
            # Trigger GPS reading (calls a callback if registered)
            if "trigger_gps" in self.callbacks:
                self.callbacks["trigger_gps"]()
            else:
                logger.info("F2 pressed: GPS reading requested")
        elif key.upper() == "F3":
            logger.info("F3 pressed: Custom action")
        elif key.upper() == "O":
            logger.info("'O' pressed, script will exit when released")
        else:
            logger.debug(f"{key} pressed")

    def key_release(self, key: str) -> None:
        if key.upper() == "F1":
                command = Command(Action.MOTION_TOGGLE, 0)
                command = Command(Action.TAKE_PICTURE, 0)

                command = Command(Action.LOCK_TOGGLE, 0)

                self.callbacks["control_actuator"](command)
        elif key.upper() == "F2":
            logger.info("F2 released")
        elif key.upper() == "F3":
            logger.info("F3 released")
        elif key.upper() == "O":
            logger.info("'O' released, script exiting.")
            self.callbacks["end_event_loop"]()
        else:
            logger.debug(f"{key} released")

    async def event_loop(self) -> None:
        import asyncio

        while True:
            await asyncio.sleep(1)

    def end_event_loop(self) -> None:
        pass
