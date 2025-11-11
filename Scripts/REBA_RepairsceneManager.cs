using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_RepairsceneManager : MonoBehaviour
{
    public GameObject valvePrefab; 
    public GameObject[] pipesInOrder;
    public Transform[] pipeTranforms = new Transform[0];
    public Transform valveSpawnPoint;
    public bool canRotateSnappedValve = false;
    public REBA_ExperimentController REBA_ExperimentController_object;
    private GameObject currentValve;
    private int currentPipeIndex = 0;

    void Start()
    {
        UpdateCurrentPipeIndicator();
    }

    void Update()
    {

    }

    public void ResetValvePosition()
    {
        if(currentValve != null)
        {
            currentValve.transform.position = valveSpawnPoint.position;
            currentValve.transform.rotation = valveSpawnPoint.rotation;
        }
    }

    void SpawnValve()
    {
            currentValve = Instantiate(valvePrefab, valveSpawnPoint.position, valveSpawnPoint.rotation);
            currentValve.transform.parent = valveSpawnPoint.transform;
            ResetValvePosition();
    }

    // Hier wird immer das nächste Pipe-Objekt aktiviert
    public void UpdateCurrentPipeIndicator()
    {
        for (int i = 0; i < pipesInOrder.Length; i++)
        {
            Debug.Log("ITERATION: " + i);
            REBA_PipeScript pipeScript = pipesInOrder[i].GetComponent<REBA_PipeScript>();
            if (pipeScript != null)
            {
                pipeScript.isActivePipe = (i == currentPipeIndex);
            }
            else
            {
                pipeScript.isActivePipe = (false);
            }
        }
        if (currentPipeIndex < pipesInOrder.Length)
        {
            Debug.Log("Next light gets active!");
            REBA_BlinkingLight pipeLight = pipesInOrder[currentPipeIndex].GetComponent<REBA_PipeScript>().blinkingLight_object;
            Debug.Log("Name of the next pipe: " + pipesInOrder[currentPipeIndex].gameObject.name);
            if (pipeLight != null)
            {
                // This Light need to be repaired
                pipeLight.SetLightConditionToMaintenanceMode(true); 
            }
            SpawnValve();
        }
    }

    public void OnPipeRepaired()
    {
        // im skript vom pipe gibt es eine referenz zum zugehörigen lampen script.
        pipesInOrder[currentPipeIndex].GetComponent<REBA_PipeScript>().blinkingLight_object.SetPipeFixed(true);
        //currentValve.GetComponent<BoxCollider>().enabled = false;
        //currentValve.GetComponent<Rigidbody>().detectCollisions = false;
        canRotateSnappedValve = false;
        // Wenn es noch pipes gibt erhöhe index und gehe zur nächsten Pipe
        if (currentPipeIndex < pipesInOrder.Length - 1)
        {
            currentPipeIndex++;
            UpdateCurrentPipeIndicator();
        }
        else
        {
            REBA_ExperimentController_object.OnTaskCompleted();
            // feedback experiment manager.
        }
    }

    public void SnapValveToPipe(Transform valveTransform, Transform pipeTransform)
    {
        canRotateSnappedValve = true;
        currentValve.transform.parent = pipeTransform;
        valveTransform.position = pipeTransform.position;
        valveTransform.rotation = pipeTransform.rotation;
        currentValve.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        Rigidbody vaultRB = currentValve.GetComponent<Rigidbody>();
        vaultRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    public GameObject getCurrentValve()
    {
        return currentValve;
    }
}
