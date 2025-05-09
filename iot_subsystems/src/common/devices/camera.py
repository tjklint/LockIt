class MockCamera(Actuator):
    """Mock implementation of the Camera."""

    def __init__(self):
        """Initializes the mock class."""
        self.device = None
        self.action = Action.TAKE_PICTURE
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