<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
    <img width="82" height="90" alt="logo img" src="docs/img/Eigenlogo.png" />
</div>
  <h3 align="center">Master Thesis Project</h3>
  <h3 align="center">Automatic Assessment of Body Ergonomics in Mixed Reality</h3>
  
## Quick Summary
This Unity3D project demonstrates how to stream full-body motion from OptiTrack/Motive into Unity to compute the ergonomic REBA-Score in real time and stream it into a Head-Up-Display. The core contribution is a plane-first angle method: joint orientations are projected onto anatomical planes (sagittal, frontal, transverse), measured as 2D angles (goniometer-style), and mapped into REBA tables A/B/C with modifiers (Activity, Force/Load, Coupling). The user is receiving a live biofeedback showing a color coded risk score (1–15) reflecting the ergonomic quality.

## About the project

<div align="center">
<img alt="Project" src="docs/img/AutoREBA Project Banner.png" />
</div>

Imagine you were wearing a pair of ultra-modern glasses. These glasses now show you a number that reflects an assessment of your posture, directly in your field of vision - for example, it could say "level 7 out of 15, increased risk" - would you spontaneously straighten up when you read this? Or let's say we use a traffic light system instead of a number system - green would mean your posture is healthy, red is signaling a high risk posture and yellow is tolerable risk - would that help you adopt a healthy posture?
It is precisely this reflex that was investigatet in this work! To do this a VR-supported system was developed that uses motion capturing to record people's current posture and evaluates it using the REBA method developed by Hignett, S. and McAtamney, L. (2000) Rapid Entire Body Assessment.

This system was tested in a small pilot study - six participants came and performed a series of tasks in VR - during which they received live feedback on their current posture in VR.

# Project: Automation of the REBA method


## The REBA Method
<table>
  <tr>
    <td width="49%">
      <img
    alt="REBA-WorkSheet" 
    src="docs/img/rapid-entire-body-assessment-reba-1.png"
        width="100%" />
    </td>
    <td width="49%">
      <h3>REBA Worksheet</h3>
      <p>
REBA stands for Rapid Entire Body Assessment. It is a standardized procedure that allows for a quick whole-body posture assessment on the basis of angular positions. The entire posture is broken down into a single risk assessment that ranges from 1 to 15, i.e. from risk-free to full risk. The worksheet by ErgoPlus on the left summarizes the Reba method for quick application. The body is divided into six segments and a health worker would compare a patient body segments with worksheet images to assess the posture and the corresponding risk values.<br />
 <a href="BACKGROUND.md"><strong>Read more in the background info »</strong></a>
    </p>
    </td>
  </tr>
</table>

#### Limitation of REBA
REBA does not evaluate every single intermediate position. In practice, we would observe the patient through his/her work process to catch critical postures, e.g. climbing a ladder with heavy loaded box. One person may carry the box overhead and another will carry it at stomach height. With REBA, we can say: How risky is a certain posture and where do we need to intervene! However, we can only evaluate the postures that we actively detect, and it is highly likely that we will overlook far more critical moments than we capture.

#### Search for a solution
REBA requires isolated angles per anatomical plane (sagittal, frontal, transversal).
Conventional 3D rotation representations (Euler, quaternion, pure 3D vector angles) do not provide robust, plane-specific individual angles.
The solution here: first project onto the anatomical plane, then measure as 2D angles (goniometer-like) and then map into the REBA tables A/B/C.

## The Idea
<table>
  <tr>
    <td width="49%">
      <img
    alt="Traditional Method" 
    src="docs/img/REBA_Goniometer.png"
        width="100%" />
    </td>
    <td width="49%">
      <h3>REBA Worksheet</h3>
      <p>
REBA stands for Rapid Entire Body Assessment. It is a standardized procedure that allows for a quick whole-body posture assessment on the basis of angular positions. The entire posture is broken down into a single risk assessment that ranges from 1 to 15, i.e. from risk-free to full risk. The worksheet by ErgoPlus on the left summarizes the Reba method for quick application. The body is divided into six segments and a health worker would compare a patient body segments with worksheet images to assess the posture and the corresponding risk values.<br />
 <a href="BACKGROUND.md"><strong>Read more in the background info »</strong></a>
    </p>
    </td>
  </tr>
</table>

I tried a lot of approaches but unfortunately they all failed in the end. For this reason, I then spent a long time looking at the practical application strategy of the REBA method - normally, experts were always on site and they observed the employees at work in order to find high-risk postures. KLICK However, this method has evolved over time and a company for ergonomics software solutions called ErgoPlus shows here that the calculation can also be used with a protractor on a photo or video.
I have once again made one thing clear to myself: the REBA method consistently breaks down rotations into the three anatomical planes and can therefore measure them in isolation from each other. KLICK here, for example, we are looking at the movement of the upper arm specifically in the sagittal plane - the forward and backward movements can be clearly separated from the other planes. CLICK If you now realize that the sagittal plane can be understood as a two-dimensional perspective of the body - then you could try to measure the angle with exactly this view using Unity 3D.
So I set myself the task of developing a method with which I project the three-dimensional movements of the body onto the respective anatomical planes - CLICK - these planes are then viewed like the photos on which you can place a protractor and with this view - CLICK - each plane is viewed like a still image of a person on which the angle should be able to be read precisely.
Instead of a protractor, a goniometer would fit better here - this is a tool that has two arms and in the application you would adjust one arm while the other is stationary to set the angle you are looking for. And I can do the same with this avatar - CLICK I create a sagittal plane specifically for a certain body segment - in this case for the forearm and specify in the software that this plane should move parallel to the upper arm from now on. The forearm then becomes the moving arm of the goniometer KLICK, which projects a beam onto the sagittal plane at the wrist and draws a 2D track with its movements. I then always use the current point of impact of this track and calculate the current angle in relation to the zero degree angle.



Basic idea: People measure photos/videos with a goniometer i.e., they use a 2D measurement. Therefore we first bring the 3D movement into the appropriate plane, then we measure the 2D angle there.

Algorithm (per segment, per frame):

Select anatomical plane (e.g., sagittal for flexion/extension).

Define local reference plane: The plane is linked to the parent segment (e.g., upper arm as reference for the forearm) so that it moves with the body.

Project segment vector: e.g., 𝑣 ⃗ = wrist − elbow v =wrist−elbow project onto the plane → 𝑣 ⃗ 2 𝐷 v 2D ​. 

 Determine 2D angle: signed angle between 𝑣 ⃗ 2 𝐷 v 2D ​

 and a neutral direction (0°) of the plane – goniometer-like.

REBA mapping: angle class per segment → Tables A/B/C → Modifiers (Activity, Force/Load, Coupling).

<div align="center">
    <img alt="compareRealities" src="docs/img/comparisonOfRealities.png" />
</div>
<div align="center">
<img alt="REBA zu Dashboard" src="docs/img/REBA zu Dashboard.png" />
</div>


<table>
  <tr>
  <td width="49%">
        <h3>Condition A</h3>
<img alt="compared realities img" src="docs/img/Condition A.png" />
 </td>
   <td width="49%">
   <h3>Condition B</h3>
<img alt="compared realities img" src="docs/img/Condition B.png" />
 </td>
 </tr>
 <tr>
  <td width="49%">
  <h3>Condition C</h3>
<img alt="compared realities img" src="docs/img/Condition C.png" />
 </td>
   <td width="49%">
   <h3>Condition D</h3>
<img alt="compared realities img" src="docs/img/Condition D.png" />
 </td>
 </tr>
</table>

### Automated REBA Assessment


Why "plane-first"?

Problem: Euler angles are sequence-dependent; quaternions provide stable orientation, but no isolated plane angles; pure 3D vector angles mix motion components.

Solution: Projection → 2D angle → REBA class. This results in clean individual values per segment and plane, exactly as required by REBA.



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
<video width="80%" controls>
  <source src="docs/video/REBA_display.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>



<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Compact view of the UML diagram showing the workflow 
<p align="center">
<img alt="UML Low Level View" src="docs/img/UML Low Level View.png" />
</p>
<p align="center">
  <em>
  Comparison of the REBA-HUD presentation: on the left an example with low risk, on the right an example with high risk.
  </em>
</p>

### Hardware Setup Overview
<p align="center">
<img alt="UML Hardware overview" src="docs/img/UML hardware overview.png" />
</p>
<p align="center">
  <em>
  Comparison of the REBA-HUD presentation: on the left an example with low risk, on the right an example with high risk.
  </em>
</p>
<p align="right">(<a href="#readme-top">back to top</a>)</p>
