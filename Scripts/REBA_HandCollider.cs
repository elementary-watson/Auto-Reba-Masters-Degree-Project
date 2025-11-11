using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class REBA_HandCollider : MonoBehaviour
{
    [Header("General")]
    public REBA_ControllerInput REBA_ControllerInput_object;
    public REBA_Calculation REBA_Calculation_object;

    [Header("Packet Warehouse Scene")]
    public TextMeshProUGUI tmp_computerDisplayText;
    public REBA_RebaBoxCollider REBA_RebaBoxCollider_object;
    const string computerDisplayTag = "Computer_Display";

    [Header("RepairStation Scene")]
    private Rigidbody vaultRb;
    GameObject heldVaultObject;
    const string vaultTag = "Gate_Valve";
    const string rebaboxTag = "REBA_Box";
    public REBA_RepairsceneManager repairsceneManager_object;
    public GameObject metalboxSpawnpoint;
    public GameObject handpositionSnappoint;
    public bool isHoldingObject = false;
    public float rotationDuration = 5.0f; // Duration in seconds to complete a full rotation
    private float currentRotation = 0.0f; // Tracks the current rotation


    private void Update()
    {
        // Wenn valve collider den Hand collider triggern sollte dann
        if (isHoldingObject) 
        {
            // dann checke ob die valve schon an der pipe befestigt wurde
            // -> die methode SnapValveToPipe wird vom collider der pipe ausgefuehrt, sodass eine flag gesetzt wird
            if (repairsceneManager_object.canRotateSnappedValve)
            {
                if (this.tag == "l_handCollider" && REBA_ControllerInput_object.leftControllerTriggerIsPressed ||
                    this.tag == "r_handCollider" && REBA_ControllerInput_object.rightControllerTriggerIsPressed)
                {
                    if (currentRotation < 360f)
                    {
                        float rotationThisFrame = (360f / rotationDuration) * Time.deltaTime;
                        heldVaultObject.transform.Rotate(0, rotationThisFrame, 0); // Assuming Y-axis rotation
                        currentRotation += rotationThisFrame;
                    }
                    else if (currentRotation >= 360f)
                    {
                        repairsceneManager_object.OnPipeRepaired();
                        isHoldingObject = false;
                        currentRotation = 0.0f;
                    }
                }


            }
            // ansonsten wird die valve zu einem objekt mit physik wenn user nicht grab taste drueckt
            else if (this.tag == "l_handCollider" && !REBA_ControllerInput_object.leftControllerGrabIsPressed ||
                this.tag == "r_handCollider" && !REBA_ControllerInput_object.rightControllerGrabIsPressed)
            {
                Debug.Log("Let go.");

                vaultRb.isKinematic = false;
                vaultRb.useGravity = true;
                //Vector3 originalScale = new Vector3(0.060435202f, 0.060435202f, 0.060435202f);  // Store the original scale
                //heldVaultObject.transform.SetParent(metalboxSpawnpoint.transform);  // Detach the vault from the hand
                //heldVaultObject.transform.localScale = originalScale;
                //isHoldingObject = false;
                Vector3 worldPosition = heldVaultObject.transform.position;
                Quaternion worldRotation = heldVaultObject.transform.rotation;
                heldVaultObject.transform.SetParent(metalboxSpawnpoint.transform);

                heldVaultObject.transform.position = worldPosition;
                heldVaultObject.transform.rotation = worldRotation;

            }
            // falls er die grab taste drueckt wird die valve aufgehoben und ist kind vom hand objekt
            else
            {
                Debug.Log("Lets grab!");

                vaultRb.isKinematic = true;
                vaultRb.useGravity = false;
                heldVaultObject.transform.SetParent(handpositionSnappoint.transform);
                heldVaultObject.transform.localPosition = new Vector3(0, 0.0888f, -0.0848f);  // Apply the corrected scale
                heldVaultObject.transform.localRotation = Quaternion.Euler(0, -90f, 90f);  // Apply the corrected scale
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == repairsceneManager_object.getCurrentValve())
        {
            heldVaultObject = other.gameObject;
            Debug.Log(gameObject.name + " is touching a vault.");
            vaultRb = other.GetComponent<Rigidbody>();  

            // Check for right hand collision with vault while grab button is pressed
            //if (this.tag == "r_handCollider" && REBA_ControllerInput_object.rightControllerGrabIsPressed == true)
            if (this.tag == "r_handCollider")
            {
                Debug.Log("Found vault with right hand: " + other.name);
                isHoldingObject = true;
            }
            // Check for left hand collision with vault while grab button is pressed
            //else if (this.tag == "l_handCollider" && REBA_ControllerInput_object.leftControllerGrabIsPressed == true)
            else if (this.tag == "l_handCollider")
            {
                Debug.Log("Found vault with left hand: " + other.name);
                //vaultRb.isKinematic = true;
                //vaultRb.useGravity = false;
                //other.transform.SetParent(handpositionSnappoint.transform);
                //other.transform.localPosition = new Vector3(0, 0.0888f, -0.0848f);  // Apply the corrected scale
                //other.transform.localRotation = Quaternion.Euler(0, -90f, 90f);  // Apply the corrected scale
                isHoldingObject = true;
            }
        }
        if (other.CompareTag(computerDisplayTag) && REBA_RebaBoxCollider_object.canConveyorMove == true)
        {
            Debug.Log(gameObject.name + " is pressing the display.");
            REBA_RebaBoxCollider_object.setConveyerState(true);
            REBA_RebaBoxCollider_object.conveyerInterfaceFeedback.text = "Please dont move your hand away.";
        }
        if (other.CompareTag(rebaboxTag))
        {
            Debug.Log(gameObject.name + "is touching the REBA Box");
            REBA_Calculation_object.couplingScore = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == repairsceneManager_object.getCurrentValve())
        {
            if (this.tag == "r_handCollider" && REBA_ControllerInput_object.rightControllerGrabIsPressed == false)
            {
                isHoldingObject = false;
                vaultRb.isKinematic = false;
                vaultRb.useGravity = true;
                //other.transform.SetParent(metalboxSpawnpoint.transform);  // Detach the vault from the hand
            }
            else if (this.tag == "l_handCollider" && REBA_ControllerInput_object.leftControllerGrabIsPressed == false)
            {
                isHoldingObject = false;
                vaultRb.isKinematic = false;
                vaultRb.useGravity = true;
                //other.transform.SetParent(metalboxSpawnpoint.transform);
            }
        }

        if (other.CompareTag(computerDisplayTag) && REBA_RebaBoxCollider_object.canConveyorMove == true)
        {
            Debug.Log(gameObject.name + " has stopped pressing the display.");
            REBA_RebaBoxCollider_object.setConveyerState(false);
            REBA_RebaBoxCollider_object.conveyerInterfaceFeedback.text = "Please return your hand to the display.";
        }
        if (other.CompareTag(rebaboxTag))
        {
            REBA_Calculation_object.couplingScore = 0;
        }
    }

}
