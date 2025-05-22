# Milestone 6: Final Presentation - Feedback

| Team name        | Les habitants               |
| ---------------- | --------------------------- |
| **Grade**        |                             |
| **Team members** | Dylan<br> Timothy<br>Joshua |

## Comments

#### Demo:

- Not ready on time (delay of over 15 minutes)

- Most sensors are represented in the app, missing a few important ones: 
  - GPS
  - Motion sensor
- App UI seems incomplete -2 map not fully implemented.

<u>Camera</u>

- Live feed sent via IoT Hub. A little bit slow
- [x] Camera live updates
- [x] Temperature/humidity

<u>Geolocation</u>

- [ ] GPS location (not displayed but received) -1
- [x] Motion sensor functional and displayed on app

<u>Security</u>

- [x] Servo was updated
- [x] Luminosity  (Red, Green, Blue) functioning
- [ ]  Door sensor (not displayed but received) -1

<u>Nice-to-haves</u>

- Realtime Firebase database of users, guest codes and permissions
- The app from the point of view of the guest this accessible pages are modified by the owner based on the allowed permissions.

<u>Robustness</u>

- Can't undo lockout 
- App doesn't provides proper informative error messages. -1



#### IoT Integration

#### Users 

- Home Owner
- Visitor

### Subsystems

#### Subsystem 1: Camera (Dylan)

- Temperature /Humidity 
- Camera

#### Subsystem 2: Geolocation (TJ)

- GPS
- Motion sensor

#### Subsystem 2: Security (Josh)

- Door sensor 

- Luminosity 

- Servo (lock)

  

## Detailed breakdown

| **Evaluation Criteria**                                      | Grade | **total** |
| ------------------------------------------------------------ | ----- | --------- |
| Final Demo                                                   |       | **5%**    |
| Well prepared and organized                                  | 0.75  | 1         |
| Respecting the time limit                                    | 1     | 1         |
| App is functional (no major crashes)                         | 2     | 2         |
| Clear explanation of design choices                          | 1     | 1         |
| **Project Documentation**                                    |       | **5%**    |
| App Overview & future works                                  |       | 1         |
| App Setup                                                    |       | 2         |
| UML diagram                                                  |       | 2         |
| **App Dev III - App implementation**                         |       | **15%**   |
| IoT Hub Communication                                        | 3     | 5         |
| App Advance Views                                            | 0     | 3         |
| App Robustness<br>App does not crash (Exceptions handled properly)<br>App checks for connectivity <br>App provides proper informative error messages. | 2     | 3         |
| Would-like-to-have features <br>Automated rules <br>Severity metrics <br>Alerts <br>Realtime database | 1     | 1         |
| User Roles (clear separation of views)                       | 1     | 1         |
| Code Quality & Comments                                      |       | 0.75      |
