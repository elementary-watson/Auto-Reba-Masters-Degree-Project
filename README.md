<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
    <img width="82" height="90" alt="Logo" src="https://github.com/elementary-watson/AutoReba-Masters-Thesis-Project/docs/img/Eigenlogo.png" />
</div>
  <h3 align="center">Master Thesis Project</h3>
  <h3 align="center">Automatic Assessment of Body Ergonomics in Mixed Reality</h3>
## Quick Summary
This Unity3D project demonstrates how to stream full-body motion from OptiTrack/Motive into Unity to compute the ergonomic REBA-Score in real time and stream it into a Head-Up-Display. The core contribution is a plane-first angle method: joint orientations are projected onto anatomical planes (sagittal, frontal, transverse), measured as 2D angles (goniometer-style), and mapped into REBA tables A/B/C with modifiers (Activity, Force/Load, Coupling). The user is receiving a live biofeedback showing a color coded risk score (1–15) reflecting the ergonomic quality.

## About the project
Imagine you were wearing a pair of ultra-modern glasses. These glasses now show you a number that reflects an assessment of your posture, directly in your field of vision - for example, it could say "level 7 out of 15, increased risk" - would you spontaneously straighten up when you read this? Or let's say we use a traffic light system instead of a number system - green would mean your posture is healthy, red is signaling a high risk posture and yellow is tolerable risk - would that help you adopt a healthy posture?
It is precisely this reflex that was investigatet in this work! To do this a VR-supported system was developed that uses motion capturing to record people's current posture and evaluates it using the REBA method developed by Hignett, S. and McAtamney, L. (2000) Rapid Entire Body Assessment.

This system was tested in a small pilot study - six participants came and performed a series of tasks in VR - during which they received live feedback on their current posture in VR.

<p align="center">
  <img
    alt="REBA-WorkSheet" 
    src="https://github.com/user-attachments/assets/24c72564-4faf-41ec-97c9-0b7162a37f3d
    width="49%" />
  <img
    alt="reba hud high risk"
    src="https://github.com/user-attachments/assets/de66eee8-80ea-4a42-9b0a-115b63b17e11"
    width="49%" />
</p>
<div align="center">
</div>


<table>
  <tr>
    <td width="49%">
      <img
        alt="REBA HUD – Low Risk"
        src="https://github.com/user-attachments/assets/571b84a7-bbd4-4cc4-a462-0e5f3cac18e0"
        width="100%" />
    </td>
    <td width="49%">
      <h3>Erklärung / Caption</h3>
      <p>
        Dieses Beispiel zeigt das REBA-HUD im Zustand „Low Risk“. 
        Rechts steht der beschreibende Text, links das Bild.
      </p>
      <p>
        <strong>Ziel:</strong> Nebeneinanderstellung von Visualisierung und kurzer Erläuterung,
        gut lesbar in GitHub-READMEs.
      </p>
    </td>
  </tr>
</table>

Why "plane-first"?

Problem: Euler angles are sequence-dependent; quaternions provide stable orientation, but no isolated plane angles; pure 3D vector angles mix motion components.

Solution: Projection → 2D angle → REBA class. This results in clean individual values per segment and plane, exactly as required by REBA.

<img width="3269" height="1227" alt="REBA zu Dashboard" src="https://github.com/user-attachments/assets/9dfbe0f8-3375-465f-927b-ea2755312bd8" />

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

### Video of REBA HUD Display
https://github.com/user-attachments/assets/1a4f515b-99e5-4136-a156-59fcbad83cb5
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Compact view of the UML diagram showing the workflow 
<p align="center">
<img width="1318" height="1345" alt="UML Low Level View" src="https://github.com/user-attachments/assets/74c15809-be5b-4cb0-b876-48a0829fa453" />
</p>
<p align="center">
  <em>
  Comparison of the REBA-HUD presentation: on the left an example with low risk, on the right an example with high risk.
  </em>
</p>

### Hardware Setup Overview
<p align="center">
<img width="862" height="726" alt="UML Oben" src="https://github.com/user-attachments/assets/15752fe4-188f-4d60-87e9-8fe498a38487" />
</p>
<p align="center">
  <em>
  Comparison of the REBA-HUD presentation: on the left an example with low risk, on the right an example with high risk.
  </em>
</p>
<p align="right">(<a href="#readme-top">back to top</a>)</p>
