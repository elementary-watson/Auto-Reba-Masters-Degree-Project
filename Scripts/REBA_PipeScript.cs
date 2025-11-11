using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_PipeScript : MonoBehaviour
{
    public bool isActivePipe = false;
    public REBA_RepairsceneManager repairSceneManager;
    public REBA_BlinkingLight blinkingLight_object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActivePipe && other.CompareTag("Gate_Valve"))
        {
            repairSceneManager.SnapValveToPipe(other.transform, this.transform);
            BoxCollider vaultBColl = other.GetComponent<BoxCollider>();
            vaultBColl.isTrigger = true;
        }
    }
}
