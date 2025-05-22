from dataclasses import dataclass
import time
from common.devices.actuator import Command, Actuator
import logging
import subprocess
from azure.iot.device import Message
import os
logger = logging.getLogger(__name__)


class Camera:
    """Implementation of the Camera."""

    def __init__(self, output_path='output.jpg'):
        """Initializes the mock class."""
        self.state = 0
        self.output_path = output_path
        
    def take_picture(self):
        "Takes a picture"
        while True:

            if os.path.exists(self.output_path):
                os.remove(self.output_path)
                
            # subprocess.run(["v4l2-ctl", "-d", "/dev/video0", "-c", "exposure_auto=1"])
            # subprocess.run(["v4l2-ctl", "-d", "/dev/video0", "-c", "exposure_absolute=800"])
            # subprocess.run(["v4l2-ctl", "-d", "/dev/video0", "-c", "brightness=128"])
            # subprocess.run(["v4l2-ctl", "-d", "/dev/video0", "-c", "gain=20"])
            
            command = [
                'ffmpeg',
                '-f', 'v4l2',
                '-video_size', '640x480',
                '-i', '/dev/video0',
                '-frames:v', '1',
                '-vf', 'scale=in_range=limited:out_range=full,eq=brightness=0.06:contrast=1.2',
                '-pix_fmt', 'yuvj422p',
                'output.jpg'
            ]


            subprocess.run(command)
            time.sleep(5)
        



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
