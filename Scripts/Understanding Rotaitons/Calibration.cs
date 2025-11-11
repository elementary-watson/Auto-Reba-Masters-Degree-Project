using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Calibration : MonoBehaviour
{
    public GameObject r_upperarm;
    public GameObject r_forearm;
    public GameObject r_hand;
    public GameObject r_shoulder;
    public GameObject neckLower;
    public GameObject head;
    public GameObject spineLower;
    public GameObject spineUpper;
    public GameObject spineLowerObject;
    public GameObject spineUpperObject;
    public GameObject r_thigh;
    public GameObject r_shin;
    public GameObject l_shin;
    private Quaternion quat_hand;
    private Quaternion quat_forearm;
    private Quaternion quat_r_upperarm;   
    private Quaternion quat_r_shoulder;
    private Quaternion quat_neckLower;
    private Quaternion quat_neckREBA;
    private Quaternion quat_head;
    private Quaternion quat_spineLower;
    private Quaternion quat_spineUpper;

    //XOF delete after test is done
    private Quaternion quat_spineLowerObject;
    private Quaternion quat_spineUpperObject;
    private Quaternion initialRotationX;
    private Quaternion initialRotationY;
    private Quaternion initialRotationZ;
    public GameObject referenceX;
    public GameObject referenceY;
    public GameObject referenceZ;

    Quaternion initialRotationThigh;
    Quaternion initialRotationShin;


    private Quaternion quat_r_thigh;
    private Quaternion quat_r_shin;
    Quaternion initialRotation;

    int neckREBAScore = 0; // The REBA score for the neck
    int neckTwistScore = 0;
    int neckBendScore = 0;

    int spineREBAScore = 0; // The REBA score for the spine
    int spineTwistScore = 0;
    int spineBendScore = 0;
    // The REBA score for the legs
    int l_legREBAScore = 0; 
    int r_legREBAScore = 0; 

    float initialThreshold = 0f; // The initial rotation value BRAUCHEN WIR DAS?
    float flexionThreshold = 20f; // The threshold for neck flexion WENN DRÜBER DANN GIBTS PUNKTE
    float adductionAbductionTolerance = 5f; // Threshhold for flexion VLT RAUSMACHEN
    float twistTolerance = 5f; // Threshhold for flexion VLT RAUSMACHEN
    float flexionExtensionTolerance = 5f; // Threshhold for flexion VLT RAUSMACHEN

    
    int tableAScore;
    int tableBScore;
    int tableCScore;

    REBA_TableABC REBA_TableABC_object;
    // Start is called before the first frame update
    void Start()
    {
        //delete me XOF after I work
        initialRotationThigh = r_thigh.transform.rotation;
        initialRotationShin = r_shin.transform.rotation;
        // Align the GameObjects with the shin bone
        referenceX.transform.up = r_shin.transform.up;
        referenceX.transform.right = r_thigh.transform.right;
        referenceX.transform.forward = r_thigh.transform.forward;

        referenceY.transform.up = r_shin.transform.up;
        referenceY.transform.right = r_thigh.transform.right;
        referenceY.transform.forward = r_thigh.transform.forward;

        referenceZ.transform.up = r_shin.transform.up;
        referenceZ.transform.right = r_thigh.transform.right;
        referenceZ.transform.forward = r_thigh.transform.forward;
        initialRotation = r_upperarm.transform.localRotation;

        REBA_TableABC_object = new REBA_TableABC();

        quat_r_shoulder = r_shoulder.transform.localRotation;
        quat_r_upperarm = r_upperarm.transform.localRotation;
        quat_forearm = r_forearm.transform.localRotation;
        quat_hand = r_hand.transform.localRotation;
        quat_neckLower = neckLower.transform.localRotation;
        
        quat_head = head.transform.localRotation;
        quat_neckREBA =  quat_head * quat_neckLower;
        //Vector3 relPosInitial = r_forearm.transform.position - r_upperarm.transform.position;
        //quat_upperarm = Quaternion.LookRotation(relPosInitial, r_upperarm.transform.up);

        quat_spineLower = spineLower.transform.localRotation;
        quat_spineUpper = spineUpper.transform.localRotation;
        
        quat_r_thigh = r_thigh.transform.localRotation;
        quat_r_shin = r_shin.transform.localRotation;

        //delete after done
        initialRotationX = Quaternion.AngleAxis(spineLower.transform.rotation.eulerAngles.x, Vector3.right);
        initialRotationY = Quaternion.AngleAxis(spineLower.transform.rotation.eulerAngles.y, Vector3.up);
        initialRotationZ = Quaternion.AngleAxis(spineLower.transform.rotation.eulerAngles.z, Vector3.forward);

        //chatgpt anglerotation
        quat_spineLowerObject = spineLowerObject.transform.rotation;
        quat_spineUpperObject = spineUpperObject.transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        Quaternion q = Quaternion.Inverse(quat_r_upperarm) * r_upperarm.transform.localRotation;
        //Quaternion q1 = Quaternion.Inverse(quat_neckLower) * neckLower.transform.rotation;
        //Vector3 relPos = r_forearm.transform.position - r_upperarm.transform.position;
        //Quaternion q1 = Quaternion.LookRotation(relPos, r_upperarm.transform.up);
        //Quaternion q = Quaternion.Inverse(quat_upperarm) * q1;
        Quaternion q_neckTemp = neckLower.transform.localRotation * head.transform.localRotation;

        // Ergebnisse der Berechnung von Step 1 bestimmen Score von Tabelle A. int [Z-Achse (Stockwerk), X-Achse, Y-Achse] 
        // Bei Arrays zählen wir logisch deshalb immer mit -1 aufrufen.
        tableAScore = REBA_TableABC_object.tableA[step1_getNeckScore() - 1, step1_getTrunkScore() - 1, step1_getLegScore() - 1];
        Debug.Log("Table A: " + tableAScore);

        step2_getUpperarmScore();

        // Calculate the current rotation relative to the initial rotation
        Quaternion relativeRotation = Quaternion.Inverse(initialRotation) * r_upperarm.transform.localRotation;

        // The axis you want to measure the rotation around
        // Since you want to measure the rotation around the X-axis, the axis would be (1, 0, 0)
        Vector3 axis = new Vector3(1, 0, 0);

        // Create a rotation that represents a rotation of 0 degrees around the axis
        Quaternion identityRotation = Quaternion.AngleAxis(0, axis);

        // Calculate the angle of the rotation around the axis
        float angle = Quaternion.Angle(identityRotation, relativeRotation);

        // Get the current rotations of the thigh and shin bones
        Quaternion currentRotationThigh = r_thigh.transform.rotation;
        Quaternion currentRotationShin = r_shin.transform.rotation;

        // Calculate the relative rotations from the initial positions
        Quaternion relativeRotationThigh = Quaternion.Inverse(initialRotationThigh) * currentRotationThigh;
        Quaternion relativeRotationShin = Quaternion.Inverse(initialRotationShin) * currentRotationShin;

        // Calculate the signed angles
        float angleX = Vector3.SignedAngle(referenceX.transform.up, r_shin.transform.up, r_thigh.transform.right);
        float angleY = Vector3.SignedAngle(referenceY.transform.up, r_shin.transform.up, r_thigh.transform.up);
        float angleZ = Vector3.SignedAngle(referenceZ.transform.up, r_shin.transform.up, r_thigh.transform.forward);
        Debug.Log("SignedAngleTest " + angleX + " " + angleY + " " + angleZ);

        // Calculate the relative rotation from the thigh to the shin
        Quaternion relativeLegRotation = Quaternion.Inverse(currentRotationThigh) * currentRotationShin;

        // Use ToAngleAxis to get the angle and axis of rotation
        float myAngle;
        Vector3 myAxis;
        relativeLegRotation.ToAngleAxis(out myAngle, out myAxis);
        // Define the six possible axes
        Vector3[] axes = new Vector3[]
        {
        Vector3.right,
        -Vector3.right,
        Vector3.up,
        -Vector3.up,
        Vector3.forward,
        -Vector3.forward
        };

        // For each axis...
        for (int i = 0; i < axes.Length; i++)
        {
            // Calculate the angle between the thigh and shin around this axis
            float tempAngle = Quaternion.Angle(r_thigh.transform.rotation, r_shin.transform.rotation);

            // Log the result
            Debug.Log("Axis " + i + ": " + angle);
        }

    }

    float getRotationAngle(Transform a, Transform b, Transform c, Transform d)
    {
        Vector3 ba = a.position - b.position;
        Vector3 bc = c.position - b.position;
        Vector3 bd = d.position - b.position;
        // Calculate the axis vector pointing towards the axis object
        // Vector3 axisVector = bd.normalized;
        float rotationAngle = Vector3.SignedAngle(ba, bc, bd);
        return rotationAngle;
    }

    public int step1_getNeckScore()
    {
        Quaternion q_necklowerTemp = Quaternion.Inverse(quat_neckLower) * neckLower.transform.localRotation;
        Quaternion q_headTemp = Quaternion.Inverse(quat_head) * head.transform.localRotation;

        Quaternion q_neckREBA = q_headTemp * q_necklowerTemp;
        //q_neckREBA = q_neckREBA * Quaternion.Euler(180.0f, 180.0f, 0.0f);
        Debug.Log("REBA neck " + q_neckREBA.eulerAngles);
        //Debug.Log("Head " + head.transform.localRotation.eulerAngles);
        Vector3 neckEulerAngles = q_neckREBA.eulerAngles;

        // Handle wrap-around at 0 and 360 degrees
        if (neckEulerAngles.x > 180)
        {
            neckEulerAngles.x -= 360;
        }

        if (neckEulerAngles.y > 180)
        {
            neckEulerAngles.y -= 360;
        }

        if (neckEulerAngles.z > 180)
        {
            neckEulerAngles.z -= 360;
        }

        // Check if the X rotation exceeds the initial threshold plus the tolerance
        if (neckEulerAngles.x > initialThreshold + flexionExtensionTolerance)
        {
            // If the X rotation (flexion) exceeds 20 degrees, set the score to 2
            if (neckEulerAngles.x > flexionThreshold)
            {
                neckREBAScore = 2;
            }
            // Otherwise, if it's within the 0-20 degrees range, set the score to 1
            else if (neckEulerAngles.x > 0 && neckEulerAngles.x <= flexionThreshold)
            {
                neckREBAScore = 1;
            }
        }
        // If we're rotating in the opposite direction beyond the tolerance, set the score to 2
        else if (neckEulerAngles.x < initialThreshold - flexionExtensionTolerance)
        {
            neckREBAScore = 2;
        }
        // If we're within the tolerance, set the score to 1
        else
        {
            neckREBAScore = 1;
        }

        // Check if the Y rotation (twist) exceeds the tolerance
        if (Mathf.Abs(neckEulerAngles.y) > twistTolerance)
        {
            neckTwistScore = 1;
        }
        else
        {
            neckTwistScore = 0;
        }

        // Check if the Z rotation (bend) exceeds the tolerance
        if (Mathf.Abs(neckEulerAngles.z) > adductionAbductionTolerance)
        {
            neckBendScore = 1;
        }
        else
        {
            neckBendScore = 0;
        }

        // if either one of them or both (twist or bend) is bigger than 0 we increment the neck score only by 1 
        int twistAndBendScore = 0;
        if (neckTwistScore == 1 || neckBendScore == 1)
            twistAndBendScore = 1;
        //Debug.Log("Mathf.Abs Neck y: " + Mathf.Abs(neckEulerAngles.y) + " z: " + Mathf.Abs(neckEulerAngles.z));

        Debug.Log("Neck REBA Score: " + (neckREBAScore + twistAndBendScore) + "\n" +
        "Neck Twist Score: " + neckTwistScore + "Neck Bend Score: " + neckBendScore);
        
        return neckREBAScore + twistAndBendScore;
    }

    public int step1_getTrunkScore()
    {
        Quaternion q_spinelowerTemp = Quaternion.Inverse(quat_spineLower) * spineLower.transform.localRotation;
        Quaternion q_spineUpperTemp = Quaternion.Inverse(quat_spineUpper) * spineUpper.transform.localRotation;
        //Quaternion q_spinelowerObjectTemp = Quaternion.Inverse(quat_spineLowerObject) * spineLowerObject.transform.rotation;
        //Quaternion q_spineUpperObjectTemp = Quaternion.Inverse(quat_spineUpperObject) * spineUpperObject.transform.rotation;
        //Quaternion q_spineREBATEMP = q_spineUpperObjectTemp * q_spinelowerObjectTemp;
        Quaternion q_spineREBA = q_spineUpperTemp * q_spinelowerTemp;
        //q_neckREBA = q_neckREBA * Quaternion.Euler(180.0f, 180.0f, 0.0f);
        //Debug.Log("REBA spine " + q_spineREBA.eulerAngles);
        //Debug.Log("Head " + head.transform.localRotation.eulerAngles);
        Quaternion q_rotator = Quaternion.Euler(0, 180, 0);
        Quaternion q_SpineResult = q_rotator * q_spineREBA;
        Vector3 spineEulerAngles = q_SpineResult.eulerAngles;


        // Handle wrap-around at 0 and 360 degrees
        if (spineEulerAngles.x > 180)
        {
            spineEulerAngles.x -= 360;
        }

        if (spineEulerAngles.y > 180)
        {
            spineEulerAngles.y -= 360;
        }

        if (spineEulerAngles.z > 180)
        {
            spineEulerAngles.z -= 360;
        }

        // Check if the X rotation exceeds the initial threshold plus the tolerance
        if (spineEulerAngles.x > initialThreshold + flexionExtensionTolerance)
        {
            // If the X rotation (flexion) exceeds 60 degrees, set the score to 4
            if (spineEulerAngles.x > 60.0f)
            {
                spineREBAScore = 4;
            }
            // If the X rotation (flexion) is between 20 and 60 degrees, set the score to 3
            else if (spineEulerAngles.x > 20.0f)
            {
                spineREBAScore = 3;
            }
            // If the X rotation (flexion) is between 0 and 20 degrees, set the score to 2
            else if (spineEulerAngles.x > 0)
            {
                spineREBAScore = 2;
            }
        }
        // If we're rotating in the opposite direction beyond the tolerance
        else if (spineEulerAngles.x < initialThreshold - flexionExtensionTolerance)
        {
            // If the extension is more than 20 degrees, set the score to 3
            if (spineEulerAngles.x < -flexionThreshold)
            {
                spineREBAScore = 3;
            }
            // If the extension is within 0 to 20 degrees, set the score to 2
            else if (spineEulerAngles.x < 0 && spineEulerAngles.x >= -flexionThreshold)
            {
                spineREBAScore = 2;
            }
        }
        // If we're within the tolerance, set the score to 1
        else
        {
            spineREBAScore = 1;
        }

        // Check if the Y rotation (twist) exceeds the tolerance
        if (Mathf.Abs(spineEulerAngles.y) > twistTolerance)
        {
            spineTwistScore = 1;
        }
        else
        {
            spineTwistScore = 0;
        }

        // Check if the Z rotation (bend) exceeds the tolerance
        if (Mathf.Abs(spineEulerAngles.z) > adductionAbductionTolerance)
        {
            spineBendScore = 1;
        }
        else
        {
            spineBendScore = 0;
        }

        // if either one of them or both (twist or bend) is bigger than 0 we increment the neck score only by 1 
        int twistAndBendScore = 0;
        if (spineTwistScore == 1 || spineBendScore == 1)
            twistAndBendScore = 1;

        //Debug.Log("SPINE TEST " + q_spineREBATEMP + " Euler: " + q_spineREBATEMP.eulerAngles);
        Debug.Log("Spine REBA Score: " + (spineREBAScore + twistAndBendScore) + "\n" +
        "Spine Twist Score: " + spineTwistScore + "Spine Bend Score: " + spineBendScore);
        Debug.Log("change Spine Converted Eulerangles: " + spineEulerAngles + " Eulerangles: " + q_spineREBA.eulerAngles);

        return spineREBAScore + twistAndBendScore;
    }

    public int step1_getLegScore()
    {
        Quaternion q_rightShinTemp = Quaternion.Inverse(quat_r_shin) * r_shin.transform.localRotation;
        Quaternion q_rightThighTemp = Quaternion.Inverse(quat_r_thigh) * r_thigh.transform.localRotation;

        Quaternion q_legREBA = q_rightShinTemp * q_rightThighTemp;

        Vector3 rightLegAngles = q_legREBA.eulerAngles;

        if (rightLegAngles.x > 180)
        {
            rightLegAngles.x -= 360;
        }

        float r_shinEulerAngles = r_shin.transform.localEulerAngles.x;
        float l_shinEulerAngles = l_shin.transform.localEulerAngles.x;

        bool bothFeetOnGround = true;
        if (bothFeetOnGround)
        {
            // right Leg Flexion Score
            if (r_shinEulerAngles > 30 && r_shinEulerAngles <= 60)
            {
                r_legREBAScore = 1;
            }
            else if (r_shinEulerAngles > 60)
            {
                r_legREBAScore = 2;
            }
            else
            {
                r_legREBAScore = 1;
            }
            // left Leg Flexion Score
            if (l_shinEulerAngles > 30 && l_shinEulerAngles <= 60)
            {
                l_legREBAScore = 1;
            }
            else if (l_shinEulerAngles > 60)
            {
                l_legREBAScore = 2;
            }
            else
            {
                l_legREBAScore = 1;
            }
        }
        Debug.Log("Shin r X: " + r_shinEulerAngles+ " Shin l X: " + l_shinEulerAngles);
        Debug.Log("rightLegAngles: " + rightLegAngles);
        Debug.Log("Left Leg score: " + l_legREBAScore + " Right Leg score: " + r_legREBAScore);
        int finalLegScore = 0;
        if (l_legREBAScore >= r_legREBAScore)
            finalLegScore = l_legREBAScore;
        else
            finalLegScore = r_legREBAScore;

        return finalLegScore;
    } 

    public int step2_getUpperarmScore()
    {
        Quaternion q_rightShoulder = Quaternion.Inverse(quat_r_shoulder) * r_shoulder.transform.localRotation;
        Quaternion q_rightUpperarm = Quaternion.Inverse(quat_r_upperarm) * r_upperarm.transform.localRotation;

        Quaternion q_rightArmREBA = q_rightUpperarm * q_rightShoulder;

        Vector3 r_ShoulderEulerAngles = q_rightArmREBA.eulerAngles;
        if (r_ShoulderEulerAngles.x > 180)
        {
            r_ShoulderEulerAngles.x -= 360;
        }

        if (r_ShoulderEulerAngles.y > 180)
        {
            r_ShoulderEulerAngles.y -= 360;
        }

        if (r_ShoulderEulerAngles.z > 180)
        {
            r_ShoulderEulerAngles.z -= 360;
        }

        Debug.Log("Upperarm Converted Eulerangles: " + r_ShoulderEulerAngles + " Eulerangles: " + q_rightArmREBA.eulerAngles);


        return 0;
    }
}
