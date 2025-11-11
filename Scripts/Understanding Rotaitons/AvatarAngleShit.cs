using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAngleShit : MonoBehaviour
{
    public GameObject From;
    public GameObject Angle;
    public GameObject To;
    public GameObject Axis;
    public GameObject upperarm;
    public GameObject lowerarm;
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Pos local: " + From.transform.localPosition + " " + Angle.transform.localPosition + " " + To.transform.localPosition + " " + Axis.transform.localPosition);
        Debug.Log("Pos world: " + From.transform.position + " " + Angle.transform.position + " " + To.transform.position + " " + Axis.transform.position);
        Debug.Log("Pos local: " + upperarm.transform.localPosition + " " + lowerarm.transform.localPosition + " " + hand.transform.localPosition);
        Debug.Log("Pos world: " + upperarm.transform.position + " " + lowerarm.transform.position + " " + hand.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Rotation: " + getRotationAngle(From.transform, Angle.transform, To.transform, Axis.transform));
    }

    float getRotationAngle(Transform From, Transform Angle, Transform To, Transform Axis)
    {
        Vector3 ba = From.position - Angle.position;
        Vector3 bc = To.position - Angle.position;
        Vector3 bd = Axis.position - Angle.position;

        // Calculate the axis vector pointing towards the axis object
        // Vector3 axisVector = bd.normalized;

        float rotationAngle = Vector3.SignedAngle(ba, bc, bd);

        return rotationAngle;
    }
}
