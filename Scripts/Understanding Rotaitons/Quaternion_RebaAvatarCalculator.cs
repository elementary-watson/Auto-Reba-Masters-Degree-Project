using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaternion_RebaAvatarCalculator : MonoBehaviour
{
    public GameObject r_shoulder, r_shin, r_upperarm, r_forearm;
    public Vector3 initialDirection_r_upperarm, initialDirection_r_shin, initialDirection_r_shoulder;
    Quaternion initialRotation_r_shoulder, initialRotation_r_upperarm, initialRotation_r_forearm, initialRotation_r_shin;
    bool canCalcQuaternions;
    public GameObject cubeTesting;

    //cube
    public Vector3 initialCubeTesting;
    Quaternion cubeTestingPLain;
    private Vector3 lowerArmR_calibrated;

    void Start()
    {
        lowerArmR_calibrated = new Vector3();
        canCalcQuaternions = false;
        cubeTestingPLain = cubeTesting.transform.rotation;

    }
    public void setInitialPose()
    {
        initialRotation_r_shoulder = r_shoulder.transform.rotation;
        initialRotation_r_upperarm = r_upperarm.transform.rotation;
        initialRotation_r_forearm = r_forearm.transform.rotation;
        initialRotation_r_shin = r_shin.transform.rotation;

        lowerArmR_calibrated = r_forearm.transform.position;
       
        canCalcQuaternions = true;
    }
    // Update is called once per frame
    void Update()
    {
        cubeTestingPLain = cubeTesting.transform.rotation;

        //Quaternion currentRotationCube = Quaternion.Inverse(cubeTesting.transform.rotation);
        Debug.Log("PLAIN CubeA: " + cubeTestingPLain);
        Debug.Log("THIS 0 CubeA: " + cubeTestingPLain[0]);
        Debug.Log("THIS 1 CubeA: " + cubeTestingPLain[1]);
        Debug.Log("THIS 2 CubeA: " + cubeTestingPLain[2]);
        Debug.Log("THIS 3 CubeA: " + cubeTestingPLain[3]);

        Debug.Log("W CubeA: " + cubeTestingPLain.w);
        Debug.Log("x CubeA: " + cubeTestingPLain.x);
        Debug.Log("y CubeA: " + cubeTestingPLain.y);
        Debug.Log("z CubeA: " + cubeTestingPLain.z);
        Debug.Log("eulerAngles CubeA: " + cubeTestingPLain.eulerAngles);
        Debug.Log("normalized CubeA: " + cubeTestingPLain.normalized);
        Debug.Log("Invers CubeA: " + Quaternion.Inverse(cubeTesting.transform.rotation));
        if (canCalcQuaternions)
        {
            // Cube       

            Quaternion currentRotation_r_upperarm = Quaternion.Inverse(initialRotation_r_upperarm) * r_upperarm.transform.rotation;
            Quaternion currentRotation_r_shoulder = Quaternion.Inverse(initialRotation_r_shoulder) * r_shoulder.transform.rotation;
            Quaternion simple_r_upperarm = r_upperarm.transform.rotation;

            Debug.Log("PLAIN Quaternion upperarm " + simple_r_upperarm);

            Debug.Log("CURRENT ROT Quaternion upperarm " + currentRotation_r_upperarm);

            //angle upper arm right(1)
            float angleUpperArmR = 0f;
            angleUpperArmR = Quaternion.Angle(currentRotation_r_upperarm, currentRotation_r_shoulder);
            Debug.Log("REBA Right Upper Arm Angle:" + angleUpperArmR);
        }
    }
}
