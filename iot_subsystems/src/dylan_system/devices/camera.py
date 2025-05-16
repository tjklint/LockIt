from dataclasses import dataclass
from common.devices.actuator import Command, Actuator
import logging

logger = logging.getLogger(__name__)


class Camera:
    """Implementation of the Camera."""

    # todo


class MockCamera:
    """Mock implementation of the Camera."""

    def __init__(self):
        """Initializes the mock class."""
        self.state = 0

    def take_picture(self):
        "Takes a picture"
        logger.info("picture taken")


@dataclass
class CameraActuator(Actuator):
    """Mock implementation of the Camera."""

    def __init__(self, device, action):
        """Initializes the class."""
        self.device = device  # ffmpeg interface
        self.action = action

    def control_actuator(self, command: Command) -> bool:
        """Simulate processing a camera command."""

        if command.data == 1:
            if self.device.state == 1 and self.device.value == 1:
                return False
            self.device.state = 1
            self.device.take_picture()
            self.device.state = 0
            return True
