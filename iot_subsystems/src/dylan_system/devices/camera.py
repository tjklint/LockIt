from dataclasses import dataclass
from common.devices.actuator import Command, Actuator
import logging
import subprocess

logger = logging.getLogger(__name__)


class Camera:
    """Implementation of the Camera."""

    def __init__(self):
        """Initializes the mock class."""
        self.state = 0
        
    def take_picture(self):
        "Takes a picture"
        command = [
        'ffmpeg',
        '-f', 'v4l2',
        '-video_size', '640x480',
        '-i', '/dev/video0',
        '-frames:v', '1',
        'output.jpg'
        ]

        subprocess.run(command)

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
