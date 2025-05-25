# Getting Started: Base Scene

The Base Scene provides a complete setup for developing VR experiences in this project. It includes the core XR rig, input handling, data recording, scene management, and calibration components. All input and data logging systems are already initialized in this scene.

## Scene Contents

### [TXR\_Player (Prefab)](https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Docs/TxrPlayer.md)

The XR camera rig. Includes:

* `TXRPlayer` script: a configured TXRPlayer instance that manages player tracking, hand tracking (`TXRHand`), controller input, and eye tracking (`TXREyeTracker`).
*  `OVRFaceExpressions` component: enables face tracking on quest pro devices.
* `PositionSetter`: allows switching between predefined starting positions for testing.

### [TXR\_DataManager (Prefab)](https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Docs/TXR%20Data%20Manager.md)

Handles runtime data logging. Includes:

* `TXRDataManager`: manages logging and initialization of data components.
* `DataContinuousWriter`: logs headset, hands, and eye positions to CSV.
* `DataExporterFaceExpression`: logs facial expression blendshape data if face tracking is enabled.

### [TXR\_SceneManager (Prefab)](https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Docs/Scene%20Manager.md)

Handles scene loading. Includes:

* `TXRSceneManager`: provides additive scene loading, active scene switching, and player repositioning when appropriate.

### GameManager (Prefab)

Handles project-level initialization. Includes:

* `ProjectInitializer`: performs calibration and setup on startup.

### EyeTrackingTest (GameObject) (Disabled on default)

A simple test setup to validate eye tracking. Includes:

* A group of cubes with colliders.
* A script that enables the red eye tracking sphere's `MeshRenderer` when the test is active.

When working correctly, a red sphere will follow your gaze and attach to any object with a collider, verifying that eye tracking is functional.

It should look something like this
<div align="center">
<img src="https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Media/com.oculus.vrshell-20250508-120336-0.gif" width="500"/>
</div>

## Usage Instructions

1. Open the Base Scene (Assets/TAUXR/Base Scene/Base Scene) in Unity. 
2. Add objects and interactive content to the scene directly, or load additional scenes additively by dragging them into the hierarchy. Configure them later in the Build Settings and `TXRSceneManager` as [described here](https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Docs/Scene%20Manager.md).
3. The scene automatically initializes tracking, input, data export, and calibration logic at runtime. To view your exported data, see [this guide](https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Docs/View%20Exported%20Data.md).

### Causion: if you are prompted with this massage, choose "Keep using ovr hands" 

<div align="center">
<img src="https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Media/dont.png" width="500"/>
</div>


## Using TXR with Meta Interaction SDK

If you want to integrate Meta's Interaction SDK while continuing to use the TXR data export system, use the **Base Scene with Meta Interactions**:

`Assets/TAUXR/Meta Components/Meta Interactions/Base Scene Meta Interactions.unity`

This scene is based on the regular Base Scene with the following adjustments:

* An **Interaction SDK building block** has been added to the TXR\_Player object.
* A custom script disables the TXRHand visual renderers to prevent duplicate hand models.

Use this scene as your starting point when combining TXR's data management features with Meta's interaction system.
