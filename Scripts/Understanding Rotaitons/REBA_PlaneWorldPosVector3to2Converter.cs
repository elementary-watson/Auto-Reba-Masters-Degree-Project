using UnityEngine;


public class REBA_PlaneWorldPosVector3to2Converter : MonoBehaviour
{
    #region avatar bones
    public GameObject r_shoulder;
    public GameObject r_upperarm;
    public GameObject r_lowerarm;
    public GameObject r_hand;
    public GameObject r_middleFinger;

    public GameObject l_shoulder;
    public GameObject l_upperarm;
    public GameObject l_lowerarm;
    public GameObject l_hand;
    public GameObject l_middleFinger;

    public GameObject trunkSpine1;
    public GameObject trunkSpine2;
    public GameObject trunkSpine3;
    public GameObject trunkSpine4;
    public GameObject neck;
    public GameObject head;

    public GameObject r_thigh;
    public GameObject r_calf;
    public GameObject r_foot;

    public GameObject l_thigh;
    public GameObject l_calf;
    public GameObject l_foot;


    // Transform references
    private Transform r_shoulderTransform;
    private Transform r_upperarmTransform;
    private Transform r_lowerarmTransform;
    private Transform r_handTransform;
    private Transform r_middleFingerTransform;
    private Transform l_shoulderTransform;
    private Transform l_upperarmTransform;
    private Transform l_lowerarmTransform;
    private Transform l_handTransform;
    private Transform l_middleFingerTransform;
    private Transform r_thighTransform;
    private Transform r_calfTransform;
    private Transform r_footTransform;
    private Transform l_thighTransform;
    private Transform l_calfTransform;
    private Transform l_footTransform;
    private Transform trunkSpine1Transform;
    private Transform trunkSpine2Transform;
    private Transform trunkSpine3Transform;
    private Transform trunkSpine4Transform;
    private Transform neckTransform;
    private Transform headTransform;

    #endregion

    #region planes

    public GameObject neckSagittalPlane;
    public GameObject neckCoronalPlane;
    public GameObject trunkSagittalPlane;
    public GameObject trunkCoronalPlane;

    public GameObject r_legSagittalPlane;
    public GameObject r_legCoronalPlane;

    public GameObject l_legSagittalPlane;
    public GameObject l_legCoronalPlane;

    public GameObject r_upperArmSagittalPlane;
    public GameObject r_upperArmCoronalPlane;
    public GameObject r_lowerArmSagittalPlane;

    public GameObject r_handSagittalPlane;
    public GameObject r_handArmCoronalPlane;

    public GameObject l_upperArmSagittalPlane;
    public GameObject l_upperArmCoronalPlane;
    public GameObject l_lowerArmSagittalPlane;

    public GameObject l_handSagittalPlane;
    public GameObject l_handArmCoronalPlane;

    // Twist planes
    public GameObject r_wristHorizontalPlane;
    public GameObject l_wristHorizontalPlane;
    public GameObject neckHorizontalPlane;
    public GameObject trunkHorizontalPlane;

    REBA_TableABC REBA_TableABC_object;


    // Renderer references
    private Renderer[] neckSagittalPlaneRenderers;
    private Renderer[] neckCoronalPlaneRenderers;
    private Renderer[] trunkSagittalPlaneRenderers;
    private Renderer[] trunkCoronalPlaneRenderers;
    private Renderer[] r_legSagittalPlaneRenderers;
    private Renderer[] r_legCoronalPlaneRenderers;
    private Renderer[] l_legSagittalPlaneRenderers;
    private Renderer[] l_legCoronalPlaneRenderers;
    private Renderer[] r_upperArmSagittalPlaneRenderers;
    private Renderer[] r_upperArmCoronalPlaneRenderers;
    private Renderer[] r_lowerArmSagittalPlaneRenderers;
    private Renderer[] r_handSagittalPlaneRenderers;
    private Renderer[] r_handArmCoronalPlaneRenderers;
    private Renderer[] r_wristHorizontalPlaneRenderers;
    private Renderer[] l_wristHorizontalPlaneRenderers;
    private Renderer[] neckHorizontalPlaneRenderers;
    private Renderer[] trunkHorizontalPlaneRenderers;

    // Transform references
    private Transform neckSagittalPlaneTransform;
    private Transform neckCoronalPlaneTransform;
    private Transform trunkSagittalPlaneTransform;
    private Transform trunkCoronalPlaneTransform;
    private Transform r_legSagittalPlaneTransform;
    private Transform r_legCoronalPlaneTransform;
    private Transform l_legSagittalPlaneTransform;
    private Transform l_legCoronalPlaneTransform;
    private Transform r_upperArmSagittalPlaneTransform;
    private Transform r_upperArmCoronalPlaneTransform;
    private Transform r_lowerArmSagittalPlaneTransform;
    private Transform r_handSagittalPlaneTransform;
    private Transform r_handArmCoronalPlaneTransform;
    private Transform l_upperArmSagittalPlaneTransform;
    private Transform l_upperArmCoronalPlaneTransform;
    private Transform l_lowerArmSagittalPlaneTransform;
    private Transform l_handSagittalPlaneTransform;
    private Transform l_handArmCoronalPlaneTransform;

    private Transform r_wristHorizontalPlaneTransform;
    private Transform l_wristHorizontalPlaneTransform;
    private Transform neckHorizontalPlaneTransform;
    private Transform trunkHorizontalPlaneTransform;
    
    #endregion

    float tempUpperArmAngle;
    int tempLowerArmAngle;
    int tempUpperArmAngleCoronal;
    private Vector3 initialForearmUp;
    public float damping = 1.0f;
    Quaternion initialRotation;
    Vector3 initialForearmRight;

    Vector3 midpointNeck;
    Vector3 midpointRightLeg;
    Vector3 midpointLeftLeg;
    Vector3 midpointTrunk;
    Vector3 midpointRightUpperarm;
    Vector3 midpointRightForearm;
    Vector3 midpointRightHand;
    Vector3 midpointLeftUpperarm;
    Vector3 midpointLeftForearm;
    Vector3 midpointLeftHand;

    Vector3 neckSagittalOffset;
    Vector3 neckCoronalOffset;
    Vector3 r_legSagittalOffset;
    Vector3 r_legCoronalOffset;    
    Vector3 l_legSagittalOffset;
    Vector3 l_legCoronalOffset;

    Vector3 trunkSagittalOffset;
    Vector3 trunkCoronalOffset;
    Vector3 rightArmSagittalOffset;
    Vector3 rightArmCoronalOffset;
    Vector3 r_handSagittalOffset;
    Vector3 r_handCoronalOffset;
    Vector3 r_handWristOffsetTwist;

    Quaternion additional90degreeXaxisRotation = Quaternion.Euler(90, 0, 0);
    Quaternion additional90degreeZaxisRotation = Quaternion.Euler(0, 0, 90);

    void Start()
    {
        // The use of Transform components decreases the resource usage
        // Assign Bone Transform references
        r_shoulderTransform = r_shoulder.transform;
        r_upperarmTransform = r_upperarm.transform;
        r_lowerarmTransform = r_lowerarm.transform;
        r_handTransform = r_hand.transform;
        r_middleFingerTransform = r_middleFinger.transform;

        l_shoulderTransform = l_shoulder.transform;
        l_upperarmTransform = l_upperarm.transform;
        l_lowerarmTransform = l_lowerarm.transform;
        l_handTransform = l_hand.transform;
        l_middleFingerTransform = l_middleFinger.transform;

        r_thighTransform = r_thigh.transform;
        r_calfTransform = r_calf.transform;
        r_footTransform = r_foot.transform;
        l_thighTransform = l_thigh.transform;
        l_calfTransform = l_calf.transform;
        l_footTransform = l_foot.transform;

        trunkSpine1Transform = trunkSpine1.transform;
        trunkSpine2Transform = trunkSpine2.transform;
        trunkSpine3Transform = trunkSpine3.transform;
        trunkSpine4Transform = trunkSpine4.transform;
        neckTransform = neck.transform;
        headTransform = head.transform;

        // Assign Plane Transform references
        neckSagittalPlaneTransform = neckSagittalPlane.transform;
        neckCoronalPlaneTransform = neckCoronalPlane.transform;
        trunkSagittalPlaneTransform = trunkSagittalPlane.transform;
        trunkCoronalPlaneTransform = trunkCoronalPlane.transform;
        r_legSagittalPlaneTransform = r_legSagittalPlane.transform;
        r_legCoronalPlaneTransform = r_legCoronalPlane.transform;
        l_legSagittalPlaneTransform = l_legSagittalPlane.transform;
        l_legCoronalPlaneTransform = l_legCoronalPlane.transform;
        r_upperArmSagittalPlaneTransform = r_upperArmSagittalPlane.transform;
        r_upperArmCoronalPlaneTransform = r_upperArmCoronalPlane.transform;
        r_lowerArmSagittalPlaneTransform = r_lowerArmSagittalPlane.transform;
        r_handSagittalPlaneTransform = r_handSagittalPlane.transform;
        r_handArmCoronalPlaneTransform = r_handArmCoronalPlane.transform;

        l_upperArmSagittalPlaneTransform = l_upperArmSagittalPlane.transform;
        l_upperArmCoronalPlaneTransform = l_upperArmCoronalPlane.transform;
        l_lowerArmSagittalPlaneTransform = l_lowerArmSagittalPlane.transform;
        l_handSagittalPlaneTransform = l_handSagittalPlane.transform;
        l_handArmCoronalPlaneTransform = l_handArmCoronalPlane.transform;

        r_wristHorizontalPlaneTransform = r_wristHorizontalPlane.transform;
        l_wristHorizontalPlaneTransform = l_wristHorizontalPlane.transform;
        neckHorizontalPlaneTransform = neckHorizontalPlane.transform;
        trunkHorizontalPlaneTransform = trunkHorizontalPlane.transform;

        // Assign Renderer references
        neckSagittalPlaneRenderers = neckSagittalPlane.GetComponentsInChildren<Renderer>();
        neckCoronalPlaneRenderers = neckCoronalPlane.GetComponentsInChildren<Renderer>();
        trunkSagittalPlaneRenderers = trunkSagittalPlane.GetComponentsInChildren<Renderer>();
        trunkCoronalPlaneRenderers = trunkCoronalPlane.GetComponentsInChildren<Renderer>();
        r_legSagittalPlaneRenderers = r_legSagittalPlane.GetComponentsInChildren<Renderer>();
        r_legCoronalPlaneRenderers = r_legCoronalPlane.GetComponentsInChildren<Renderer>();
        l_legSagittalPlaneRenderers = l_legSagittalPlane.GetComponentsInChildren<Renderer>();
        l_legCoronalPlaneRenderers = l_legCoronalPlane.GetComponentsInChildren<Renderer>();
        r_upperArmSagittalPlaneRenderers = r_upperArmSagittalPlane.GetComponentsInChildren<Renderer>();
        r_upperArmCoronalPlaneRenderers = r_upperArmCoronalPlane.GetComponentsInChildren<Renderer>();
        r_lowerArmSagittalPlaneRenderers = r_lowerArmSagittalPlane.GetComponentsInChildren<Renderer>();
        r_handSagittalPlaneRenderers = r_handSagittalPlane.GetComponentsInChildren<Renderer>();
        r_handArmCoronalPlaneRenderers = r_handArmCoronalPlane.GetComponentsInChildren<Renderer>();
        r_wristHorizontalPlaneRenderers = r_wristHorizontalPlane.GetComponentsInChildren<Renderer>();
        l_wristHorizontalPlaneRenderers = l_wristHorizontalPlane.GetComponentsInChildren<Renderer>();
        neckHorizontalPlaneRenderers = neckHorizontalPlane.GetComponentsInChildren<Renderer>();
        trunkHorizontalPlaneRenderers = trunkHorizontalPlane.GetComponentsInChildren<Renderer>();

        // Turn off all renderers at start
        ToggleRenderers(neckSagittalPlaneRenderers, false);
        ToggleRenderers(neckCoronalPlaneRenderers, false);
        ToggleRenderers(trunkSagittalPlaneRenderers, false);
        ToggleRenderers(trunkCoronalPlaneRenderers, false);
        ToggleRenderers(r_legSagittalPlaneRenderers, false);
        ToggleRenderers(r_legCoronalPlaneRenderers, false);
        ToggleRenderers(l_legSagittalPlaneRenderers, false);
        ToggleRenderers(l_legCoronalPlaneRenderers, false);
        ToggleRenderers(r_upperArmSagittalPlaneRenderers, false);
        ToggleRenderers(r_upperArmCoronalPlaneRenderers, false);
        ToggleRenderers(r_lowerArmSagittalPlaneRenderers, false);
        ToggleRenderers(r_handSagittalPlaneRenderers, false);
        ToggleRenderers(r_handArmCoronalPlaneRenderers, false);
        ToggleRenderers(r_wristHorizontalPlaneRenderers, false);
        ToggleRenderers(l_wristHorizontalPlaneRenderers, false);
        ToggleRenderers(neckHorizontalPlaneRenderers, false);
        ToggleRenderers(trunkHorizontalPlaneRenderers, false);
    }

    // Method to toggle visibility of a group of renderers
    private void ToggleRenderers(Renderer[] renderers, bool state)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = state;
        }
    }

    private void UpdatePlanePosition()
    {
        // Set the plane's position to the midpoint of the upper arm and forearm
        // Damit signedangle die winkel sauber rauszieht muss die plane an das koeperteil angebracht werden dass wir untersuchen

        midpointNeck = (neckTransform.position + headTransform.position) / 2;
        midpointRightLeg = (r_thighTransform.position + r_calfTransform.position) / 2;
        midpointLeftLeg = (l_thighTransform.position + l_calfTransform.position) / 2;
        midpointTrunk = (trunkSpine1Transform.position + trunkSpine2Transform.position) / 2;
        midpointRightUpperarm = (r_upperarmTransform.position + r_lowerarmTransform.position) / 2;
        //XOF Vector3 midpointRightUpperarm = (r_forearmTransform.position);
        // Die plane wird hier auf den unterarm gesetzt damit der unterarm parallel mit der plane verläuft
        // Die idee ist es den arm als den beweglichen arm des goniometers einzustellen und eine andere achse wird als festes gegenstück eingesetzt.
        // Diese feste Achse wird einfach die durch Vector2.up; festgelegt und mit der signedangle methode verrechnet
        midpointRightForearm = (r_lowerarmTransform.position + r_handTransform.position) / 2;
        midpointRightHand = (r_handTransform.position + r_middleFingerTransform.position) / 2;

        midpointLeftUpperarm = (l_upperarmTransform.position + l_lowerarmTransform.position) / 2;
        midpointLeftForearm = (l_lowerarmTransform.position + l_handTransform.position) / 2;
        midpointLeftHand = (l_handTransform.position + l_middleFingerTransform.position) / 2;


        neckSagittalOffset = new Vector3(1, 0, 0);
        neckCoronalOffset = new Vector3(0, 0, 1);

        r_legSagittalOffset = new Vector3(0, 0, 1);
        r_legCoronalOffset = new Vector3(1, 0, 0);
        l_legSagittalOffset = new Vector3(0, 0, -1);
        l_legCoronalOffset = new Vector3(-1, 0, 0);

        trunkSagittalOffset = new Vector3(1, 0, 0);
        trunkCoronalOffset = new Vector3(0, 0, 1);

        rightArmSagittalOffset = new Vector3(0, 1, 0);
        rightArmCoronalOffset = new Vector3(1, 0, 0);

        r_handSagittalOffset = new Vector3(0, 0, 1);
        r_handCoronalOffset = new Vector3(-1, 0, 0);
        r_handWristOffsetTwist = new Vector3(0, 0, 0);

        // Dieser Code sorgt dafür dass sich die planes nicht um die eigene achse drehen wenn der avatar sich dreht.
        // Die planes kriegen einen pivot point zugewiesen sodass sie ihre relative position beibehalten koennen.        
        neckSagittalOffset = neckTransform.rotation * neckSagittalOffset;
        neckCoronalOffset = neckTransform.rotation * neckCoronalOffset;

        r_legSagittalOffset = r_thighTransform.rotation * r_legSagittalOffset;
        r_legCoronalOffset = r_thighTransform.rotation * r_legCoronalOffset;

        trunkSagittalOffset = trunkSpine1Transform.rotation * trunkSagittalOffset;
        trunkCoronalOffset = trunkSpine1Transform.rotation * trunkCoronalOffset;

        // Rotate the offset vector by the avatar's rotation
        rightArmSagittalOffset = r_shoulderTransform.rotation * rightArmSagittalOffset;
        rightArmCoronalOffset = r_shoulderTransform.rotation * rightArmCoronalOffset;

        r_handSagittalOffset = r_lowerarmTransform.rotation * r_handSagittalOffset;
        r_handCoronalOffset = r_lowerarmTransform.rotation * r_handCoronalOffset;
        r_handWristOffsetTwist = r_lowerarmTransform.rotation * r_handWristOffsetTwist;

        //Die vorangegangenen Rechnungen fuer die aktuelle Position und Rotation werden hier angewandt
        neckSagittalPlaneTransform.position = midpointNeck + neckSagittalOffset;
        neckCoronalPlaneTransform.position = midpointNeck + neckCoronalOffset;
        trunkSagittalPlaneTransform.position = midpointTrunk + trunkSagittalOffset;
        trunkCoronalPlaneTransform.position = midpointTrunk + trunkCoronalOffset;

        r_legSagittalPlaneTransform.position = midpointRightLeg + r_legSagittalOffset;
        r_legCoronalPlaneTransform.position = midpointRightLeg + r_legCoronalOffset;
        l_legSagittalPlaneTransform.position = midpointLeftLeg + l_legSagittalOffset;
        l_legCoronalPlaneTransform.position = midpointLeftLeg + l_legCoronalOffset;

        r_upperArmCoronalPlaneTransform.position = midpointRightUpperarm + rightArmCoronalOffset;
        r_upperArmSagittalPlaneTransform.position = midpointRightUpperarm + rightArmSagittalOffset;
        r_lowerArmSagittalPlaneTransform.position = midpointRightForearm + rightArmSagittalOffset;

        l_upperArmCoronalPlaneTransform.position = midpointRightUpperarm + rightArmCoronalOffset;
        l_upperArmSagittalPlaneTransform.position = midpointRightUpperarm + rightArmSagittalOffset;
        l_lowerArmSagittalPlaneTransform.position = midpointRightForearm + rightArmSagittalOffset;

        r_handSagittalPlaneTransform.position = midpointRightHand + r_handSagittalOffset;
        r_handArmCoronalPlaneTransform.position = midpointRightHand + r_handCoronalOffset;

    }

    private void UpdatePlaneRotation() 
    {
        // Calculate the direction of the upper arm
        Vector3 rightUpperArmDirection = (r_upperarmTransform.position - r_shoulderTransform.position).normalized;
        // Calculate the direction of the lower arm
        Vector3 rightLowerArmDirection = (r_lowerarmTransform.position - r_upperarmTransform.position).normalized;

        // Calculate the direction of the upper arm
        Vector3 leftUpperArmDirection = (l_lowerarmTransform.position - l_shoulderTransform.position).normalized;
        // Calculate the direction of the lower arm
        Vector3 leftLowerArmDirection = (l_lowerarmTransform.position - l_upperarmTransform.position).normalized;

        // We want the plane's x-axis to be along the upper arm's direction, its y-axis to be perpendicular to the arm, and its z-axis to be the cross product of these two directions
        Vector3 r_planeRight = rightUpperArmDirection; // planeRight
        Vector3 r_planeUp = Vector3.Cross(rightLowerArmDirection, rightUpperArmDirection); // planeUp // This makes the plane's y-axis always perpendicular to the arm
        Vector3 r_planeForward = Vector3.Cross(r_planeUp, r_planeRight); // planeForward

        // We want the plane's x-axis to be along the upper arm's direction, its y-axis to be perpendicular to the arm, and its z-axis to be the cross product of these two directions
        Vector3 l_planeRight = leftUpperArmDirection; // planeRight
        Vector3 l_planeUp = Vector3.Cross(leftLowerArmDirection, leftUpperArmDirection); // planeUp // This makes the plane's y-axis always perpendicular to the arm
        Vector3 l_planeForward = Vector3.Cross(l_planeUp, l_planeRight); // planeForward


        // Draw the vectors
        //Debug.DrawRay(r_shoulderTransform.position, upperArmDirection, Color.red);
        //Debug.DrawRay(midpoint, planeRight, Color.cyan);
        //Debug.DrawRay(midpoint, lowerArmDirection, Color.red);
        //Debug.DrawRay(r_thighTransform.position, calfDirection, Color.magenta);
        //Debug.DrawRay(r_footTransform.position, footDirection, Color.cyan);
        //Debug.DrawRay(r_calfTransform.position, legDirection, Color.black);
        //Debug.DrawRay(midpoint, planeForward, Color.magenta);
        //Debug.DrawRay(midpoint, planeForwardCoronal, Color.white);
        //Debug.DrawRay(midpoint, planeUpCoronal, Color.green);

        // Construct the rotation of the plane using these vectors
        Quaternion rotationNeckSagittal = Quaternion.LookRotation(neckTransform.right);
        Quaternion rotationTrunkSagittal = Quaternion.LookRotation(trunkSpine1Transform.right);
        Quaternion rotationTrunkCoronal = Quaternion.LookRotation(trunkSpine1Transform.forward);
        Quaternion rotationRightLegSagittal = Quaternion.LookRotation(r_thighTransform.forward);
        Quaternion rotationLeftLegSagittal = Quaternion.LookRotation(l_thighTransform.forward);
        Quaternion rotationRightUpperarmCoronal = Quaternion.LookRotation(rightUpperArmDirection);
        Quaternion rotationRightLowerarmSagittal = Quaternion.LookRotation(r_planeForward, r_planeUp);
        Quaternion rotationRightHandSagittal = Quaternion.LookRotation(r_lowerarmTransform.forward);
        Quaternion rotationRightHandCoronal = Quaternion.LookRotation(r_lowerarmTransform.up, -r_lowerarmTransform.right);
        Quaternion rotationRightHandTwist = Quaternion.LookRotation(r_shoulderTransform.up, r_handTransform.up);

        //Quaternion rotationNeckCoronal = Quaternion.LookRotation(neckTransform.forward);
        // Calculate the angle by which the neck is tilted forward
        float tiltAngle = Vector3.Angle(neckTransform.forward, Vector3.up) - 90;
        // Create a counter-rotation Quaternion
        Quaternion counterRotation = Quaternion.AngleAxis(-tiltAngle, neckTransform.right);
        // Apply the counter-rotation to the neck's forward direction
        Vector3 adjustedForward = counterRotation * neckTransform.forward;
        // Now use the adjusted forward direction to set the plane's rotation
        Quaternion rotationNeckCoronal = Quaternion.LookRotation(adjustedForward);

        //// rotate the plane so its rotation is set to the sagittal or coronal plane
        //Quaternion additionalSagittalNeckRotation = Quaternion.Euler(90, 0, 0);
        //Quaternion additionalCoronalNeckRotation = Quaternion.Euler(90, 0, 0);
        //Quaternion additionalSagittalRightLegRotation = Quaternion.Euler(90, 0, 0);
        //Quaternion additionalCoronalRightLegRotation = Quaternion.Euler(0, 0, 90);
        //Quaternion additionalSagittalTrunkRotation = Quaternion.Euler(90, 0, 0);
        //Quaternion additionalCoronalTrunkRotation = Quaternion.Euler(90, 0, 0);
        //Quaternion additionalLowerarmRotation = Quaternion.Euler(0, 0, 90);
        //Quaternion additionalCoronalUpperarmRotation = Quaternion.Euler(0, 0, 90);
        //Quaternion additionalsagittalUpperarmRotation = Quaternion.Euler(90, 0, 0);
        //Quaternion additionalSagittalRightHandRotation = Quaternion.Euler(90, 0, 0);
        //Quaternion additionalCoronalRightHandRotation = Quaternion.Euler(0, 0, 0);
        //Quaternion additionalTwistRightHandRotation = Quaternion.Euler(0, 0, 0);

        neckSagittalPlaneTransform.rotation = rotationNeckSagittal * additional90degreeXaxisRotation;
        neckCoronalPlaneTransform.rotation = rotationNeckCoronal * additional90degreeXaxisRotation;
        r_legSagittalPlaneTransform.rotation = rotationRightLegSagittal * additional90degreeXaxisRotation;
        r_legCoronalPlaneTransform.rotation = rotationRightLegSagittal * additional90degreeZaxisRotation;        
        l_legSagittalPlaneTransform.rotation = rotationLeftLegSagittal * additional90degreeXaxisRotation;
        l_legCoronalPlaneTransform.rotation = rotationLeftLegSagittal * additional90degreeZaxisRotation;

        trunkSagittalPlaneTransform.rotation = rotationTrunkSagittal * additional90degreeXaxisRotation;
        trunkCoronalPlaneTransform.rotation = rotationTrunkCoronal * additional90degreeXaxisRotation;
        //r_lowerArmSagittalPlaneTransform.rotation = rotation;
        r_lowerArmSagittalPlaneTransform.rotation = rotationRightLowerarmSagittal * additional90degreeZaxisRotation;
        r_upperArmCoronalPlaneTransform.rotation = rotationRightUpperarmCoronal * additional90degreeZaxisRotation;
        r_upperArmSagittalPlaneTransform.rotation = rotationRightUpperarmCoronal * additional90degreeXaxisRotation;
        r_handSagittalPlaneTransform.rotation = rotationRightHandSagittal * additional90degreeXaxisRotation;
        r_handArmCoronalPlaneTransform.rotation = rotationRightHandCoronal;
        //r_handArmTwistPlaneTransform.rotation = rotationRightHandTwist;
    }
    void Update()
    {
        UpdatePlanePosition();
        UpdatePlaneRotation();

        //// Get the up vector of the hand
        //Vector3 handUp = r_handTransform.up;
        //// Create a forward vector in the xz plane
        //Vector3 planeForwardHand = r_handArmTwistPlaneTransform.forward;
        //planeForwardHand.y = 0;
        //// Normalize the forward vector
        //planeForwardHand.Normalize();
        //// Create the rotation for the plane
        //Quaternion planeRotation = Quaternion.LookRotation(planeForwardHand, handUp);
        //// Set the rotation of the plane
        //r_handArmTwistPlaneTransform.rotation = planeRotation;

        //WINNER???
        //// Get the rotation of the hand in Euler angles
        //Vector3 handEulerAngles = r_handTransform.rotation.eulerAngles;

        //// Create a new rotation that only includes the y-axis rotation of the hand
        //Quaternion yRotation = Quaternion.Euler(0, handEulerAngles.y, 0);

        //// Get the rotation of a reference object (e.g., the parent of the hand) in Euler angles
        //Vector3 referenceEulerAngles = r_upperarmTransform.rotation.eulerAngles;

        //// Create a new rotation that only includes the x and z-axis rotations of the reference object
        //Quaternion xzRotation = Quaternion.Euler(referenceEulerAngles.x, 0, referenceEulerAngles.z);
        //// Combine the rotations
        //Quaternion planeRotation = yRotation * xzRotation;
        //// Set the rotation of the plane
        //r_handArmTwistPlaneTransform.rotation = planeRotation;

        // Get the forward vector of the hand
        Vector3 handForward = r_handTransform.forward;

        // Set the y component of the forward vector to zero
        handForward.y = 0;

        // Normalize the forward vector
        handForward.Normalize();

        // Create a rotation from the world's forward vector to the hand's forward vector
        Quaternion toHandForward = Quaternion.FromToRotation(Vector3.forward, handForward);

        // Apply this rotation to the world's up vector to get the plane's up vector
        Vector3 planeUpHand = toHandForward * Vector3.up;

        // Create the rotation for the plane
        Quaternion planeRotation = Quaternion.LookRotation(handForward, planeUpHand);

        // Set the rotation of the plane
        //r_handArmTwistPlaneTransform.rotation = planeRotation;

        //float yRotation = Vector3.Angle(r_forearmTransform.up, Vector3.up);
        //Quaternion rotationRightHandTwistAssist = Quaternion.AngleAxis(yRotation, Vector3.up);
        //twist_assistiveRightHandObjectTransform.rotation = rotationRightHandTwistAssist;

        // The plane's rotation is set such that it won't roll around its forward direction (the forearm won't roll), and its right direction is fixed along the upper arm

        Vector2 upReferenceDirection = Vector2.up; // This represents the direction of the forearm at the start
        Vector2 rightArmReferenceDirection = Vector2.right; // This represents the direction of the forearm at the start

        //Vector2 upperArmCurrentPosition = GetPointOnPlane(r_upperarmTransform, r_lowerArmSagittalPlane);
        //Vector2 lowerArmCurrentPosition = GetPointOnPlane(r_forearmTransform, r_lowerArmSagittalPlane);
        Vector2 r_upperarmCurrentPositionSagittal = GetPointOnPlane(r_lowerarm.transform, r_upperArmSagittalPlane);
        Vector2 r_upperarmCurrentPositionCoronal = GetPointOnPlane(r_lowerarmTransform, r_upperArmCoronalPlane);
        Vector2 r_lowerarmCurrentPositionSagittal = GetPointOnPlane(r_handTransform, r_lowerArmSagittalPlane);
        Vector2 r_handCurrentPositionSagittal = GetPointOnPlane(r_middleFingerTransform, r_handSagittalPlane);
        Vector2 r_handCurrentPositionCoronal = GetPointOnPlane(r_middleFingerTransform, r_handArmCoronalPlane);

        Vector2 neckCurrentPositionSagittal = GetPointOnPlane(headTransform, neckSagittalPlane);
        Vector2 neckCurrentPositionCoronal = GetPointOnPlane(headTransform, neckCoronalPlane);
        Vector2 trunkCurrentPositionSagittal = GetPointOnPlane(trunkSpine2Transform, trunkSagittalPlane);
        Vector2 trunkCurrentPositionCoronal = GetPointOnPlane(trunkSpine2Transform, trunkCoronalPlane);
        Vector2 r_legCurrentPositionCoronal = GetPointOnPlane(r_footTransform, r_legCoronalPlane);
        Vector2 r_legCurrentPositionSagittal = GetPointOnPlane(r_footTransform, r_legSagittalPlane);

        Vector2 l_legCurrentPositionSagittal = GetPointOnPlane(l_footTransform, l_legSagittalPlane);
        Vector2 l_legCurrentPositionCoronal = GetPointOnPlane(l_footTransform, l_legCoronalPlane);

        // Calculate angles
        //float upperArmAngle = Vector2.SignedAngle(upperArmCurrentPosition, upperArmCurrentPosition);

        // For TABLE B
        int upperArmAngleFlexing = ((int)Vector2.SignedAngle(upReferenceDirection, r_upperarmCurrentPositionSagittal));
        int upperArmAngleAdduction = ((int)Vector2.SignedAngle(-rightArmReferenceDirection, r_upperarmCurrentPositionCoronal));
        int lowerArmAngleFlexing = ((int)Vector2.SignedAngle(-upReferenceDirection, r_lowerarmCurrentPositionSagittal));

        int r_handAngleFlexing = ((int)Vector2.SignedAngle(rightArmReferenceDirection, r_handCurrentPositionSagittal));
        int r_handAngleAdduction = ((int)Vector2.SignedAngle(upReferenceDirection, r_handCurrentPositionCoronal));

        int neckAngleFlexing = ((int)Vector2.SignedAngle(-upReferenceDirection, neckCurrentPositionSagittal));
        int neckAngleAdduction = ((int)Vector2.SignedAngle(-upReferenceDirection, neckCurrentPositionCoronal));

        // For TABLE A
        int r_legAngleAdduction = ((int)Vector2.SignedAngle(upReferenceDirection, r_legCurrentPositionCoronal));
        int r_legAngleFlexing = ((int)Vector2.SignedAngle(-rightArmReferenceDirection, r_legCurrentPositionSagittal));

        int l_legAngleFlexing = ((int)Vector2.SignedAngle(upReferenceDirection, l_legCurrentPositionSagittal));
        int l_legAngleAdduction = ((int)Vector2.SignedAngle(-rightArmReferenceDirection, l_legCurrentPositionCoronal));

        int trunkAngleFlexing = ((int)Vector2.SignedAngle(-upReferenceDirection, trunkCurrentPositionSagittal));
        int trunkAngleAdduction = ((int)Vector2.SignedAngle(-upReferenceDirection, trunkCurrentPositionCoronal));

        //// Draw the upReferenceDirection vector
        //Vector3 upReferenceDirection3D = new Vector3(0, upReferenceDirection.y, upReferenceDirection.x);
        //Debug.DrawLine(r_lowerArmSagittalPlaneTransform.position, r_lowerArmSagittalPlaneTransform.position + upReferenceDirection3D, Color.green);

        //// Draw the r_handCurrentPositionTwist vector
        //Vector3 r_handCurrentPositionTwist3D = new Vector3(0, r_lowerarmCurrentPositionSagittal.y, r_lowerarmCurrentPositionSagittal.x);
        //Debug.DrawLine(r_lowerArmSagittalPlaneTransform.position, r_lowerArmSagittalPlaneTransform.position + r_handCurrentPositionTwist3D, Color.magenta);


        //Debug.DrawLine(r_handArmTwistPlane.transform.position, r_handArmTwistPlane.transform.position + upReferenceDirection3D, Color.green);

        // Draw the ray
        //Debug.DrawRay(rayStart, rayEnd - rayStart, Color.yellow);
        // Get the right vectors in the local space of the hand
        Vector3 r_handRightLocalVector = r_handTransform.right;
        Vector3 r_handHorizontalPlaneLocalVector = r_wristHorizontalPlaneTransform.right;
        Vector3 l_handRightLocalVector = l_handTransform.right;
        Vector3 l_handHorizontalPlaneLocalVector = l_wristHorizontalPlaneTransform.right;


        Vector3 neckRightLocalVector = neckTransform.right;
        Vector3 neckHorizontalPlaneLocalVector = neckHorizontalPlaneTransform.right;

        Vector3 trunkRightLocalVector = trunkSpine4Transform.right;
        Vector3 trunkHorizontalPlaneLocalVector = trunkHorizontalPlaneTransform.right;

        // Calculate the twist angle
        int trunkTwistAngle = ((int)Vector3.Angle(trunkRightLocalVector, trunkHorizontalPlaneLocalVector));
        int r_handTwistAngle = ((int)Vector3.Angle(r_handRightLocalVector, r_handHorizontalPlaneLocalVector));
        int l_handTwistAngle = ((int)Vector3.Angle(l_handRightLocalVector, l_handHorizontalPlaneLocalVector));
        int neckTwistAngle = ((int)Vector3.Angle(neckRightLocalVector, neckHorizontalPlaneLocalVector));

        // Log the angle
        Debug.Log("r_hand Twist Angle: " + r_handTwistAngle);
        Debug.Log("l_hand Twist Angle: " + l_handTwistAngle);
        Debug.Log("neck Twist Angle: " + neckTwistAngle);
        Debug.Log("trunk Twist Angle: " + trunkTwistAngle);

        //Debug.Log("Twist Angle GPT: " + twistAngleTest);
        //Debug.Log("Twist Angle GPT handUp: " + handUp);
        //Debug.Log("Twist Angle GPT planeForward: " + planeForwardHand);

        //// Calculate the start and end points of the ray in the world space
        //Vector3 rayStart = r_handArmTwistPlaneTransform.position;
        //Vector3 rayEnd = rayStart + r_handArmTwistPlane.transform.TransformDirection(handRightLocal);

        //Vector3 upReferenceDirection3D = new Vector3(0, upReferenceDirection.y, upReferenceDirection.x);
        //Debug.DrawLine(r_handArmTwistPlane.transform.position, r_handArmTwistPlane.transform.position + upReferenceDirection3D, Color.green);


        //Debug.DrawRay(rayBegin, rayEnding - rayBegin, Color.black);        //// Draw the handCurrentPositionSagittal
        //Vector3 handPosition3D = new Vector3(r_lowerarmCurrentPositionSagittal.x, 0, r_lowerarmCurrentPositionSagittal.y);
        //Debug.DrawRay(r_lowerArmSagittalPlane.transform.position, handPosition3D, Color.blue);

        //Vector3 handPosition3DCoronal = new Vector3(r_upperarmCurrentPositionCoronal.x, 0, r_upperarmCurrentPositionCoronal.y);
        //Debug.DrawRay(r_upperArmCoronalPlane.transform.position, handPosition3DCoronal, Color.green);

        Debug.Log("Neck angle flex: " + neckAngleFlexing);
        Debug.Log("Neck angle add: " + neckAngleAdduction);

        Debug.Log("r_leg angle flex: " + r_legAngleFlexing);
        Debug.Log("r_leg angle add: " + r_legAngleAdduction);
        Debug.Log("l_leg angle flex: " + l_legAngleFlexing);
        Debug.Log("l_leg angle add: " + l_legAngleAdduction);

        Debug.Log("Trunk flex: " + trunkAngleFlexing);
        Debug.Log("Trunk add: " + trunkAngleAdduction);

        Debug.Log("Upper arm angle Flex: " + upperArmAngleFlexing);
        Debug.Log("Upper arm angle adduction " + upperArmAngleAdduction);

        //Debug.Log("Upper arm angle: " + (upperArmAngle - tempUpperArmAngle));
        if (lowerArmAngleFlexing < 0 && tempLowerArmAngle < 0)
            Debug.Log("Lower arm angle " + lowerArmAngleFlexing + " with temp " + tempLowerArmAngle + ": " + (lowerArmAngleFlexing + (-1 * tempLowerArmAngle)));
        else if (lowerArmAngleFlexing > 0 && tempLowerArmAngle < 0)
            Debug.Log("Lower arm angle " + lowerArmAngleFlexing + " with temp " + tempLowerArmAngle + ": " + (lowerArmAngleFlexing + (-1 * tempLowerArmAngle)));
        else if (lowerArmAngleFlexing > 0 && tempLowerArmAngle > 0)
            Debug.Log("Lower arm angle " + lowerArmAngleFlexing + " with temp " + tempLowerArmAngle + ": " + (lowerArmAngleFlexing - (tempLowerArmAngle)));
        else if (lowerArmAngleFlexing < 0 && tempLowerArmAngle > 0)
            Debug.Log("Lower arm angle " + lowerArmAngleFlexing + " with temp " + tempLowerArmAngle + ": " + (lowerArmAngleFlexing - (tempLowerArmAngle)));
        else
            Debug.Log("Lower arm angle " + lowerArmAngleFlexing);
        //Debug.Log("Lower arm angle " + lowerArmAngleFlexing + " with temp " + tempLowerArmAngle);

        if (upperArmAngleAdduction < 0)
            Debug.Log("Upper arm angle adduction " + upperArmAngleAdduction + " with temp "+ tempUpperArmAngleCoronal + ": " + (upperArmAngleAdduction + tempUpperArmAngleCoronal));
        else
            Debug.Log("Upper arm angle adduction " + upperArmAngleAdduction + " with temp " + tempUpperArmAngleCoronal + ": " +  (upperArmAngleAdduction - tempUpperArmAngleCoronal));
        
        Debug.Log("Wrist flex: " + r_handAngleFlexing);
        Debug.Log("Wrist add: " + r_handAngleAdduction);

        // Reset the lower arm angle when space is pressed	
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempUpperArmAngleCoronal = upperArmAngleAdduction;
            tempLowerArmAngle = lowerArmAngleFlexing;

            // Get the current directions of the forearm and upper arm in the plane's local space
            upReferenceDirection = GetPointOnPlane(r_lowerarmTransform, r_lowerArmSagittalPlane);
            rightArmReferenceDirection = GetPointOnPlane(r_upperarmTransform, r_upperArmCoronalPlane);

            //if (tempLowerArmAngle < 0) // Die initialen Werte können entweder posivitv oder negativ sein. Wir setzen auf positiv damit die If Bedingungen einfacher sind
            //    tempLowerArmAngle *= -1;

            if (tempUpperArmAngleCoronal < 0)
                tempUpperArmAngleCoronal *= -1;
        }
        // Reset the lower arm angle when space is pressed	
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            tempLowerArmAngle = 0;
            tempUpperArmAngleCoronal = 0;
        }

        //// Ergebnisse der Berechnung von Step 1 bestimmen Score von Tabelle A. int [Z-Achse (Stockwerk), X-Achse, Y-Achse] 
        //// Bei Arrays zählen wir logisch deshalb immer mit -1 aufrufen.
        //tableAScore = REBA_TableABC_object.tableA[step1_getNeckScore() - 1, step1_getTrunkScore() - 1, step1_getLegScore() - 1];
        //Debug.Log("Table A: " + tableAScore);

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

        // Convert this point to the plane's local space
        Vector3 localPoint = plane.transform.InverseTransformPoint(closestPoint);

        // Use Debug.DrawLine to visualize the shortest line
        // xof Debug.DrawLine(bone.position, closestPoint, Color.yellow);

        // Log local x and z
        Debug.Log("Bone " + bone.name + " Local x: " + localPoint.x.ToString("F2") + " Local z: " + localPoint.z.ToString("F2"));

        // Return 2D coordinates on the plane
        return new Vector2(localPoint.x, localPoint.z);
    }
}

