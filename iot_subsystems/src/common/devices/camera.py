from dataclasses import dataclass
from actuator import Action, Command, Actuator


@dataclass
class MockCamera(Actuator):
    """Mock implementation of the Camera."""

    def __init__(self, action):
        """Initializes the mock class."""
        self.action = action
        self.state = 0

    def picture(self):
        """Simulate taking a picture."""
        print("MockCamera: Picture taken")


    def control_actuator(self, command: Command) -> bool:
        """Simulate processing a camera command."""

        if command.data == 1:
            if self.state == 1 and self.device.value == 1:
                return False
            self.state = 1
            self.picture()
            self.state = 0
            return True
        
def main():
    """Routine for testing actuators when this file is run as a script rather than a module."""
    camera_actuator = MockCamera(action=Action.TAKE_PICTURE)

    take_picture = Command(Action.FAN_TOGGLE, 1)
    result = camera_actuator.control_actuator(take_picture)
    print(f"Command result: {result}")


if __name__ == "__main__":


    try:
        main()
    except KeyboardInterrupt:
        pass