using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarEulerPrint : MonoBehaviour
{

    public GameObject r_upperarm;
    public GameObject r_forearm;
    public GameObject r_hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Local Pose: " + r_upperarm.transform.localEulerAngles + " " + r_forearm.transform.localEulerAngles + " " + r_hand.transform.localEulerAngles);
    }
}
