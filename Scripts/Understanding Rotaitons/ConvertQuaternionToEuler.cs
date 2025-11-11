using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertQuaternionToEuler : MonoBehaviour
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
        Quaternion q = r_shin.transform.rotation;
        float Pitch = Mathf.Rad2Deg * Mathf.Atan2(2 * q.x * q.w - 2 * q.y * q.z, 1 - 2 * q.x * q.x - 2 * q.z * q.z);
        float Yaw = Mathf.Rad2Deg * Mathf.Atan2(2 * q.y * q.w - 2 * q.x * q.z, 1 - 2 * q.y * q.y - 2 * q.z * q.z);
        float Roll = Mathf.Rad2Deg * Mathf.Asin(2 * q.x * q.y + 2 * q.z * q.w);

        Debug.Log("Converter " + Pitch + " " + Yaw + " " + Roll);
    }
}
