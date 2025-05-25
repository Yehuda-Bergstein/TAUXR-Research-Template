# 🧰 TAUXR Unity Toolkit

The **TAUXR Unity XR Toolkit** is a powerful set of tools designed to help developers create immersive **VR/XR experiences** for educational and research purposes.

This guide covers the key components of the toolkit:

- 🗂️ Unity Project Template  
- 📊 Data Collection Tools  
- 🕹️ Player Interaction Management  


Each component is optimized for **Meta Quest devices**, streamlining development and improving performance.

---
## Requirements

- Unity 6 or  Unity 2022.3 LTS +

---
## 🗂️ Unity Project Template

Quickly jump into VR/XR development with our pre-configured Unity project template for Meta Quest. It includes:

- Optimized settings for standalone VR  
- Minimal setup for beginners  
- A solid foundation to start building immersive content  

➡️ [Get started using the Base Scene](https://github.com/TAU-XR/TAUXR-Research-Template/blob/main/Docs/Getting%20Started%20with%20Base%20Scene.md) – the foundational scene for all XR development and data logging.

---

## 📊 Data Collection and Export Tools

Track user behavior and export structured data for research and analysis:

- **Analytics**: Log custom events and user actions; export to CSV.
- **Continuous Data**: Record head, hands, and eyes (position + rotation) per frame.
- **Face Expressions** *(Meta Quest Pro only)*:  
  Capture 63 blendshape values (range 0–1) every frame and export them.

Perfect for analyzing interaction design and studying user behavior in VR.

---

## 🕹️ TXR Player – Managing Player Interactions

Central to the toolkit, the **TXR Player** handles player presence and inputs:

- 🎯 **Head & Hands Tracking** – Get accurate transforms in real-time
- ✋ **Hand Tracking** – Includes clean visual models for gesture-based interaction
- 🤏 **Pinch Detection** – Enable natural gesture-based interaction
- 🎮 **Controller Input & Haptics** – Access button data + vibration feedback
- 👁️ **Eye Tracking** – Use gaze data for attention-aware experiences *(Quest Pro only)*

---

The Development of these tools was sponsored by the Minerva Center for Human Intelligence in immersive, augmented and mixed Realities.
