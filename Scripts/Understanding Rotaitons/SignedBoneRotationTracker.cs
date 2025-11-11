using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignedBoneRotationTracker : MonoBehaviour
{
    public Transform thigh;
    public Transform shin;

    private Vector3 initialDirectionThigh;
    private Vector3 initialDirectionShin;

    void Start()
    {
        // Store the initial directions of the thigh and shin bones
        initialDirectionThigh = thigh.transform.up; // Assuming the bones' "up" direction is along their length
        initialDirectionShin = shin.transform.up;
    }

    void Update()
    {
        // Calculate the current directions of the thigh and shin bones
        Vector3 currentDirectionThigh = thigh.transform.up;
        Vector3 currentDirectionShin = shin.transform.up;

        // Calculate the signed angles for each axis
        float angleThighX = Vector3.SignedAngle(initialDirectionThigh, currentDirectionThigh, Vector3.right);
        float angleShinX = Vector3.SignedAngle(initialDirectionShin, currentDirectionShin, Vector3.right);

        float angleThighY = Vector3.SignedAngle(initialDirectionThigh, currentDirectionThigh, Vector3.up);
        float angleShinY = Vector3.SignedAngle(initialDirectionShin, currentDirectionShin, Vector3.up);

        float angleThighZ = Vector3.SignedAngle(initialDirectionThigh, currentDirectionThigh, Vector3.forward);
        float angleShinZ = Vector3.SignedAngle(initialDirectionShin, currentDirectionShin, Vector3.forward);

        // Now you can use these angles in your REBA calculations
        Debug.Log("Thigh angles: " + angleThighX + ", " + angleThighY + ", " + angleThighZ);
        Debug.Log("Shin Thigh angles: " + angleShinX + ", " + angleShinY + ", " + angleShinZ);
    }
}
