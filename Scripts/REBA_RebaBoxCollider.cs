using PCS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class REBA_RebaBoxCollider : MonoBehaviour
{
    [Header("General")]
    public TextMeshProUGUI weightMachineFeedback;
    public TextMeshProUGUI conveyerInterfaceFeedback;
    private Coroutine weightMachineCoroutine;
    const string weightMachineTag = "Weight_Machine";
    const string conveyerEndpoint = "Conveyor_Endpoint";
    const string conveyerStartpoint = "Conveyer_Startpoint";
    public GameObject conveyerEndpointGameobject;
    public GameObject conveyerStartpointGameobject;

    public REBA_ExperimentController REBA_ExperimentController_object;
    public bool canConveyorMove;
    public bool mustWeightRebaBox;
    private bool isMovingTowardsEnd; // Tracks the direction of conveyor movement
    public bool boxWillMove = false;
    public GameObject rebaBox;
    public GameObject copyBox;

    Vector3 copyBoxPosition;
    private float conveyorSpeed = 1.0f;

    public bool flagEndpoint;
    public bool flagStartpoint;

    // Number of loops for this task - 3 times
    public int taskCounter;
    // A boolean state that tells the experimenter and the class that the user is ready
    public bool isInPanelMode;

    void Start()
    {
        isInPanelMode = false;
        taskCounter = 0;
        weightMachineFeedback.text = "Ready";
        canConveyorMove = false;
        flagEndpoint = false;  
        flagStartpoint = false;
        mustWeightRebaBox = true;

        isMovingTowardsEnd = true;

        copyBoxPosition = copyBox.transform.position;
        StartCoroutine(UpdateFeedbackTextAfterDelay());
    }

    void Update()
    {
        //if (taskCounter > 2)
        //{
        //    REBA_ExperimentController_object.OnTaskCompleted();
        //    isInPanelMode = false;
        //}
        if (boxWillMove)
        {
            // Calculate the next position of the copy box along the conveyor path
            // if the flag isMovingTowardsEnd is false we will move the towards the start!
            Vector3 targetPosition = isMovingTowardsEnd ? conveyerEndpointGameobject.transform.position : conveyerStartpointGameobject.transform.position;
            copyBox.transform.position = Vector3.MoveTowards(copyBox.transform.position, targetPosition, conveyorSpeed * Time.deltaTime);

            // Check if the box reached the end or start point
            if (copyBox.transform.position == targetPosition)
            {
                if (isMovingTowardsEnd)
                {
                    // Box reached the end, now move it back to the start
                    isMovingTowardsEnd = false;
                    Debug.Log("Box reached the end point");
                }
                else
                {
                    // Box reached the start, switch the boxes
                    Debug.Log("ELSE Triggered box reached the start again");
                    SwitchBoxes(false);
                    boxWillMove = false; // Stop the conveyor movement
                    mustWeightRebaBox = true;
                    canConveyorMove = false;
                    taskCounter++;
                    Debug.Log("Count to next loop " + taskCounter);
                    conveyerInterfaceFeedback.text = "Standby";
                    isMovingTowardsEnd = true;
                }
            }
        }
    }
    private IEnumerator UpdateFeedbackTextAfterDelay()
    {
        yield return new WaitForSeconds(2);  // Wait for 2 seconds
        weightMachineFeedback.text = "Ready";  // Update the text
    }
    public void setConveyerState(bool givePowerToConveyer)
    {
        // Methode wird nur einmal ausgeführt und deshalb setzen wir hier nur die flags und tauschen die box
        Debug.Log("Can Conveyer move?: " + givePowerToConveyer);
        if (givePowerToConveyer)
        {
            SwitchBoxes(true);
            boxWillMove = true;
        }
        if(!givePowerToConveyer)
        {
            boxWillMove = false;
        }
    }
    public void SwitchBoxes(bool canCreateFake)
    {
        if (canCreateFake)
        {
            // move the copy box towards to the reba box an copy rotation while switching the mesh visibility
            rebaBox.GetComponent<MeshRenderer>().enabled = false;
            copyBox.GetComponent<MeshRenderer>().enabled = true;
            copyBox.transform.position = rebaBox.transform.position;
            copyBox.transform.rotation = rebaBox.transform.rotation;
        }
        else
        {
            rebaBox.GetComponent<MeshRenderer>().enabled = true;
            copyBox.GetComponent<MeshRenderer>().enabled = false;
            copyBox.transform.position = copyBoxPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isInPanelMode) { 
            if (other.CompareTag(weightMachineTag) && mustWeightRebaBox == true)
            {
                weightMachineFeedback.text = "Calculating...";
                if (weightMachineCoroutine != null)
                {
                    StopCoroutine(weightMachineCoroutine);
                }
                weightMachineCoroutine = StartCoroutine(WeightMachineTimer());
            }
            if (other.CompareTag(conveyerStartpoint) && mustWeightRebaBox == false)
            {
                conveyerInterfaceFeedback.text = "Touch display to continue";
                canConveyorMove = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isInPanelMode)
        {
            Debug.Log("REBA OnTriggerExit");
            weightMachineFeedback.text = "Ready to use";

            if (other.CompareTag(weightMachineTag) && mustWeightRebaBox == true)
            {
                weightMachineFeedback.text = "Please put the packet back!";
                if (weightMachineCoroutine != null)
                {
                    StopCoroutine(weightMachineCoroutine);
                }
            }
            if (other.CompareTag(conveyerStartpoint) && mustWeightRebaBox == false)
            {
                canConveyorMove = false;
            }
        }
    }
    private IEnumerator WeightMachineTimer()
    {        
        yield return new WaitForSeconds(7);
        weightMachineFeedback.text = "Done! Please continue";
        mustWeightRebaBox = false;
    }

}