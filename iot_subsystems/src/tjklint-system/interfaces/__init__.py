import logging
from common.interfaces import Interface

logger = logging.getLogger(__name__)

class TJKlintSystemInterface(Interface):
    """Custom interface for tjklint-system."""

    def key_press(self, key: str) -> None:
        if key.upper() == "F1":
            if "send_motion" in self.callbacks:
                self.callbacks["send_motion"]()
        elif key.upper() == "F2":
            if "send_gps" in self.callbacks:
                self.callbacks["send_gps"]()
        elif key.upper() == "F3":
            if "send_custom" in self.callbacks:
                self.callbacks["send_custom"]()
        elif key.upper() == "O":
            logger.info("'O' pressed, script will exit when released")
        else:
            logger.debug(f"{key} pressed")

    def key_release(self, key: str) -> None:
        if key.upper() == "O":
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
