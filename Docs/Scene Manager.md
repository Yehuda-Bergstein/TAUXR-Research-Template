## TXRSceneManager.cs – Scene Loader

Handles scene transitions in VR by keeping the **base scene always loaded** and **additively loading** the main scene at runtime. Supports optional player repositioning and smooth fade transitions.

---

### 🔧 Scene Setup

In your **Base Scene**, make sure to include:

- ✅ A **GameManager prefab** with the `ProjectInitializer` script  
- ✅ A **TXRSceneManager prefab** with the `TXRSceneManager` script  

These handle calibration and scene loading automatically.

---

### 🛠️ Build Settings

1. Open **File > Build Settings**
2. Add scenes in this order:
   1. Base Scene (index 0)
   2. Main Scene (index 1)
   3. Any additional scenes...

> ⚠️ The **base scene must always be first**.

---

### ▶️ What Happens at Runtime

#### ✅ In Build:
- Fades screen to black  
- Runs environment calibration *(if enabled)*  
- Additively loads the main scene  
- Fades back to clear  

#### 🧪 In Editor:
- Assumes base + main scenes are opened additively  
- Automatically detects the active scene  
- Calibration optional via `_shouldCalibrateOnEditor`

---

### 🔁 Scene Control API

```csharp
TXRSceneManager.Instance.SwitchActiveScene("SceneName");

