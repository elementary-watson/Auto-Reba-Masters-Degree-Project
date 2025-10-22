<div align="center">
    <img width="82" height="90" alt="Logo" src="/docs/img/Eigenlogo.png" />
</div>
  <h3 align="center">Background Info</h3>
  <h3 align="center">Automatic Assessment of Body Ergonomics in Mixed Reality</h3>


The core research problem was the computing of ergonomic information in real time. The initial assumption was that once the hardware and software foundations are in place (OptiTrack/Motive + Unity) the automated calculation of the REBA-Method would be simple since an extraction of the joint angles from the motion-capture stream should be trivial. In practice, this was far trickier. 

## REBA - Rapid Entire Body Assessment
REBA is a standardized method that rates whole-body posture using angle categories and condenses the result into a single risk score from 1 to 15. The aim is speed and consistency without needing animation.

Here we see the graphic that summarizes the application phase of the Reba method. We will now systematically go through all the points:
First of all, it is important to understand that we are dividing the body into six segments CLICK to see these segments on the left and right of the illustrations. The method is designed to be intuitive and efficient - i.e. in the application we would simply look at which image most closely corresponds to the posture of the corresponding body segment.
The keyword here is isolated angular rotations! 
CLICK We can take a closer look at the concept on the torso, where we have the most angular gradations. 
Hier sehen wir jetzt den Torso im Detail – und die Anordnung dieser Bilder wurde aufsteigend von links nach rechts nach dem Risikolevel angepasst. Das heißt im ersten Bild KLICK ganz links hätte eine Person das niedrigste Risiko sich zu verletzen. 
Here in the top right-hand corner CLICK we can see how much the angle has changed from the starting position. We are already in the starting position here and therefore naturally have a value of zero degrees CLICK. The image next to it CLICK then shows us a deviation of 0 to 20 degrees with the help of a blue light cone. Depending on the amount of deviation CLICK, each posture angle is also given a certain risk score - the starting position always starts with a 1 and the image to the right of it has a risk score of 2.
CLICK If you continue to walk, the risk score increases to 3, as the person is now bending either more forwards or backwards, and a score of 4 CLICK if they are bending very far forwards. 
And the principle, which adjusts the risk levels based on angular deviations, is then also applied to all other 6 body segments. 
If we look at these body segments, we might think that we are only looking at the angles of movement that go forwards or backwards. In fact, the REBA method also takes sideways movements and twists of the body into account. However, it does not have any detailed gradations.
In anatomy, these directions of movement are considered separately, as we can see here:





## Why the usual rotation methods failed
Our human bodies are able to perform complex movements and on approach was to use "Euler Angles" to measure the limbs rotation values. Euler angles rotate around three axes in sequence (think of a gyroscope with three rings). Because the rotations are order-dependent, extreme configurations can collapse two axes into one <a href="https://www.youtube.com/watch?v=zc8b2Jo7mno&ab_channel=GuerrillaCG"><strong>Gimbal Lock »</strong></a> and the system loses a degree of freedom. The concept of Euler angles is relatively intuitive but the gimbal lock problem kept interfering with the calculation as soon as the tracked user would make use of his ability to move freely.

A promising alternative was the so-called quaternions - a quaternion is also a mathematical method to describe 3D rotation: they avoid the gimbal lock problem and provide a stable orientation in 3D space - and are also the standard in Unity 3D. You can think of them simply as a combination of "axis and rotation angle" - the problem is that you can't extract isolated angles from the quaternions. I recently tried to work with vector calculations, but unfortunately this approach did not bring any progress either.
I tried a lot of approaches but unfortunately they all failed in the end. For this reason, I then spent a long time looking at the practical application strategy of the REBA method - normally, experts were always on site and they observed the employees at work in order to find high-risk postures. KLICK However, this method has evolved over time and a company for ergonomics software solutions called ErgoPlus shows here that the calculation can also be used with a protractor on a photo or video.
I have once again made one thing clear to myself: the REBA method consistently breaks down rotations into the three anatomical planes and can therefore measure them in isolation from each other. KLICK here, for example, we are looking at the movement of the upper arm specifically in the sagittal plane - the forward and backward movements can be clearly separated from the other planes. CLICK If you now realize that the sagittal plane can be understood as a two-dimensional perspective of the body - then you could try to measure the angle with exactly this view using Unity 3D.
So I set myself the task of developing a method with which I project the three-dimensional movements of the body onto the respective anatomical planes - CLICK - these planes are then viewed like the photos on which you can place a protractor and with this view - CLICK - each plane is viewed like a still image of a person on which the angle should be able to be read precisely.
Instead of a protractor, a goniometer would fit better here - this is a tool that has two arms and in the application you would adjust one arm while the other is stationary to set the angle you are looking for. And I can do the same with this avatar - CLICK I create a sagittal plane specifically for a certain body segment - in this case for the forearm and specify in the software that this plane should move parallel to the upper arm from now on. The forearm then becomes the moving arm of the goniometer KLICK, which projects a beam onto the sagittal plane at the wrist and draws a 2D track with its movements. I then always use the current point of impact of this track and calculate the current angle in relation to the zero degree angle.
Here we see the concept as an animation from the profile view - CLICK You can imagine this idea as two hands on a clock. Only that one hand has stopped at zero.
