<a id="background-top"></a>
<div align="center">
    <img width="82" height="90" alt="Logo" src="/docs/img/Eigenlogo.png" />
</div>
  <h3 align="center">Background Info</h3>
  <p align="center">
   <a href="README.md"><strong>Back to ReadMe »</strong></a>
  </p>
  <h3 align="center">Automatic Assessment of Body Ergonomics in Mixed Reality</h3>


The core research problem was the computing of ergonomic information in real time. The initial assumption was that once the hardware and software foundations are in place (OptiTrack/Motive + Unity) the automated calculation of the REBA-Method would be simple since an extraction of the joint angles from the motion-capture stream should be trivial. In practice, this was far trickier. 



## REBA – Rapid Entire Body Assessment

REBA is a standardized observational method for rating whole-body posture. It assigns categorical angle values to different body segments and condenses them into a single ergonomic risk score from 1 to 15. In this project, REBA serves as the basis for assessing ergonomic risk from body postures in VR.

<table>
  <tr>
    <td width="49%">
      <img
        alt="REBA Worksheet" 
        src="docs/img/rapid-entire-body-assessment-reba-1.png"
        width="100%" />
    </td>
    <td width="49%">
      <h3>REBA Worksheet (overview)</h3>
      <p>
        The worksheet summarizes how REBA is applied in practice. The body is divided into six segments (neck, trunk, legs, upper arms, lower arms, wrists). For each segment, the observer selects the illustration that best matches the current posture. Each illustration represents a specific angle category and is linked to a discrete risk score.
        <br /><br />
        This image-based approach makes REBA quick to apply and reasonably consistent between observers, even when only still images or short video sequences are available.
        <br /><br />
        Detailed background information on the REBA method, angle categories, and scoring logic used in this project can be found here:
        <a href="BACKGROUND.md"><strong>Explore the docs »</strong></a>
      </p>
    </td>
  </tr>
</table>

Although the examples on the worksheet mainly show forward and backward bending, REBA also accounts for lateral bending and twisting of the body. These additional movements are handled as posture adjustments on top of the basic angle categories.

<p align="right">(<a href="#background-top">back to top</a>)</p>



## Why the usual rotation methods failed
Our human bodies are able to perform complex movements and on approach was to use "Euler Angles" to measure the limbs rotation values. Euler angles rotate around three axes in sequence (think of a gyroscope with three rings). Because the rotations are order-dependent, extreme configurations can collapse two axes into one <a href="https://www.youtube.com/watch?v=zc8b2Jo7mno&ab_channel=GuerrillaCG"><strong>Gimbal Lock »</strong></a> and the system loses a degree of freedom. The concept of Euler angles is relatively intuitive but the gimbal lock problem kept interfering with the calculation as soon as the tracked user would make use of his ability to move freely.

A promising alternative was the so-called quaternions - a quaternion is also a mathematical method to describe 3D rotation: they avoid the gimbal lock problem and provide a stable orientation in 3D space - and are also the standard in Unity 3D. You can think of them simply as a combination of "axis and rotation angle" - the problem is that you can't extract isolated angles from the quaternions. I recently tried to work with vector calculations, but unfortunately this approach did not bring any progress either.
I tried a lot of approaches but unfortunately they all failed in the end. For this reason, I then spent a long time looking at the practical application strategy of the REBA method - normally, experts were always on site and they observed the employees at work in order to find high-risk postures. KLICK However, this method has evolved over time and a company for ergonomics software solutions called ErgoPlus shows here that the calculation can also be used with a protractor on a photo or video.
I have once again made one thing clear to myself: the REBA method consistently breaks down rotations into the three anatomical planes and can therefore measure them in isolation from each other. KLICK here, for example, we are looking at the movement of the upper arm specifically in the sagittal plane - the forward and backward movements can be clearly separated from the other planes. CLICK If you now realize that the sagittal plane can be understood as a two-dimensional perspective of the body - then you could try to measure the angle with exactly this view using Unity 3D.
So I set myself the task of developing a method with which I project the three-dimensional movements of the body onto the respective anatomical planes - CLICK - these planes are then viewed like the photos on which you can place a protractor and with this view - CLICK - each plane is viewed like a still image of a person on which the angle should be able to be read precisely.
Instead of a protractor, a goniometer would fit better here - this is a tool that has two arms and in the application you would adjust one arm while the other is stationary to set the angle you are looking for. And I can do the same with this avatar - CLICK I create a sagittal plane specifically for a certain body segment - in this case for the forearm and specify in the software that this plane should move parallel to the upper arm from now on. The forearm then becomes the moving arm of the goniometer KLICK, which projects a beam onto the sagittal plane at the wrist and draws a 2D track with its movements. I then always use the current point of impact of this track and calculate the current angle in relation to the zero degree angle.
Here we see the concept as an animation from the profile view - CLICK You can imagine this idea as two hands on a clock. Only that one hand has stopped at zero.

<p align="right">(<a href="#background-top">back to top</a>)</p>


## Mein Ansatz


I tried a lot of approaches but unfortunately they all failed in the end. For this reason, I then spent a long time looking at the practical application strategy of the REBA method - normally, experts were always on site and they observed the employees at work in order to find high-risk postures. KLICK However, this method has evolved over time and a company for ergonomics software solutions called ErgoPlus shows here that the calculation can also be used with a protractor on a photo or video.
I have once again made one thing clear to myself: the REBA method consistently breaks down rotations into the three anatomical planes and can therefore measure them in isolation from each other. KLICK here, for example, we are looking at the movement of the upper arm specifically in the sagittal plane - the forward and backward movements can be clearly separated from the other planes. CLICK If you now realize that the sagittal plane can be understood as a two-dimensional perspective of the body - then you could try to measure the angle with exactly this view using Unity 3D.
So I set myself the task of developing a method with which I project the three-dimensional movements of the body onto the respective anatomical planes - CLICK - these planes are then viewed like the photos on which you can place a protractor and with this view - CLICK - each plane is viewed like a still image of a person on which the angle should be able to be read precisely.
Instead of a protractor, a goniometer would fit better here - this is a tool that has two arms and in the application you would adjust one arm while the other is stationary to set the angle you are looking for. And I can do the same with this avatar - CLICK I create a sagittal plane specifically for a certain body segment - in this case for the forearm and specify in the software that this plane should move parallel to the upper arm from now on. The forearm then becomes the moving arm of the goniometer KLICK, which projects a beam onto the sagittal plane at the wrist and draws a 2D track with its movements. I then always use the current point of impact of this track and calculate the current angle in relation to the zero degree angle.



#### Search for a solution
REBA requires isolated angles per anatomical plane (sagittal, frontal, transversal).
Conventional 3D rotation representations (Euler, quaternion, pure 3D vector angles) do not provide robust, plane-specific individual angles.
The solution here: first project onto the anatomical plane, then measure as 2D angles (goniometer-like) and then map into the REBA tables A/B/C.




Basic idea: People measure photos/videos with a goniometer i.e., they use a 2D measurement. Therefore we first bring the 3D movement into the appropriate plane, then we measure the 2D angle there.

Algorithm (per body segment, per frame):

Select anatomical plane (e.g., sagittal for flexion/extension).

Define local reference plane: The plane is linked to the parent segment (e.g., upper arm as reference for the forearm) so that it moves with the body.

Project segment vector: e.g., 𝑣 ⃗ = wrist − elbow v =wrist−elbow project onto the plane → 𝑣 ⃗ 2 𝐷 v 2D ​. 

 Determine 2D angle: signed angle between 𝑣 ⃗ 2 𝐷 v 2D ​

 and a neutral direction (0°) of the plane – goniometer-like.

REBA mapping: angle class per segment → Tables A/B/C → Modifiers (Activity, Force/Load, Coupling).