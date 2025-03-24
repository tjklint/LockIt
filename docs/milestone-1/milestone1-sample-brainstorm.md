# Project Brainstorming

## Purpose

Our project aims to create a smart doorbell and security system that enhances home security while providing real-time environmental insights. The system features a motion-activated doorbell camera and a smart lock that can be controlled remotely via a mobile app. In addition to security, the app will display key environmental data such as luminosity, humidity, and temperature.

## Subsystems

### Subsystem 1

This subsystem is responsible for monitoring the front door area using a motion-activated camera and sensor. It enhances home security by detecting movement and capturing footage.

#### Devices

| Component Name   | Interface Type   | Documentation Link |
|-----------------|-----------------|--------------------|
| Camera         | USB      | TBD  |
| Motion Sensor  | GPIO / I2C       | TBD  |
| GPS | GPIO / I2C |

### Subsystem 2

This subsystem is responsible for collecting real-time environmental data, including temperature, luminosity, and humidity. It enhances the smart doorbell system by providing homeowners with insights into outdoor conditions, allowing them to make informed decisions about their home environment.

#### Devices

| Component Name      | Interface Type | Documentation Link |
|--------------------|---------------|--------------------|
| Temperature Sensor | PIN           | TBD |
| Luminosity Sensor  | BUS           | TBD |
| Humidity Sensor    | PIN           | TBD |

### Subsystem 3

This subsystem manages the smart lock mechanism, allowing users to remotely control access to their home via the mobile app.

#### Devices

| Component Name | Interface Type | Documentation Link |
|---------------|---------------|--------------------|
| Lock         | GPIO / I2C     | TBD  |
| Motor        | PWM / GPIO     | TBD  |

