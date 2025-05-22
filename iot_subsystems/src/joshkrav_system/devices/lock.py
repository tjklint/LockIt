from gpiozero import Servo
from time import sleep
from dataclasses import dataclass
from common.devices.actuator import Action, Command, Actuator


@dataclass
class LockActuator(Actuator):
    """Actuator to control the state of a digital gpio fan"""

    device: Servo
    action: Action
    state: float = 0

    def control_actuator(self, command: Command) -> bool:
        """Function that controls the fan actuator"""
        
        if self.state == 1:
            self.device.min()
            self.state = 0
            command.data = 0
            return True
        else:
            self.device.max()
            self.state = 1
            command.data = 1
            
        return False


TEST_SLEEP_TIME = 1


def main():
    """Routine for testing actuators when this file is run as a script rather than a module."""
    lock = Servo(pin=16)
    lock_actuator = LockActuator(device=lock, action=Action.LOCK_TOGGLE)

    while True:
        lock_on = Command(Action.LOCK_TOGGLE, 1)
        lock_actuator.control_actuator(lock_on)
        sleep(TEST_SLEEP_TIME)


if __name__ == "__main__":
    """
    Usage:
        $ PYTHONPATH=${PYTHONPATH}:src src/hvac_system/devices/fan.py
    Alternatively:
        $ export PYTHONPATH=${PYTHONPATH}:${HOME}/path-to-repository/src
        $ python src/hvac_system/devices/fan.py
    """

    try:
        main()
    except KeyboardInterrupt:
        pass
