IoT Device Name: Project_Device

Devices used:
Lock
TMG39931 Sensor
SEC-100 Magnetic Door Sensors
AHT20 Sensor

Connections:
Lock: GPIO OutputDevice, pin 16, analog
TMG39931 Sensor: Temporarily AHT20 sensor mock, address 0x39,bus=1, i2c
SEC-100 Magnetic Door Sensors: Sensor, pin TBD, analog
AHT20 Sensor: AHT20 Grove Class, address 0x38, bus=4, i2c
