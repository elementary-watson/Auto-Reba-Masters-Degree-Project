using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_Playerarea : MonoBehaviour
{
    const string vaultTag = "Gate_Valve";
    public REBA_RepairsceneManager repairSceneManager;

    void OnTriggerEnter(Collider other)
    {
        if (repairSceneManager != null)
        {
            if (other.gameObject == repairSceneManager.getCurrentValve())
            {
                Debug.Log("Valve has left the area!");
                // Valve has left the player area
                repairSceneManager.ResetValvePosition();
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
