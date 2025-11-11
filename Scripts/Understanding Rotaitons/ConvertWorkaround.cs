using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertWorkaround : MonoBehaviour
{
    public GameObject r_thigh;
    public GameObject r_shin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Converter Workaround " + getPan(r_shin.transform) + " " + getRoll(r_shin.transform) + " " + getTilt(r_shin.transform)); 
    }

    float getPan(Transform t)
    {
        return t.localEulerAngles.z;
    }

    float getRoll(Transform originalTransform)
    {
        GameObject tempGO = new GameObject();
        Transform t = tempGO.transform;
        t.localRotation = originalTransform.localRotation;

        t.Rotate(0, 0, t.localEulerAngles.z * -1);

        GameObject.Destroy(tempGO);
        return t.localEulerAngles.x;
    }

    float getTilt(Transform originalTransform)
    {
        GameObject tempGO = new GameObject();
        Transform t = tempGO.transform;
        t.localRotation = originalTransform.localRotation;

        t.Rotate(0, 0, t.localEulerAngles.z * -1);
        t.Rotate(t.localEulerAngles.x * -1, 0, 0);

        GameObject.Destroy(tempGO);
        return t.localEulerAngles.y;
    }
}
