<!-- README HEADER -->

<h1 align="center">
  LockIt
</h1>

<!--<div align="center">
  <img src="https://github.com/your-repo/demo.gif" alt="Smart Doorbell Demo">
</div>--->

<h3 align="center">
  Smart Doorbell and Security System with Environmental Monitoring
</h3>

<h4 align="center">
Technologies used:
</h4>

<p align="center">
    <img src="https://img.shields.io/badge/python-%2314354C.svg?style=for-the-badge&logo=python&logoColor=white" alt="Python">
    <img src="https://img.shields.io/badge/raspberry%20pi-%23C51A4A.svg?style=for-the-badge&logo=raspberry-pi&logoColor=white" alt="Raspberry Pi">
    <img src="https://img.shields.io/badge/IOT-%23000000.svg?style=for-the-badge&logo=iot&logoColor=white" alt="IoT">
    <img src="https://img.shields.io/badge/firebase-%23039BE5.svg?style=for-the-badge&logo=firebase" alt="Firebase">
    <img src="https://img.shields.io/badge/mobile%20app-%230A66C2.svg?style=for-the-badge&logo=android&logoColor=white" alt="Mobile App">
</p>


## 🏠 **Project Description**

LockIt is a smart doorbell and remote lock system that empowers homeowners to monitor and control access to their property from anywhere. This system bridges a mobile application with a Raspberry Pi reTerminal using Microsoft Azure IoT Hub. Sensor data and lock commands are exchanged through Azure, allowing real-time monitoring and control of the physical device from a clean .NET MAUI interface.

The backend, built in Python, runs on the reTerminal and communicates with various sensors and actuators (like motion sensors and a servo motor acting as the door lock), constantly pushing telemetry and awaiting direct method calls.

<br/>

## 🛠️ **Contributions**

| Developer             | Contribution                          |
|-----------------------|----------------------------------------|
| **[Timothy Klint](https://github.com/tjklint)** | *System design, Python backend, .NET MAUI integration* |
| **Dylan Savelson**    | *Sensor integration & actuator control* |
| **Joshua Kravitz**    | *Locking subsystem & cloud messaging setup* |

> 💡 While each team member had a primary focus, we all collaborated closely on all aspects of the project.

<br/>

## 🧰 **Technologies Used**

**Application Development 3:**
- .NET MAUI (C#)
- Visual Studio 2022

**Connected Objects:**
- Python 3.11
- Azure IoT Hub
- Raspberry Pi reTerminal
- VS Code, GPIOZero, asyncio

<br/>

## 🔐 **Hardware Setup**

| Sensor/Actuator | Pin Used | Function                    |
|-----------------|----------|-----------------------------|
| Motion Sensor   | GPIO 22  | Detects nearby movement     |
| GPS Module      | /dev/ttyS0 | Sends real-time location     |
| Door Sensor     | GPIO 18  | Detects door open/close     |
| Lock (Servo)    | GPIO 16  | Opens/closes lock mechanism |

<br/>

## 🔗 Azure Configuration

1. **IoT Hub Setup**
   - Create an IoT Hub on Azure
   - In “Built-in endpoints,” copy the Event Hub-compatible endpoint and name
   - In “Shared access policies,” grab the `iothubowner` connection string

2. **Device Registration**
   - Register a device in the IoT Hub
   - Copy the device connection string for your `.env` file on the reTerminal

3. **Required .env (reTerminal)**
```env
IOTHUB_DEVICE_CONNECTION_STRING="HostName=yourhub.azure-devices.net;DeviceId=LockItDevice;SharedAccessKey=..."
ENVIRONMENT=PRODUCTION
```

4. **Required `appsettings.json` (.NET MAUI App)**
```json
{
  "Settings": {
    "EventHubConnectionString": "Endpoint=sb://...;SharedAccessKey=...;EntityPath=...",
    "EventHubName": "LockItHub",
    "IotHubConnectionString": "HostName=yourhub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=..."
  }
}
```

<br/>

## 💬 **Cloud-to-Device Methods**

| Method Name   | Payload Example              | Description                 |
|---------------|------------------------------|-----------------------------|
| `toggle_lock` | `{"value": 1}`               | Locks/unlocks the door      |
| `is_online`   | none                          | Returns 200 OK if online    |

Example:
```bash
az iot hub invoke-device-method --hub-name LockItHub --device-id LockItDevice --method-name toggle_lock --method-payload '{"value": 1}'
```

<br/>

## 📡 Sensor Telemetry

Sensor readings (motion, GPS) are sent every 5 seconds by default. The logger writes this data to `readings.csv`:

```
2025-05-22 12:45:33,True,,Motion Sensor Triggered
2025-05-22 12:45:38,45.5017,-73.5673,Latitude/Longitude
```

Enable sensor logging in `runner.py`:
```python
logging.basicConfig(
    level=logging.DEBUG,
    format="%(asctime)s,%(message)s",
    filename="readings.csv",
)
```

<br/>

## 📱 Mobile App Features

- 🔐 Toggle lock remotely
- 📍 View GPS location of your doorbell
- 📊 View motion history
- 🧾 See logs and sensor status
- 🔒 Role-based access: homeowners vs visitors

<br/>

## 🧪 Testing & Validation

All sensors and actuators were tested on a Raspberry Pi reTerminal powered by a power bank. Logs were collected outdoors to validate GPS accuracy and motion responsiveness.

> ✅ Direct method `is_online` must return 200 OK and be visible in the reTerminal console

<br/>

## 🗂️ UML, Wireframes & Docs

classDiagram

class Surveillance {
  -int _motionSensorPin
  -int _camera
  -GpioController _motionSensor
  -int _GPS
  +int Camera
  +GpioController MotionSensor
  +int GPS
  +bool IsMotion()
  +void GetGPSData()
  +VideoCaptureDevice GetCamera()
}

class SecurityModel {
  -int _lockPin
  -bool _isLocked
  -bool _isClosed
  +bool IsLocked
  +bool IsClosed
  +void Unlocking()
  +void Locking()
}

class FirebaseAuthResponse {
  +string idToken
  +string email
  +string refreshToken
  +string expiresIn
  +string localId
  +string Error
}

class EnvironmentalSensor {
  -double _temperatureSensor
  -double _humiditySensor
  -LuminositySensor _luminositySensor
  +double TemperatureSensor
  +double HumiditySensor
  +LuminositySensor LuminositySensor
}

class LuminositySensor {
  -double _infraRed
  -double _green
  -double _blue
  -double _red
  -double _proximity
  +double infraRed
  +double Green
  +double Blue
  +double Red
  +double Proximity
}

class User {
  +string Uid
  +string Email
  +string DisplayName
}

Surveillance --> GpioController : uses
Surveillance --> VideoCaptureDevice : returns
SecurityModel --> GpioController : uses
EnvironmentalSensor --> LuminositySensor : has
```


<h6 align="center">
  Like what you see? Give us a ⭐ to support our work!  
</h6>

FIGMA LINK: https://www.figma.com/design/uf5WlLdbZZYCgFgUs9wD4E/Untitled?node-id=0-1&t=dAYyFkfrFAsHoqCr-1
