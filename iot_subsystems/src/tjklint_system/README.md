## 📡 Geolocation + Motion Subsystem Device Documentation

Below is a reference guide for connecting your GPS and motion detection hardware components to ensure proper functionality in the field.

| Sensor/Actuator         | Port on Grove Base Hat | Port Type | Unit |
|-------------------------|------------------------|-----------|------|
| Motion Sensor (PIR)     | 22                     | PIN       | N/A  |
| GPS Module              | /dev/ttyS0             | UART      | N/A  |

---

## 🔐 IoT Device Configuration: Project_Device

This configuration outlines the essential device mappings and communication settings for the `Project_Device` unit, focusing on security and location subsystems.

- **Devices Used:**
  - Lock
  - TMG39931 Sensor

- **Connections:**
  - 🔓 **Lock** — GPIO Servo Device, **Pin 16**, *Analog*
  - 🌈 **TMG39931 Sensor** — I2C Sensor Class, *Address 0x39*, *Bus 1*

- **Known Issues:**
  - #34: [https://github.com/420-6A6-6P3-W25/final-project-leshabitants/issues/34 ](https://github.com/420-6A6-6P3-W25/final-project-leshabitants/issues/34)
  - #33: [https://github.com/420-6A6-6P3-W25/final-project-leshabitants/issues/33](https://github.com/420-6A6-6P3-W25/final-project-leshabitants/issues/33)
