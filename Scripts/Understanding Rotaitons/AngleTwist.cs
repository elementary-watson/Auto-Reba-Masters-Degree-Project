using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTwist : MonoBehaviour
{
    public GameObject Twister;
    public GameObject From;
    public GameObject Angle;
    public GameObject Axis;
    public GameObject To;
    public GameObject Bone02;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(
            "TwistRotation: " + getRotationAngle(Twister.transform, Angle.transform, Axis.transform, To.transform) + "\n" +
            "FlexRotation: " + getRotationAngle(From.transform, Angle.transform, To.transform, Axis.transform)            
            );

        Debug.Log(Bone02.transform.localEulerAngles);

    }

    float getRotationAngle(Transform a, Transform b, Transform c, Transform d)
    {
        Vector3 ba = a.position - b.position;
        Vector3 bc = c.position - b.position;
        Vector3 bd = d.position - b.position;
        // Calculate the axis vector pointing towards the axis object
        // Vector3 axisVector = bd.normalized;
        float rotationAngle = Vector3.SignedAngle(ba, bc, bd);
        return rotationAngle;
    }
}
