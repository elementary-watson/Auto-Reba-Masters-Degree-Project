using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_PlaneVector3to2Converter : MonoBehaviour
{    
    public GameObject r_shoulder;
    public GameObject r_upperarm;
    public GameObject r_forearm;
    public GameObject r_hand;
    public GameObject r_armSagittalPlane;
    public GameObject r_lowerArmSagittalPlane;
    
    private Vector3 initialPoint;

    float tempUpperArmAngle;
    float tempLowerArmAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Update the position as before
        Vector3 offset = r_shoulder.transform.up * 1; // Change the multiplier as needed
        r_armSagittalPlane.transform.position = r_shoulder.transform.position + offset;
        // Now, rotate the plane to face the arm
        Vector3 directionToArm = r_shoulder.transform.position - r_armSagittalPlane.transform.position;
        Quaternion directionRotation = Quaternion.LookRotation(directionToArm, r_shoulder.transform.right);
        Quaternion additionalRotation = Quaternion.Euler(90, 0, 0); // This represents a 90 degrees rotation about x-axis
        r_armSagittalPlane.transform.rotation = directionRotation * additionalRotation;
        // Reset the y rotation to 0
        //r_armSagittalPlane.transform.rotation = Quaternion.Euler(r_armSagittalPlane.transform.rotation.eulerAngles.x, 0, r_armSagittalPlane.transform.rotation.eulerAngles.z);

        /*
        // Update the position as before
        Vector3 offsetLowerArm = r_forearm.transform.right * 1; // Change the multiplier as needed
        r_lowerArmSagittalPlane.transform.position = r_forearm.transform.position + offsetLowerArm;
        // Now, rotate the plane to face the arm
        Vector3 directionToLowerArm = r_forearm.transform.position - r_lowerArmSagittalPlane.transform.position;
        Quaternion directionLowerArmRotation = Quaternion.LookRotation(directionToLowerArm, r_forearm.transform.right);
        Quaternion additionalLowerArmRotation = Quaternion.Euler(90, 0, 0); // This represents a 90 degrees rotation about x-axis
        r_lowerArmSagittalPlane.transform.rotation = directionLowerArmRotation * additionalLowerArmRotation;
        //r_lowerArmSagittalPlane.transform.rotation = Quaternion.Euler(r_lowerArmSagittalPlane.transform.rotation.eulerAngles.x, 0, r_lowerArmSagittalPlane.transform.rotation.eulerAngles.z);
        */
        //// Update the position as before
        //Vector3 offsetLowerArm = r_forearm.transform.right * 1; // Change the multiplier as needed
        //r_lowerArmSagittalPlane.transform.position = r_forearm.transform.position + offsetLowerArm;

        //// Make the plane face the reference bone, but keep the rotation parallel to the ground
        //Vector3 directionToBone = (r_forearm.transform.position - r_lowerArmSagittalPlane.transform.position).normalized;

        //// Plane's forward direction should be this calculated direction but without any Y-axis rotation
        //Vector3 planeForward = new Vector3(directionToBone.x, 0, directionToBone.z);

        //// This quaternion represents rotation parallel to the floor but facing the reference bone
        //Quaternion rotationParallelToFloor = Quaternion.LookRotation(planeForward, Vector3.up);

        //// Use the roll angle of the original rotation
        //float rollAngle = r_lowerArmSagittalPlane.transform.eulerAngles.z;
        //Quaternion additionalRollRotation = Quaternion.Euler(-90, 0, rollAngle);

        //// Apply the rotations
        //r_lowerArmSagittalPlane.transform.rotation = rotationParallelToFloor * additionalRollRotation;

        //===========================

        //// Position the plane's center at the forearm's location offset in the forearm's right direction
        //Vector3 offsetLowerArm = r_forearm.transform.right * 1; // Change the multiplier as needed
        //r_lowerArmSagittalPlane.transform.position = r_forearm.transform.position + offsetLowerArm;

        //// Vector from the plane's center to the forearm
        //Vector3 directionToBone = r_forearm.transform.position - r_lowerArmSagittalPlane.transform.position;

        //// Make the plane's forward direction (local x-axis) face the forearm
        //Quaternion rotationFacingForearm = Quaternion.LookRotation(directionToBone, Vector3.up);
        //Quaternion additionalRollRotation = Quaternion.Euler(-90, 0, 0);
        //r_lowerArmSagittalPlane.transform.rotation = rotationFacingForearm * additionalRollRotation;

        //// Set the plane's position offset relative to the forearm
        //Vector3 offsetLowerArm = r_forearm.transform.right * 1; // Adjust the multiplier as needed
        //r_lowerArmSagittalPlane.transform.position = r_forearm.transform.position + offset;

        //// Calculate the direction of the upper arm
        //Vector3 upperArmDirection = (r_upperarm.transform.position - r_shoulder.transform.position).normalized;

        //// We want the plane's x-axis to be along the upper arm's direction, its y-axis to be upwards, and its z-axis to be the cross product of these two directions
        //Vector3 planeRight = upperArmDirection;
        //Vector3 planeUp = Vector3.Cross(upperArmDirection, (r_forearm.transform.position - r_upperarm.transform.position).normalized);
        //Vector3 planeForward = Vector3.Cross(planeUp, planeRight);
        //Quaternion additionalLowerarmRotation = Quaternion.Euler(0, 0, 90);
        //// Construct the rotation of the plane using these vectors
        //Quaternion rotation = Quaternion.LookRotation(planeForward, planeUp);
        //r_lowerArmSagittalPlane.transform.rotation = rotation * additionalLowerarmRotation;

        // Set the plane's position to the midpoint of the upper arm and forearm
        Vector3 midpoint = (r_upperarm.transform.position + r_forearm.transform.position) / 2;
        r_lowerArmSagittalPlane.transform.position = midpoint;

        // Calculate the direction of the upper arm
        Vector3 upperArmDirection = (r_upperarm.transform.position - r_shoulder.transform.position).normalized;

        // Calculate the direction of the forearm
        Vector3 lowerArmDirection = (r_hand.transform.position - r_forearm.transform.position).normalized;

        // We want the plane's x-axis to be along the upper arm's direction, its y-axis to be upwards, and its z-axis to be the cross product of these two directions
        Vector3 planeRight = upperArmDirection;
        Vector3 planeUp = Vector3.up; // Here we keep the y-axis of the plane always aligned with the world up direction
        Vector3 planeForward = Vector3.Cross(planeUp, planeRight);

        // Construct the rotation of the plane using these vectors
        Quaternion rotation = Quaternion.LookRotation(planeForward, planeUp);
        r_lowerArmSagittalPlane.transform.rotation = rotation;

        // The plane's rotation is set such that it won't roll around its forward direction (the forearm won't roll), and its right direction is fixed along the upper arm


        Vector2 lowerArmReferenceDirection = Vector2.up; // This represents the direction of the forearm at the start

        // Calculate current positions
        //Vector2 shoulderCurrentPosition = GetPointOnPlane(r_shoulder.transform, r_armSagittalPlane);
        Vector2 upperArmCurrentPosition = new Vector2(0,0);//GetPointOnPlane(r_upperarm.transform, r_armSagittalPlane);
        Vector2 UPPERlowerArmCurrentPosition = new Vector2(0, 0);//GetPointOnPlane(r_forearm.transform, r_armSagittalPlane);
        Vector2 lowerArmCurrentPosition = GetPointOnPlane(r_forearm.transform, r_lowerArmSagittalPlane);
        Vector2 handCurrentPosition = GetPointOnPlane(r_hand.transform, r_lowerArmSagittalPlane);


        // Calculate angles
        float upperArmAngle = Vector2.SignedAngle(upperArmCurrentPosition, upperArmCurrentPosition);
        float lowerArmAngle = Vector2.SignedAngle(lowerArmReferenceDirection, handCurrentPosition);

        
        // Log angles
        Debug.Log("Upper arm angle: " + (upperArmAngle - tempUpperArmAngle));
        Debug.Log("Lower arm angle: " + (lowerArmAngle - tempLowerArmAngle));

        // Reset angles when space is pressed	
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempUpperArmAngle = upperArmAngle;
            tempLowerArmAngle = lowerArmAngle;

            Debug.Log("updateInit");
        }
    }
    private Vector2 GetPointOnPlane(Transform bone, GameObject plane)
    {
        // Plane's normal
        Vector3 planeNormal = plane.transform.up;

        // Vector from the plane point to the bone
        Vector3 planeToBone = bone.position - plane.transform.position;

        // Calculate the distance from the bone to the plane along the plane's normal
        float distance = Vector3.Dot(planeToBone, planeNormal);

        // Calculate the closest point on the plane to the bone
        Vector3 closestPoint = bone.position - planeNormal * distance;

        // Now, we need to convert this point to the plane's local space
        Vector3 localPoint = plane.transform.InverseTransformPoint(closestPoint);

        // Use Debug.DrawLine to visualize the shortest line
        Debug.DrawLine(bone.position, closestPoint, Color.yellow);

        // Log local x and z
        Debug.Log("Bone " + bone.name + " Local x: " + localPoint.x.ToString("F2") + " Local z: " + localPoint.z.ToString("F2"));

        // Return 2D coordinates on the plane
        return new Vector2(localPoint.x, localPoint.z);
    }

    //private Vector2 GetPointOnPlane(Transform bone, GameObject plane)
    //{
    //    RaycastHit hit;
    //    Vector3 direction = plane.transform.position - bone.position;

    //    // Use Debug.DrawRay to visualize the raycast
    //    Debug.DrawRay(bone.position, direction, Color.yellow);

    //    if (Physics.Raycast(bone.position, direction, out hit, Mathf.Infinity))
    //    {
    //        Vector3 localPoint = plane.transform.InverseTransformPoint(hit.point);
    //        Debug.Log("Bone " + bone.name + " Local x: " + localPoint.x + " Local z: " + localPoint.z);
    //        return new Vector2(localPoint.x, localPoint.z);
    //    }
    //    else
    //    {
    //        Debug.LogError("Raycast from " + bone.name + " did not hit " + plane.name);
    //        return Vector2.zero;
    //    }
    //}
}