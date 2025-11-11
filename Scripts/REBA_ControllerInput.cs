using UnityEngine;
using System.Collections;
using Oculus.Platform;

public class REBA_ControllerInput : MonoBehaviour
{
    public bool rightControllerGrabIsPressed;
    public bool leftControllerGrabIsPressed;
    public bool rightControllerTriggerIsPressed;
    public bool leftControllerTriggerIsPressed;
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            Debug.Log("Left controller X button pressed!");
        }

        // Check if the Y button is pressed
        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            Debug.Log("Left controller Y button pressed!");
        }

        // Check for the left controller's grab button
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            leftControllerGrabIsPressed = true;
            Debug.Log("Left controller grab button pressed!");
        }
        else
        {
            leftControllerGrabIsPressed = false;
        }

        // Check for the left controller's trigger button
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.5f)
        {
            Debug.Log("Left controller trigger button pressed!");
        }

        // Right Controller
        // Check if the A button is pressed
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            Debug.Log("Right controller A button pressed!");
        }

        // Check if the B button is pressed
        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            Debug.Log("Right controller B button pressed!");
        }

        // Check for the right controller's grab button
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.RTouch))
        {
        }

        // Check for the right controller's trigger button
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.RTouch) > 0.5f)
        {
            Debug.Log("Right controller trigger button pressed!");
        }

        // Check for the right controller's grab (hand) button
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.RTouch) > 0.5f)
        {
            Debug.Log("Right controller grab button pressed!");
        }

        // Check for the right controller's index trigger
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.RTouch) > 0.5f)
        {
            Debug.Log("Right controller trigger button pressed!");
        }

        if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.5) 
        {
            Debug.Log("Trigger left");
            leftControllerTriggerIsPressed = true;
        }
        else 
        {
            leftControllerTriggerIsPressed = false;
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.5) 
        {
            Debug.Log("RIndexTrigger right");
            rightControllerTriggerIsPressed = true;
        }
        else
        { 
            rightControllerTriggerIsPressed = false; 
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0.5)
        {
            rightControllerGrabIsPressed = true;
            Debug.Log("Right controller grab button pressed!");
        }
        else
        {
            rightControllerGrabIsPressed = false;
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTriggerCurl) > 0.5)
        {
            Debug.Log("RIndexTriggerCurl right");
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTriggerSlide) > 0.5)
        {
            Debug.Log("RIndexTriggerSlide right");
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RStylusForce) > 0.5)
        {
            Debug.Log("RStylusForce right");
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RThumbRestForce) > 0.5)
        {
            Debug.Log("RThumbRestForce right");
        }
    }
    void FixedUpdate()
    {
        // Update the OVRInput state for this physics frame
        OVRInput.FixedUpdate();
    }
}
