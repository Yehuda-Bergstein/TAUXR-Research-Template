

---

## 📊 Data Management

### 📝 Overview

The **TAUXR Data Management System** is a modular data collection and export package designed to log VR session data for research and experiments. It supports both **continuous data tracking** (e.g., player movement, hand tracking) and **event-based analytics** (e.g., participant actions).

Data is saved **locally** on the device (`Application.persistentDataPath`) as CSV files.
In the future, export to remote servers is planned.

### 🔎 Data Types

| Type       | Description                                                                                        | Example                                           |
| ---------- | -------------------------------------------------------------------------------------------------- | ------------------------------------------------- |
| Continuous | Logged every frame or every X frames. Used for movement, rotation, eye tracking, face expressions. | Player position, Head rotation, Eye gaze          |
| Analytics  | Logged on specific frames when an event occurs. Used for participant actions.                      | Reached location, Pressed button, Completed trial |

The system allows flexible extension to add custom data fields or remove default fields.

---

### 💻 Required Components in the Scene

All data logging is handled by the **`TXR_DataManager (Prefab)`**, which you must add to your scene.
This prefab includes all required scripts and manages initialization automatically.

| Script                                    | Purpose                                                                                                          |
| ----------------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| `TXRDataManager`                          | Master controller. Manages data initialization and logging. Automatically links to player data from `TXRPlayer`. |
| `DataContinuousWriter`                    | Continuously logs headset, hand, and eye tracking positions and rotations to CSV.                                |
| `DataExporterFaceExpression` *(optional)* | Logs Meta Quest facial expression blendshape data to CSV if face tracking is enabled.                            |

> ✅ **Note:** The `TXR_DataManager` prefab is designed to be universal across projects. It automatically connects to `TXRPlayer.Instance` at runtime for all tracked data references. No manual assignment is needed.

> ✅ **Note:** You do not need to create or manage writer scripts manually. The prefab contains the complete system.

---

### 🎮 Individual Script Responsibilities

#### `TXRDataManager`

* Central controller of the entire data system.
* Declares and manages all `AnalyticsDataClass` event instances.
* Provides public methods to log data for any custom analytics table.
* Writes analytics events to file via `AnalyticsWriter`.
* Starts continuous tracking via `DataContinuousWriter`.
* Starts face tracking logging (optional) via `DataExporterFaceExpression`.
* **Automatically links to player data from `TXRPlayer.Instance` at initialization.**

Internal logic:

* In build: data export is always active.
* In editor: data export only if `shouldExport` is manually enabled.

#### `DataContinuousWriter`

* Records player movement and orientation:

  * Head position & rotation (from `TXRPlayer.PlayerHead`)
  * Left and Right hand positions & rotations (from `TXRPlayer.LeftHand`, `TXRPlayer.RightHand`)
  * (Optional) Eye gaze focused object, eye gaze hit position, eye rotations (from `TXRPlayer`)
  * Any custom Transforms added by user
* Logs to file every frame or at specified interval (`logFrequency`).
* **Eyes:** Only logs eye rotations, not world positions.
* **Focused Object:** If eye tracking is active, raycasts from right eye and logs hit object name.

#### `DataExporterFaceExpression` *(optional)*

* Records Meta Quest face tracking blendshape weights every frame.
* Each frame logs values for all supported expressions.
* **Pulls face tracking data from `TXRPlayer.OVRFace`.**

#### `AnalyticsWriter`

* Receives `AnalyticsDataClass` objects.
* Creates CSV files per event type.
* Writes column headers (dynamically detected via reflection).
* Appends new event lines to correct file.

---

### 🛠️ How to Add New Analytics Events

1. **Create a new class that implements `AnalyticsDataClass`**
   This class represents **1 row of data** in your CSV.

```csharp
[Serializable]
public class MyEvent : AnalyticsDataClass
{
    public string TableName => "MyCustomEvents";
    public float TimeOfEvent;
    public string EventName;

    public MyEvent(string name)
    {
        TimeOfEvent = Time.time;
        EventName = name;
    }
}
```

2. **Declare it in `TXRDataManager`**

```csharp
private MyEvent myEvent;
```

3. **Create a public method to log it**

```csharp
public void LogMyEvent(string name)
{
    myEvent = new MyEvent(name);
    WriteAnalyticsToFile(myEvent);
}
```

4. **Call your logging method from anywhere in the project**

```csharp
TXRDataManager.Instance.LogMyEvent("Player entered zone A");
```

👉 All writing, table creation and header management is automatic.

---

### ✅ Summary of Current Default Tracked Data

| Data                                                | Fields                                                                |
| --------------------------------------------------- | --------------------------------------------------------------------- |
| Player Head (`TXRPlayer.PlayerHead`)                | Position (x, y, z), Rotation (pitch, yaw, roll)                       |
| Hands (`TXRPlayer.LeftHand`, `TXRPlayer.RightHand`) | Left + Right position + rotation                                      |
| Eyes (`TXRPlayer`, optional)                        | Focused object name, Eye gaze hit position, Right & Left eye rotation |
| Face (`TXRPlayer.OVRFace`, optional)                | All Meta blendshape weights                                           |
| Additional Transforms                               | Any added via `AdditionalTransforms[]` in `DataContinuousWriter`      |
| Analytics Events                                    | Any custom `AnalyticsDataClass` you define                            |

---


