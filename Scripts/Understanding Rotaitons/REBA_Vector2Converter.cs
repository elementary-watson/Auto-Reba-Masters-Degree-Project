using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_Vector2Converter : MonoBehaviour
{
    public GameObject r_upperarm;
    public GameObject r_forearm;
    public GameObject sphere_A;
    public GameObject sphere_B;

    private Vector3 initialUpperArmDirection;
    private Vector3 initialForearmDirection;

    void Start()
    {
        // Store the initial directions of the upper arm and forearm
        initialUpperArmDirection = r_upperarm.transform.up; // Assuming the bones' "up" direction is along their length
        initialForearmDirection = r_forearm.transform.up;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Store the initial directions of the upper arm and forearm
            initialUpperArmDirection = r_upperarm.transform.up; // Assuming the bones' "up" direction is along their length
            initialForearmDirection = r_forearm.transform.up;
        }
        // Calculate the current directions of the upper arm and forearm
        Vector3 currentUpperArmDirection = r_upperarm.transform.up;
        Vector3 currentForearmDirection = r_forearm.transform.up;

        // Subtract the initial directions from the current directions to get the change in direction
        Vector3 deltaUpperArmDirection = currentUpperArmDirection - initialUpperArmDirection;
        Vector3 deltaForearmDirection = currentForearmDirection - initialForearmDirection;

        // Project the 3D direction deltas onto the XZ plane to get 2D vectors
        Vector2 upperArmDirection2D = new Vector2(deltaUpperArmDirection.x, deltaUpperArmDirection.z);
        Vector2 forearmDirection2D = new Vector2(deltaForearmDirection.x, deltaForearmDirection.z);

        // Calculate the angle between the upper arm and forearm
        float angle = Vector2.SignedAngle(upperArmDirection2D, forearmDirection2D);

        // Print the angle to the console
        Debug.Log("Upper arm to forearm angle: " + angle);
        Debug.Log("Upper arm to local: " + r_upperarm.transform.localPosition + "forearm " + r_forearm.transform.localPosition);
        Debug.Log("Upper arm to world: " + r_upperarm.transform.position + "forearm " + r_forearm.transform.position);

        Vector3 upperarmPos = new Vector3(r_upperarm.transform.position.x + 1, r_upperarm.transform.position.y, r_upperarm.transform.position.z);
        sphere_A.transform.localPosition = upperarmPos;
        Vector3 lowerarmPos = new Vector3(r_forearm.transform.position.x + 1, r_forearm.transform.position.y, r_forearm.transform.position.z);
        sphere_B.transform.localPosition = lowerarmPos;
    }
}
