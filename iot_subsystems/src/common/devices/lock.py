from gpiozero import OutputDevice
from time import sleep
from dataclasses import dataclass
from actuator import Action, Command, Actuator


@dataclass
class LockActuator(Actuator):
    """Actuator to control the state of a digital gpio fan"""
    device: OutputDevice
    action: Action
    state: float = 0

    def control_actuator(self, command: Command) -> bool:
        """Function that controls the fan actuator"""
        print(command)
        return False


TEST_SLEEP_TIME = 1


def main():
    """Routine for testing actuators when this file is run as a script rather than a module."""
    lock = OutputDevice(pin=16)
    lock_actuator = LockActuator(device=lock, action=Action.LOCK_TOGGLE)

    lock_on = Command(Action.LOCK_TOGGLE, 1)
    lock_off = Command(Action.LOCK_TOGGLE, 0)
    lock_actuator.control_actuator(lock_on)
    sleep(TEST_SLEEP_TIME)
    lock_actuator.control_actuator(lock_on)
    sleep(TEST_SLEEP_TIME)
    lock_actuator.control_actuator(lock_off)
    sleep(TEST_SLEEP_TIME)
    lock_actuator.control_actuator(lock_off)
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