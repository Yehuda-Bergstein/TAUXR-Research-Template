# TXRPlayer.cs Documentation

## Purpose

The `TXRPlayer` class is a core singleton that manages the local VR player. It acts as the central point for initializing, updating, and providing access to:

* Player tracking anchors (head and hands)
* Hand tracking
* Eye tracking
* Face tracking
* Player input management

## Serialized Fields and Properties

### Player Trackables

* `ovrRig (Transform)`: Root of the OVR rig. Used only when optional face-tracking (`IsFaceTrackingEnabled`) is injected.
* `playerHead (Transform)`: Reference to the player head anchor.
* `rightHandAnchor (Transform)`: Reference to the right hand anchor.
* `leftHandAnchor (Transform)`: Reference to the left hand anchor.

**Public Accessors:**

* `PlayerHead (Transform)`
* `RightHand (Transform)`
* `LeftHand (Transform)`

### Hand Tracking

* `HandLeft (TXRHand)`: Left hand management.
* `HandRight (TXRHand)`: Right hand management.

### Eye Tracking

* `EyeTracker (TXREyeTracker)`: Eye tracking component.
* `IsEyeTrackingEnabled (bool)`: Enables or disables eye tracking.

**Public Accessors:**

* `FocusedObject (Transform)` → `EyeTracker.FocusedObject`
* `EyeGazeHitPosition (Vector3)` → `EyeTracker.EyeGazeHitPosition`
* `RightEye (Transform)` → `EyeTracker.RightEye`
* `LeftEye (Transform)` → `EyeTracker.LeftEye`

### Face Tracking

* `ovrFace (OVRFaceExpressions)`: Reference for face tracking.
* `IsFaceTrackingEnabled (bool)`: Enables or disables face tracking.

**Public Accessor:**

* `OVRFace (OVRFaceExpressions)`

### Additional

* `colorOverlayMR (MeshRenderer)`: Material used for fading the player view.
* `ControllersInputManager (ControllersInputManager)`: Handles physical controller inputs.
* `PinchingInputManager (PinchingInputManager)`: Handles hand pinch gesture inputs.

## Public Methods

### Initialization and Update

* `DoInAwake()`: Initializes all major subsystems.

  * Calls `Init()` for both hands.
  * Instantiates `ControllersInputManager` and `PinchingInputManager`.
  * If eye tracking is enabled, calls `EyeTracker.Init()`.
  * If face tracking is enabled, adds `OVRFaceExpressions` to `ovrRig`.

* `Update()`:

  * Updates hand tracking via `HandRight.UpdateHand()` and `HandLeft.UpdateHand()`.
  * Updates eye tracking via `EyeTracker.UpdateEyeTracker()` if enabled.

### View Management

* `async UniTask FadeViewToColor(Color targetColor, float duration)`

  * Gradually fades the view by changing the `colorOverlayMR` color.
  * If `duration == 0`, the color is set instantly.

### Positioning and Repositioning

* `void RepositionPlayer(PlayerRepositioner repositioner)`

  * Repositions player after headset initialization.

* `async UniTask RepositionAfterHeadsetLoaded(PlayerRepositioner repositioner)`

  * Waits for `OVRManager.isHmdPresent` and for player head movement.
  * Rotates player to face `repositioner.transform.forward`.
  * Moves player to match `repositioner.transform.position`.

### Calibration and Services

* `void SetPassthrough(bool state)` → Toggles passthrough using `TXRHeadsetServices.Instance`.

### Hand Utility

* `Transform GetHandFingerCollider(HandType handType, FingerType fingerType)`

  * Returns the `Transform` of a specific finger collider for a hand.
  * Delegates to `TXRHand.GetFingerCollider()`.

| HandType | Result               |
| -------- | -------------------- |
| Left     | `HandLeft` collider  |
| Right    | `HandRight` collider |
| Any      | `HandLeft` collider  |
| None     | null                 |

## External Dependencies

* `TXRHand`: Manages hand tracking and pinch detection.
* `TXREyeTracker`: Manages gaze tracking.
* `OVRFaceExpressions`: Provides face tracking blendshape data.
* `ControllersInputManager`: Handles hardware controller button inputs.
* `PinchingInputManager`: Handles hand-based pinch gesture inputs.

## Summary of Interactions

| System           | Description                                                                                      |
| ---------------- | ------------------------------------------------------------------------------------------------ |
| Hands            | Tracked via `TXRHand`, including finger positions and pinch detection.                           |
| Eye Tracking     | Tracked via `TXREyeTracker`, including gaze target and hit point.                                |
| Face Tracking    | Uses Meta `OVRFaceExpressions`.                                                                  |
| Input            | Combines `ControllersInputManager` for controllers and `PinchingInputManager` for hand pinching. |
| Scene Management | `RepositionPlayer()` used by `TXRSceneManager`.                                                  |

## Example Usage

### Access Player Hands

```csharp
Transform leftHand = TXRPlayer.Instance.LeftHand;
Transform rightHand = TXRPlayer.Instance.RightHand;
```

### Get Specific Finger Collider

```csharp
Transform indexFingerCollider = TXRPlayer.Instance.GetHandFingerCollider(HandType.Right, FingerType.Index);
```

### Check Eye Tracking Focused Object

```csharp
if (TXRPlayer.Instance.IsEyeTrackingEnabled && TXRPlayer.Instance.FocusedObject != null)
    Debug.Log($"User is looking at: {TXRPlayer.Instance.FocusedObject.name}");
```

### Fade View to Color

```csharp
await TXRPlayer.Instance.FadeViewToColor(Color.black, 2f); // fade to black over 2 seconds
```

### Reposition Player

```csharp
PlayerRepositioner repositioner = FindObjectOfType<PlayerRepositioner>();
if (repositioner != null)
    TXRPlayer.Instance.RepositionPlayer(repositioner);
```

## Notes

* The `TXRPlayer` must be present in scene as a singleton.
* It serves as the primary interface for accessing player tracking data for other systems.

---

This documentation fully describes the `TXRPlayer` as implemented in your provided code base. Let me know if you want to proceed similarly with other classes.
