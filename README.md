# Auto-Reba-Masters-Degree-Project
This repository demonstrates how to stream full-body motion from OptiTrack/Motive into Unity, compute REBA in real time, and render a clean HUD to Meta Quest Pro. The core contribution is a plane-first angle method: joint orientations are projected onto anatomical planes (sagittal, frontal, transverse), measured as 2D angles (goniometer-style), and mapped into REBA tables A/B/C with modifiers (Activity, Force/Load, Coupling). The HUD shows a single risk score (1–15) plus a traffic-light status.



## REBA HUD – Low vs. High Risk

<p align="center">
  <img
    alt="reba hud low risk"
    src="https://github.com/user-attachments/assets/571b84a7-bbd4-4cc4-a462-0e5f3cac18e0"
    width="49%" />
  <img
    alt="reba hud high risk"
    src="https://github.com/user-attachments/assets/de66eee8-80ea-4a42-9b0a-115b63b17e11"
    width="49%" />
</p>

<p align="center">
  <em>
Comparison of the REBA-HUD presentation: on the left an example with low risk, on the right an example with high risk.
  </em>
</p>

## Video of REBA HUD Display
https://github.com/user-attachments/assets/1a4f515b-99e5-4136-a156-59fcbad83cb5

## UML diagram of the essential classes 

<img width="1318" height="1345" alt="UML Low Level View" src="https://github.com/user-attachments/assets/74c15809-be5b-4cb0-b876-48a0829fa453" />


## Hardware Setup Overview  
<img width="862" height="726" alt="UML Oben" src="https://github.com/user-attachments/assets/15752fe4-188f-4d60-87e9-8fe498a38487" />
