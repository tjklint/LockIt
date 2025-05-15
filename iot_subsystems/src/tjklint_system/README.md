# Motion & GPS

## IOT_DEVICE Name
**MotionGPS**

## Subsystem Overview
This subsystem contains two devices connected to the Base Hat:

- **Grove Adjustable PIR Motion Sensor**
- **Grove GPS Air530 Sensor**

These components allow the IOT system to track motion detection and geographical position (latitude & longitude).

## Devices

### 1. Grove Adjustable PIR Motion Sensor

- **Model:** Grove - Adjustable PIR Motion Sensor
- **Documentation:** [Seeed Wiki](https://wiki.seeedstudio.com/Grove-Adjustable_PIR_Motion_Sensor/)
- **Type:** Digital
- **Purpose:** Detects motion using passive infrared

| Signal        | GPIO Pin | Type    | Notes             |
|---------------|----------|---------|-------------------|
| OUT (Motion)  | GPIO12   | Digital | Motion detection output |
| VCC           | 3.3V     | Power   |                   |
| GND           | GND      | Ground  |                   |

### 2. Grove GPS Air530

- **Model:** Grove - GPS (Air530)
- **Documentation:** [Seeed Wiki](https://wiki.seeedstudio.com/Grove-GPS-Air530/)
- **Type:** UART (Serial)
- **Purpose:** Provides location data (latitude and longitude)

| Signal      | GPIO Pin | Type   | Notes                  |
|-------------|----------|--------|------------------------|
| TX (GPS)    | GPIO15   | UART   | TX from GPS to RX Pi   |
| RX (GPS)    | GPIO14   | UART   | RX to GPS from TX Pi   |
| VCC         | 3.3V     | Power  |                        |
| GND         | GND      | Ground |                        |

## Unimplemented Devices

- Both (both devices are currently mocked in development).

## Relevant Issues:

- https://github.com/420-6A6-6P3-W25/final-project-leshabitants/issues/33
- https://github.com/420-6A6-6P3-W25/final-project-leshabitants/issues/34
$$
