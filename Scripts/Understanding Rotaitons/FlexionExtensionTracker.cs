using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexionExtensionTracker : MonoBehaviour
{
    public Transform thigh;
    public Transform shin;

    void Update()
    {
        // Get the current rotations of the thigh and shin bones
        Quaternion currentRotationThigh = thigh.transform.rotation;
        Quaternion currentRotationShin = shin.transform.rotation;

        // Project the rotations onto the sagittal plane by ignoring the y and z components
        Quaternion sagittalRotationThigh = Quaternion.Euler(currentRotationThigh.eulerAngles.x, 0, 0);
        Quaternion sagittalRotationShin = Quaternion.Euler(currentRotationShin.eulerAngles.x, 0, 0);

        // Calculate the relative rotation from the thigh to the shin
        Quaternion relativeRotation = Quaternion.Inverse(sagittalRotationThigh) * sagittalRotationShin;


        // The angle of flexion/extension is the angle of the relative rotation
        float flexionExtensionAngle = relativeRotation.eulerAngles.x;

        // Adjust the angle to be in the range of -180 to 180
        if (flexionExtensionAngle > 180)
        {
            flexionExtensionAngle -= 360;
        }



        // Now you can use flexionExtensionAngle in your REBA calculations

        Debug.Log("Sagittal Plane: " + flexionExtensionAngle);
    }
}
