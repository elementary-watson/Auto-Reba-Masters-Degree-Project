using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneRotationTracker : MonoBehaviour
{
    public Transform thighBone;
    public Transform shinBone;
    public GameObject thighTracker;
    public GameObject shinTracker;
    public GameObject shinTrackerRoll;
    public GameObject thighTrackerEuler;
    public GameObject shinTrackerEuler;
    private Quaternion initialThighRotation;
    private Quaternion initialShinRotation;

    void Start()
    {
        // Store the initial rotations of the thigh and shin bones
        initialThighRotation = thighBone.rotation;
        initialShinRotation = shinBone.rotation;
    }

    void Update()
    {
        // Calculate the relative rotations of the thigh and shin bones
        Quaternion relativeThighRotation = Quaternion.Inverse(initialThighRotation) * thighBone.rotation;
        Quaternion relativeShinRotation = Quaternion.Inverse(initialShinRotation) * shinBone.rotation;

        // Get the current rotation of the shin
        Quaternion currentRotationShin = shinBone.transform.rotation;

        // Convert the current rotation of the shin to Euler angles
        Vector3 eulerRotationShin = currentRotationShin.eulerAngles;

        // Set the rotation of the shinTrackerEuler to be the same as the x rotation of the shin
        shinTrackerEuler.transform.rotation = Quaternion.Euler(eulerRotationShin.x, 0, 0);

        // Apply these relative rotations to the tracker game objects
        thighTracker.transform.rotation = relativeThighRotation;
        //shinTracker.transform.rotation = Quaternion.Euler(relativeShinRotation.eulerAngles.x,0,0);
        shinTracker.transform.rotation = relativeShinRotation;
        shinTrackerRoll.transform.rotation = relativeShinRotation;

        // Calculate the angle between the tracker game objects
        //float angle = Vector3.Angle(thighTracker.transform.up, shinTracker.transform.up);

        // Calculate the angle of flexion/extension (pitch)
        float pitchAngle = Vector3.Angle(thighTracker.transform.up, shinTracker.transform.up);

        // Calculate the angle of abduction/adduction (roll)
        float rollAngle = Vector3.Angle(thighTracker.transform.right, shinTracker.transform.right);

        // Calculate the angle of twist (yaw)
        float yawAngle = Vector3.Angle(thighTracker.transform.forward, shinTracker.transform.forward);


        // Calculate the angle of flexion/extension (pitch)
        float pitchAngleRoll = Vector3.Angle(thighTracker.transform.up, shinTrackerRoll.transform.up);

        // Calculate the angle of abduction/adduction (roll)
        float rollAngleRoll = Vector3.Angle(thighTracker.transform.right, shinTrackerRoll.transform.right);

        // Calculate the angle of twist (yaw)
        float yawAngleRoll = Vector3.Angle(thighTracker.transform.forward, shinTrackerRoll.transform.forward);


        Debug.Log("BoneRotationTracker " + pitchAngle + " " + rollAngle + " " + yawAngle);
        Debug.Log("BoneRotationTrackerRoll " + pitchAngleRoll + " " + rollAngleRoll + " " + yawAngleRoll);
        // Now you can use this angle in your REBA calculations
    }
}
