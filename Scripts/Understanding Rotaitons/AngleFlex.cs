//#define positionsLocalWorld
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleFlex : MonoBehaviour {
    public GameObject From;
    public GameObject Angle;
    public GameObject To;
    public GameObject Axis;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("FlexRotation: " + getRotationAngle(From.transform, Angle.transform, To.transform, Axis.transform));
    }

    float getRotationAngle(Transform From, Transform Angle, Transform To, Transform Axis) {
        Vector3 ba = From.position - Angle.position;
        Vector3 bc = To.position - Angle.position;
        Vector3 bd = Axis.position - Angle.position;
        float rotationAngle = Vector3.SignedAngle(ba, bc, bd);
        return rotationAngle;
    }
}