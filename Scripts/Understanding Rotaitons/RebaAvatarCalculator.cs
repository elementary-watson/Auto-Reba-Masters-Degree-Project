//#define isBoneRotating
//#define useGPTCode

using CjLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebaAvatarCalculator : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public GameObject cubeBlue;
    public GameObject cubeGreen;
    public Quaternion cubeBlueQuaternion;
    public GameObject whatever;// up node vector

    public GameObject hip;
    public GameObject l_shoulder;
    public GameObject r_shoulder;
    public GameObject pelvis;
    public GameObject neck1, neck2;
    public GameObject spine1, spine2, spine3, spine4;
    public GameObject l_upperarm, r_upperarm;
    public GameObject l_forearm, r_forearm;
    public GameObject l_hand, r_hand;
    public GameObject l_thigh, r_thigh, l_shin, r_shin;

    public bool canCalcQuaternions;

    private Quaternion initialRotation_neck1, initialRotation_neck2,
        initialRotation_spine1, initialRotation_spine2,
        initialRotation_spine3, initialRotation_spine4,
        initialRotation_l_shoulder, initialRotation_r_shoulder,
        initialRotation_l_upperarm, initialRotation_r_upperarm,
        initialRotation_l_forearm, initialRotation_r_forearm,
        initialRotation_l_hand, initialRotation_r_hand,
        initialRotation_l_thigh, initialRotation_r_thigh,
        initialRotation_l_shin, initialRotation_r_shin;
    public Vector3 initialDirection_r_upperarm, initialDirection_r_shin, initialDirection_r_shoulder;

    void Start()
    {
        canCalcQuaternions = false;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    public void setInitialStartposition()
    {
        // Quaternions from rotation
        initialRotation_neck1 = neck1.transform.rotation;
        initialRotation_neck2 = neck2.transform.rotation;
        initialRotation_spine1 = spine1.transform.rotation;
        initialRotation_spine2 = spine2.transform.rotation;
        initialRotation_spine3 = spine3.transform.rotation;
        initialRotation_spine4 = spine4.transform.rotation;
        initialRotation_l_shoulder = l_shoulder.transform.rotation;
        initialRotation_r_shoulder = r_shoulder.transform.rotation;
        initialRotation_l_upperarm = l_upperarm.transform.rotation;
        initialRotation_r_upperarm = r_upperarm.transform.rotation;
        initialRotation_l_forearm = l_forearm.transform.rotation;
        initialRotation_r_forearm = r_forearm.transform.rotation;
        initialRotation_l_hand = l_hand.transform.rotation;
        initialRotation_r_hand = r_hand.transform.rotation;
        initialRotation_l_thigh = l_thigh.transform.rotation;
        initialRotation_r_thigh = r_thigh.transform.rotation;
        initialRotation_l_shin = l_shin.transform.rotation;
        initialRotation_r_shin = r_shin.transform.rotation;

        //Directions of bones
        initialDirection_r_upperarm = r_upperarm.transform.up;
        initialDirection_r_shin = r_shin.transform.up;
        initialDirection_r_shoulder = r_shoulder.transform.up;

        canCalcQuaternions = true;
    }
    // Decompose a rotation into a swing rotation and a twist rotation
    //public static void DecomposeSwingTwist(Quaternion q, Vector3 direction, out Quaternion swing, out Quaternion twist)
    //{
    //    Vector3 r = new Vector3(q.x, q.y, q.z);  // Quaternion axis
    //    Vector3 p = Vector3.Project(r, direction);  // Projection of r onto direction

    //    twist = new Quaternion(p.x, p.y, p.z, q.w).normalized;

    //    swing = q * Quaternion.Inverse(twist);
    //}
    void DecomposeRotationGPT(Quaternion rotation, out Quaternion alongY, out Quaternion alongZ)
    {
        // Project the rotation onto the y-axis (flexion/extension)
        Vector3 yComponent = Vector3.Project(new Vector3(rotation.x, rotation.y, rotation.z), Vector3.up);
        alongY = new Quaternion(yComponent.x, yComponent.y, yComponent.z, rotation.w).normalized;

        // Project the rotation onto the z-axis (abduction/adduction)
        Vector3 zComponent = Vector3.Project(new Vector3(rotation.x, rotation.y, rotation.z), Vector3.forward);
        alongZ = new Quaternion(zComponent.x, zComponent.y, zComponent.z, rotation.w).normalized;
    }

    public static void DecomposeSwingTwist
    (
      Quaternion q,
      Vector3 twistAxis,
      out Quaternion swing,
      out Quaternion twist
    )
    {
        Vector3 r = new Vector3(q.x, q.y, q.z); // (rotaiton axis) * cos(angle / 2)

        // singularity: rotation by 180 degree
        if (r.sqrMagnitude < MathUtil.Epsilon)
        {
            Vector3 rotatedTwistAxis = q * twistAxis;
            Vector3 swingAxis = Vector3.Cross(twistAxis, rotatedTwistAxis);

            if (swingAxis.sqrMagnitude > MathUtil.Epsilon)
            {
                float swingAngle = Vector3.Angle(twistAxis, rotatedTwistAxis);
                swing = Quaternion.AngleAxis(swingAngle, swingAxis);
            }
            else
            {
                // more singularity: rotation axis parallel to twist axis
                swing = Quaternion.identity; // no swing
            }

            // always twist 180 degree on singularity
            twist = Quaternion.AngleAxis(180.0f, twistAxis);
            return;
        }

        // formula & proof: 
        // http://www.euclideanspace.com/maths/geometry/rotations/for/decomposition/
        Vector3 p = Vector3.Project(r, twistAxis);
        twist = new Quaternion(p.x, p.y, p.z, q.w);
        twist = Normalize(twist);
        swing = q * Quaternion.Inverse(twist);
    }
    public static Quaternion Normalize(Quaternion q)
    {
        float magInv = 1.0f / Magnitude(q);
        return new Quaternion(magInv * q.x, magInv * q.y, magInv * q.z, magInv * q.w);
    }

    public static float Magnitude(Quaternion q)
    {
        return Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
    }

    public static float MagnitudeSqr(Quaternion q)
    {
        return q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
    }

    Quaternion swing_Quat, twist_Quat;
    void Update()
    {
        Debug.Log("Excitement " + r_forearm.transform.localEulerAngles);
        Debug.Log("Hope: " + getValueHope(r_hand.transform, r_forearm.transform));
        Debug.Log("Sadness: " + getValueHope(r_forearm.transform, r_upperarm.transform));
        Debug.DrawRay(r_forearm.transform.position, r_forearm.transform.forward, Color.red);
        Debug.DrawRay(whatever.transform.position, whatever.transform.forward, Color.green);
        
        //Debug.Log("Excitement " + getAnglePositionViaPlane(r_upperarm.transform, r_forearm.transform, r_hand.transform, whatever.transform));
        Debug.Log("Excitement " + getRotationAngle(r_upperarm.transform, r_forearm.transform, r_hand.transform, whatever.transform));
        //Debug.Log("Excitement " + DecomposeSwingTwist(r_forearm.transform));

        lineRenderer.SetPosition(0, r_forearm.transform.position);
        lineRenderer.SetPosition(1, whatever.transform.position);

        if (canCalcQuaternions)
        {
            /*
            // Get the current rotation of the bone
            Quaternion currentRotation = r_upperarm.transform.rotation;

            // Calculate the rotation relative to the initial pose
            Quaternion relativeRotation = Quaternion.Inverse(initialRotation_r_upperarm) * currentRotation;

            cubeBlueQuaternion = new Quaternion(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);
            cubeBlue.transform.rotation = cubeBlueQuaternion;
            // Define the axes of rotation
            Vector3 flexionExtensionAxis = Vector3.up;  // Adjust as needed
            Vector3 abductionAdductionAxis = Vector3.forward;  // Adjust as needed

            // Project the relative rotation onto the axes of rotation
            float flexionExtensionAngle = Quaternion.Angle(Quaternion.identity, Quaternion.AngleAxis(Vector3.Dot(relativeRotation.eulerAngles, flexionExtensionAxis), flexionExtensionAxis));
            float abductionAdductionAngle = Quaternion.Angle(Quaternion.identity, Quaternion.AngleAxis(Vector3.Dot(relativeRotation.eulerAngles, abductionAdductionAxis), abductionAdductionAxis));

            // Print the angles
            Debug.Log("Bone flexion/extension: " + flexionExtensionAngle);
            Debug.Log("Bone abduction/adduction: " + abductionAdductionAngle);
            */
#if useGPTCode //DecomposeRotation

            //shoulder
            Quaternion currentRotation_r_shoulder = r_shoulder.transform.rotation;
            //arms
            Quaternion currentRotation_r_upperarm = r_upperarm.transform.rotation;
            Quaternion currentRotation_r_forearm = r_forearm.transform.rotation;
            //legs
            Quaternion currentRotation_r_shin = r_shin.transform.rotation;

            Quaternion rotationChange_r_upperarm = FindRotationQuaternion(initialRotation_r_upperarm, currentRotation_r_upperarm);
            Quaternion rotationChange_r_forearm = FindRotationQuaternion(initialRotation_r_forearm, currentRotation_r_forearm);

            Quaternion rotationChange_r_shin = FindRotationQuaternion(initialRotation_r_shin, currentRotation_r_shin);

            Quaternion rotationChange_r_shoulder = FindRotationQuaternion(initialRotation_r_shoulder, currentRotation_r_shoulder);

            // Draw rays for initial directions
            Debug.DrawRay(r_shoulder.transform.position, initialDirection_r_shoulder, Color.red);
            Debug.DrawRay(r_upperarm.transform.position, initialDirection_r_upperarm, Color.red);
            Debug.DrawRay(r_shin.transform.position, initialDirection_r_shin, Color.red);

            // Draw rays for current directions
            Debug.DrawRay(r_shoulder.transform.position, r_shoulder.transform.up, Color.green);
            Debug.DrawRay(r_upperarm.transform.position, r_upperarm.transform.up, Color.green);
            Debug.DrawRay(r_shin.transform.position, r_shin.transform.up, Color.green);

            // gpt quaternions
            // Decompose the rotation change into flexion/extension and abduction/adduction
            DecomposeRotation(rotationChange_r_upperarm, out Quaternion flexionExtension, out Quaternion abductionAdduction);
            // Calculate the angles of the decomposed rotations
            flexionExtensionAngle = Quaternion.Angle(Quaternion.identity, flexionExtension);
            abductionAdductionAngle = Quaternion.Angle(Quaternion.identity, abductionAdduction);
            // Print the angles
            Debug.Log("Here Upper arm flexion/extension: " + flexionExtensionAngle);
            Debug.Log("Here Upper arm abduction/adduction: " + abductionAdductionAngle);


            // upperarm twist and swing
            DecomposeSwingTwist(rotationChange_r_upperarm, initialDirection_r_upperarm, out Quaternion swing, out Quaternion twist);

            Vector3 swingEulerAngles = swing.eulerAngles;  // Flexion/extension and abduction/adduction
            Vector3 twistEulerAngles = twist.eulerAngles;  // Internal/external rotation
            Debug.Log("Vector3 upperarm swingEulerAngles " + swingEulerAngles);
            Debug.Log("Vector3 upperarm twistEulerAngles " + twistEulerAngles);

            // Convert to signed angles
            swingEulerAngles.x = ConvertToSignedAngle(swingEulerAngles.x);
            swingEulerAngles.y = ConvertToSignedAngle(swingEulerAngles.y);
            swingEulerAngles.z = ConvertToSignedAngle(swingEulerAngles.z);
            Debug.Log("Converted upperarm swingEulerAngles.x " + swingEulerAngles.x);
            Debug.Log("Converted upperarm swingEulerAngles.y " + swingEulerAngles.y);
            Debug.Log("Converted upperarm swingEulerAngles.z " + swingEulerAngles.z);

            twistEulerAngles.x = ConvertToSignedAngle(twistEulerAngles.x);
            twistEulerAngles.y = ConvertToSignedAngle(twistEulerAngles.y);
            twistEulerAngles.z = ConvertToSignedAngle(twistEulerAngles.z);
            Debug.Log("Converted upperarm twistEulerAngles.x " + twistEulerAngles.x);
            Debug.Log("Converted upperarm twistEulerAngles.y " + twistEulerAngles.y);
            Debug.Log("Converted upperarm twistEulerAngles.z " + twistEulerAngles.z);

            DecomposeSwingTwist(rotationChange_r_shin, initialDirection_r_shin, out swing_Quat, out twist_Quat);
            swingEulerAngles = swing_Quat.eulerAngles;  // Flexion/extension and abduction/adduction
            twistEulerAngles = twist_Quat.eulerAngles;  // Internal/external rotation
            Debug.Log("Vector3 swingEulerAngles shin" + swingEulerAngles);
            Debug.Log("Vector3 twistEulerAngles shin" + twistEulerAngles);

            // Convert to signed angles
            swingEulerAngles.x = ConvertToSignedAngle(swingEulerAngles.x);
            swingEulerAngles.y = ConvertToSignedAngle(swingEulerAngles.y);
            swingEulerAngles.z = ConvertToSignedAngle(swingEulerAngles.z);
            Debug.Log("Converted shin swingEulerAngles.x " + swingEulerAngles.x);
            Debug.Log("Converted shin swingEulerAngles.y " + swingEulerAngles.y);
            Debug.Log("Converted shin swingEulerAngles.z " + swingEulerAngles.z);

            twistEulerAngles.x = ConvertToSignedAngle(twistEulerAngles.x);
            twistEulerAngles.y = ConvertToSignedAngle(twistEulerAngles.y);
            twistEulerAngles.z = ConvertToSignedAngle(twistEulerAngles.z);
            Debug.Log("Converted shin twistEulerAngles.x " + twistEulerAngles.x);
            Debug.Log("Converted shin twistEulerAngles.y " + twistEulerAngles.y);
            Debug.Log("Converted shin twistEulerAngles.z " + twistEulerAngles.z);

            DecomposeSwingTwist(rotationChange_r_shoulder, initialDirection_r_shoulder, out swing_Quat, out twist_Quat);
            swingEulerAngles = swing_Quat.eulerAngles;  // Flexion/extension and abduction/adduction
            twistEulerAngles = twist_Quat.eulerAngles;  // Internal/external rotation
            Debug.Log("Vector3 swingEulerAngles shoulder" + swingEulerAngles);
            Debug.Log("Vector3 twistEulerAngles shoulder" + twistEulerAngles);

            // Convert to signed angles
            swingEulerAngles.x = ConvertToSignedAngle(swingEulerAngles.x);
            swingEulerAngles.y = ConvertToSignedAngle(swingEulerAngles.y);
            swingEulerAngles.z = ConvertToSignedAngle(swingEulerAngles.z);
            Debug.Log("Converted shoulder swingEulerAngles.x " + swingEulerAngles.x);
            Debug.Log("Converted shoulder swingEulerAngles.y " + swingEulerAngles.y);
            Debug.Log("Converted shoulder swingEulerAngles.z " + swingEulerAngles.z);

            twistEulerAngles.x = ConvertToSignedAngle(twistEulerAngles.x);
            twistEulerAngles.y = ConvertToSignedAngle(twistEulerAngles.y);
            twistEulerAngles.z = ConvertToSignedAngle(twistEulerAngles.z);
            Debug.Log("Converted shoulder twistEulerAngles.x " + twistEulerAngles.x);
            Debug.Log("Converted shoulder twistEulerAngles.y " + twistEulerAngles.y);
            Debug.Log("Converted shoulder twistEulerAngles.z " + twistEulerAngles.z);

            Debug.Log("Rotation change of r_upperarm: " + rotationChange_r_upperarm);
            Debug.Log("Rotation change of r_forearm: " + rotationChange_r_forearm);
            

            Vector3 rotationChangeEulerAngles = rotationChange_r_upperarm.eulerAngles;

            Debug.Log("Rotation change of r_upperarm in Euler angles: " + rotationChangeEulerAngles);

            float angle;
            Vector3 axis;
            rotationChange_r_upperarm.ToAngleAxis(out angle, out axis);

            Debug.Log("Rotation change of r_upperarm in axis-angle: " + angle + " degrees around " + axis);
#endif
#if isBoneRotating
        CheckBoneRotation(neck1.transform, initialRotation_neck1, "neck1");
        CheckBoneRotation(neck2.transform, initialRotation_neck2, "neck2");
        CheckBoneRotation(spine1.transform, initialRotation_spine1, "s1");
        CheckBoneRotation(spine2.transform, initialRotation_spine2, "s2");
        CheckBoneRotation(spine3.transform, initialRotation_spine3, "s3");
        CheckBoneRotation(spine4.transform, initialRotation_spine4, "s4");
        CheckBoneRotation(l_upperarm.transform, initialRotation_l_upperarm, "l_upperarm");
        CheckBoneRotation(r_upperarm.transform, initialRotation_r_upperarm, "r_upperarm");
        CheckBoneRotation(l_forearm.transform, initialRotation_l_forearm, "l_fore");
        CheckBoneRotation(r_forearm.transform, initialRotation_r_forearm, "r_fore");
        CheckBoneRotation(l_hand.transform, initialRotation_l_hand, "l_hand");
        CheckBoneRotation(r_hand.transform, initialRotation_r_hand, "r_hand");
        CheckBoneRotation(l_thigh.transform, initialRotation_l_thigh, "l_thigh");
        CheckBoneRotation(r_thigh.transform, initialRotation_r_thigh, "r_thigh");
        CheckBoneRotation(l_shin.transform, initialRotation_l_shin, "l_shin");
        CheckBoneRotation(r_shin.transform, initialRotation_r_shin, "r_shin");

            // Calculate the trunk rotation using different combinations of bones
            //float trunkRotation1 = CalculateTrunkRotation(spine1.transform, initialRotation_spine1, "pelvis to spine1");
            //float trunkRotation2 = CalculateTrunkRotation(spine4.transform, initialRotation_spine4, "pelvis to spine4");
            //float trunkRotation3 = CalculateTrunkRotation(spine1.transform, spine4.transform, "spine1 to spine4");
            //float armRotation1 = CalculateTrunkRotation(l_forearm.transform, initialRotation_l_upperarm, "pelvis to spine1");
            Debug.Log("GetAngle Forarm upperarm " + getAngle(l_forearm.transform, l_upperarm.transform));

            // wir nutzen hier jetzt position
            Debug.Log(getAnglePosition(l_upperarm.transform, l_forearm.transform, l_hand.transform));
            Debug.Log(getLocalRotationInUnity(l_forearm.transform, l_hand.transform));
#endif
            /*
            Debug.Log("l_Wrist Pitch " + getWristPitch(l_hand.transform.localEulerAngles));
            Debug.Log("l_Wrist Yaw " + getWristYaw(l_hand.transform.localEulerAngles));
            Debug.Log("l_Wrist Roll " + getWristRoll(l_hand.transform.localEulerAngles));
            Debug.Log("l_Wrist Roll_90 " + getWristRoll90(l_hand.transform.localEulerAngles));

            Debug.Log("###############");

            Debug.Log("r_lowerArm Pitch " + getWristPitch(r_forearm.transform.localEulerAngles));
            Debug.Log("r_lowerArm Yaw " + getWristYaw(r_forearm.transform.localEulerAngles));
            Debug.Log("r_lowerArm Roll " + getWristRoll(r_forearm.transform.localEulerAngles));
            Debug.Log("r_lowerArm Roll_90 " + getWristRoll90(r_forearm.transform.localEulerAngles));


            Debug.Log("r_lowerArm Identity getPitch " + getPitch(r_forearm.transform));
            Debug.Log("r_lowerArm Identity getRoll " + getRoll(r_forearm.transform));
            Debug.Log("r_lowerArm Identity getYaw " + getYaw(r_forearm.transform));
            */


            //Debug.Log("r_lowerArm Quaternion  " + FindRotationQuaternion(initialRotation_r_upperarm, initialRotation_r_forearm));
            //Debug.Log("Roll Raw " + l_hand.transform.localEulerAngles.y);


            // Print out the trunk rotations
            //Debug.Log("Trunk rotation (pelvis to spine1): " + trunkRotation1);
            //Debug.Log("Trunk rotation (pelvis to spine4): " + trunkRotation2);
            //Debug.Log("Trunk rotation (spine1 to spine4): " + trunkRotation3);
            //Debug.Log("Arm rotation: " + armRotation1);

        }

    }

    public float getValueHope(Transform src, Transform trg)
    {
        Vector3 trgDir = trg.position - src.position;
        float temp = Vector3.SignedAngle(trgDir, src.forward, Vector3.up);
        return temp;
    }

    Quaternion FindRotationQuaternion(Quaternion outerQuaternion, Quaternion innerQuaternion)
    {
        Quaternion inverse = Quaternion.Inverse(outerQuaternion);
        Quaternion rotation = innerQuaternion * inverse;
        return rotation;
    }
    public static float ConvertToSignedAngle(float angle)
    {
        if (angle > 180)
        {
            return angle - 360;
        }
        else
        {
            return angle;
        }
    }

    float getRotationAngle(Transform a, Transform b, Transform c, Transform d)
    {
        Vector3 ba = a.position - b.position;
        Vector3 bc = c.position - b.position;
        Vector3 bd = d.position - b.position;

        // Calculate the axis vector pointing towards the axis object
        Vector3 axisVector = bd.normalized;

        float rotationAngle = Vector3.SignedAngle(ba, bc, axisVector);

        return rotationAngle;
    }

    float getAnglePositionViaPlane(Transform a, Transform b, Transform c, Transform d)
    {
        Vector3 ba = a.position - b.position;
        Vector3 bc = c.position - b.position;       
        // Calculate the normal vector of the plane formed by the vectors ba and bc
        Vector3 planeNormal = Vector3.Cross(ba, bc).normalized;

        // Calculate the direction vector from the reference object (b) to the up-object
        Vector3 upDirection = d.position - b.position;

        // Project the up-direction vector onto the plane
        Vector3 projectedUpDirection = Vector3.ProjectOnPlane(upDirection, planeNormal);

        // Calculate the rotation angle between the projected up-direction and the world up-vector
        float rotationAngle = Vector3.SignedAngle(ba, bc, projectedUpDirection);

        return rotationAngle;
    }


    float getAnglePosition(Transform a, Transform b, Transform c)
    {
        Vector3 ba = a.position - b.position;
        Vector3 bc = c.position - b.position;
        return Vector3.Angle(ba, bc);
    }
    // der chef sagt an
    Vector3 getLocalRotationInUnity(Transform bone1, Transform bone2)
    {
        //Vector3 targetDir = bone1.position - bone2.position;
        return bone2.localEulerAngles;//Quaternion.Angle(bone1.rotation, bone2.rotation);//Vector3.Angle(targetDir,bone2.forward);
    }
    // rightUpperarm XOF

    float getPitch(Transform t)
    {
        //Quaternion tempQ = t.rotation;
        //t.localRotation = Quaternion.identity;
        return t.localEulerAngles.x;
    }
    float getRoll(Transform t)
    {
        //Quaternion tempQ = t.rotation;
        //t.localRotation = Quaternion.identity;
        return t.localEulerAngles.y;
    }
    float getYaw(Transform t)
    {
        //Quaternion tempQ = t.rotation;
        //t.localRotation = Quaternion.identity;
        return t.localEulerAngles.z;
    }


    // leftHand XOF
    float getWristPitch(Vector3 wrist)
    {
        return Mathf.Repeat(wrist.x + 360, 360) - 360; //wrist - new Vector3(180f);
    }
    float getWristYaw(Vector3 wrist)
    {
        return Mathf.Repeat(wrist.z + 180, 360) - 180; //wrist - new Vector3(180f);
    }
    float getWristRoll(Vector3 wrist)
    {
        return Mathf.Repeat(wrist.y + 180, 360) - 180; //wrist - new Vector3(180f);       
    }
    float getWristRoll90(Vector3 wrist)
    {
        return Mathf.Repeat(wrist.y + 90, 360) - 90; //wrist - new Vector3(180f);       
    }


    void CheckBoneRotation(Transform bone, Quaternion initialRotation, string boneName)
    {
        if (Quaternion.Angle(bone.rotation, initialRotation) > 0.01f)
        {
            Debug.Log(boneName + " is moving");
        }
    }


    // der chef sagt an
    float getAngle(Transform bone1, Transform bone2)
    {

        //Vector3 targetDir = bone1.position - bone2.position;
        return Quaternion.Angle(bone1.rotation, bone2.rotation);//Vector3.Angle(targetDir,bone2.forward);
    }



    float CalculateTrunkRotation(Transform bone1, Quaternion initialRotation1, string description)
    {
        // Calculate the current rotation of the bone relative to its initial rotation
        Quaternion currentRotation1 = Quaternion.Inverse(initialRotation1) * bone1.rotation;

        // Calculate the angle of rotation
        float angle1 = Quaternion.Angle(initialRotation1, currentRotation1);

        // Return the angle
        return angle1;
    }

    float CalculateTrunkRotation(Transform bone1, Transform bone2, string description)
    {
        // Calculate the current rotation of the bone1 relative to bone2
        Quaternion currentRotation = Quaternion.Inverse(bone2.rotation) * bone1.rotation;

        // Calculate the angle of rotation
        float angle = Quaternion.Angle(bone2.rotation, currentRotation);

        // Return the angle
        return angle;
    }
}