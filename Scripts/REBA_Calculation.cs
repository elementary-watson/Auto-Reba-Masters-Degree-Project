using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class REBA_Calculation : MonoBehaviour
{
    [Header("Constant Class Valus")]
    const int const_toleranceValue = 15;

    [Header("Avatar Gameobjects")]
    public GameObject r_shoulder;
    public GameObject r_upperarm;
    public GameObject r_forearm;
    public GameObject r_hand;
    public Transform r_handTransform;
    public GameObject r_middleFinger;

    public GameObject l_shoulder;
    public GameObject l_upperarm;
    public GameObject l_forearm;
    public GameObject l_hand;
    public GameObject l_middleFinger;

    public GameObject hip;
    public GameObject spine1;
    public GameObject spine2;
    public GameObject spine3;
    public GameObject spine4;
    public GameObject neck;
    public GameObject head;

    public GameObject r_thigh;
    public GameObject r_calf;
    public GameObject r_foot;

    public GameObject l_thigh;
    public GameObject l_calf;
    public GameObject l_foot;

    //public GameObject twist_assistiveRightHandObject;
    [Header("Plane Gameobjects")]

    public GameObject neckSagittalPlane;
    public GameObject neckCoronalPlane;
    public GameObject neckHorizontalPlane;

    public GameObject trunkSagittalPlane;
    public GameObject trunkCoronalPlane;
    public GameObject trunkHorizontalPlane;

    public GameObject r_legSagittalPlane;
    public GameObject r_legCoronalPlane;

    public GameObject l_legSagittalPlane;
    public GameObject l_legCoronalPlane;

    public GameObject r_upperArmSagittalPlane;
    public GameObject r_upperArmCoronalPlane;
    public GameObject l_upperArmSagittalPlane;
    public GameObject l_upperArmCoronalPlane;

    public GameObject r_lowerArmSagittalPlane;
    public GameObject l_lowerArmSagittalPlane;

    public GameObject r_handSagittalPlane;
    public GameObject r_handArmCoronalPlane;
    public GameObject r_handTwistPlane;

    public GameObject l_handSagittalPlane;
    public GameObject l_handArmCoronalPlane;
    public GameObject l_handTwistPlane;

    private Renderer[] neckSagittalPlaneRenderers;
    private Renderer[] neckCoronalPlaneRenderers;
    private Renderer[] neckTwistPlaneRenderers;

    private Renderer[] trunkSagittalPlaneRenderers;
    private Renderer[] trunkCoronalPlaneRenderers;
    private Renderer[] trunkTwistPlaneRenderers;

    private Renderer[] r_legSagittalPlaneRenderers;
    private Renderer[] r_legCoronalPlaneRenderers;
    private Renderer[] l_legSagittalPlaneRenderers;
    private Renderer[] l_legCoronalPlaneRenderers;

    private Renderer[] r_upperArmSagittalPlaneRenderers;
    private Renderer[] r_upperArmCoronalPlaneRenderers;
    private Renderer[] l_upperArmSagittalPlaneRenderers;
    private Renderer[] l_upperArmCoronalPlaneRenderers;

    private Renderer[] r_lowerArmSagittalPlaneRenderers;
    private Renderer[] l_lowerArmSagittalPlaneRenderers;

    private Renderer[] r_handSagittalPlaneRenderers;
    private Renderer[] r_handArmCoronalPlaneRenderers;
    private Renderer[] r_handTwistPlaneRenderers;

    private Renderer[] l_handSagittalPlaneRenderers;
    private Renderer[] l_handArmCoronalPlaneRenderers;
    private Renderer[] l_handTwistPlaneRenderers;


    [Header("Initial Stance Angles")]
    float tempUpperArmAngle;

    // table A
    int initialFlex_NeckAngle;
    int initialAdd_NeckAngle;
    int initialTwist_NeckAngle;

    int initialFlex_TrunkAngle;
    int initialAdd_TrunkAngle;
    int initialTwist_TrunkAngle;

    int r_initialFlex_LegAngle;
    int l_initialFlex_LegAngle;

    // table B
    int r_initialFlex_UpperArm;
    int r_initialAdd_UpperArm;
    int l_initialFlex_UpperArm;
    int l_initialAdd_UpperArm;

    int r_initialFlex_LowerArm;
    int l_initialFlex_LowerArm;

    int r_initialFlex_Hand;
    int r_initialAdd_Hand;
    int r_initialTwist_Hand;
    int l_initialFlex_Hand;
    int l_initialAdd_Hand;
    int l_initialTwist_Hand;

    [Header("Angle variables")]
    public int neckAngleFlexing;
    public int neckAngleAdduction;
    public int neckAngleTwist;

    public int trunkAngleFlexing;
    public int trunkAngleAdduction;
    public int trunkAngleTwist;

    public int r_legAngleFlexing;
    public int r_legAngleAdduction;
    public int l_legAngleFlexing;
    public int l_legAngleAdduction;

    public int r_upperArmAngleFlexing;
    public int r_upperArmAngleAdduction;
    public int r_lowerArmAngleFlexing;
    public int lowerArmAngleFlexingTransform;
    public int r_handAngleFlexing;
    public int r_handAngleAdduction;
    public int r_handAngleTwist;

    public int l_upperArmAngleFlexing;
    public int l_upperArmAngleAdduction;
    public int l_lowerArmAngleFlexing;
    public int l_handAngleFlexing;
    public int l_handAngleAdduction;
    public int l_handAngleTwist;

    [Header("Final Angles")]
    // table A
    int finalFlex_NeckAngle;
    int finalAdd_NeckAngle;
    int finalTwist_NeckAngle;

    int finalFlex_TrunkAngle;
    int finalAdd_TrunkAngle;
    int finalTwist_TrunkAngle;

    int r_finalFlex_LegAngle;
    int l_finalFlex_LegAngle;

    // table B
    int r_finalFlex_UpperArm;
    int r_finalAdd_UpperArm;
    int l_finalFlex_UpperArm;
    int l_finalAdd_UpperArm;

    int r_finalFlex_LowerArm;
    int l_finalFlex_LowerArm;

    int r_finalFlex_Wrist;
    int r_finalAdd_Wrist;
    int r_finalTwist_Wrist;
    int l_finalFlex_Wrist;
    int l_finalAdd_Wrist;
    int l_finalTwist_Wrist;

    [Header("Posture Scores")]    
    [SerializeField] private int tableA_PostureScore;
    [SerializeField] private int tableB_PostureScore;
    [SerializeField] private int tableC_PostureScore;
    [SerializeField] private int forceLoadScore;
    [SerializeField] public int couplingScore;
    [SerializeField] private int activityScore;

    public bool isRightFootTouching;
    public bool isLeftFootTouching;

    public bool isRightShoulderTouching;
    public bool isLeftShoulderTouching;

    public bool isRapidlyChanging;
    public int currentLoadInLBS;

    [Header("Tolerances")]
    public int flexTolerance;
    public int adductionTolerance;
    public int twistTolerance;

    [Header("Functionality")]
    public bool isKeyInputEnabled = true;
    public TextMeshProUGUI toggleButtonText;
    public List<TextMeshProUGUI> angleTextsTable_A;
    public List<TextMeshProUGUI> angleTextsTable_B;

    public List<TextMeshProUGUI> scoreTextsTable_A;
    public List<TextMeshProUGUI> scoreTextsTable_B;

    public List<TextMeshProUGUI> finalScoreTexts_A;
    public List<TextMeshProUGUI> finalScoreTexts_B;
    public List<TextMeshProUGUI> finalScoreTexts_C;

    public List<TextMeshProUGUI> AdditionalScores_A;
    public List<TextMeshProUGUI> AdditionalScores_B;
    public List<TextMeshProUGUI> AdditionalScores_C;
    public float offsetDistance = 0.5f;
    private REBA_TableABC REBA_TableABC_object;
    public REBA_uiScoreController REBA_uiScoreController_object;
    public REBA_ScoreAnalysis REBA_ScoreAnalysis_object;
    public REBA_ExperimentController experimentController_object;

    void Start()
    {
        Application.targetFrameRate = 120; // Target 90 FPS

        REBA_TableABC_object = new REBA_TableABC();
        flexTolerance = const_toleranceValue;
        adductionTolerance = const_toleranceValue;
        twistTolerance = const_toleranceValue;

        // Set the additional scores to 0
        forceLoadScore = 0;
        couplingScore = 0;
        activityScore = 0;

        // table A
        initialFlex_NeckAngle = 0;
        initialAdd_NeckAngle = 0;
        initialTwist_NeckAngle = 0;

        initialFlex_TrunkAngle = 0;
        initialAdd_TrunkAngle = 0;
        initialTwist_TrunkAngle = 0;

        r_initialFlex_LegAngle = 0;
        l_initialFlex_LegAngle = 0;

        // table B
        r_initialFlex_UpperArm = 0;
        r_initialAdd_UpperArm = 0;
        l_initialFlex_UpperArm = 0;
        l_initialAdd_UpperArm = 0;

        r_initialFlex_LowerArm = 0;
        l_initialFlex_LowerArm = 0;

        r_initialFlex_Hand = 0;
        r_initialAdd_Hand = 0;
        r_initialTwist_Hand = 0;
        l_initialFlex_Hand = 0;
        l_initialAdd_Hand = 0;
        l_initialTwist_Hand = 0;

        // Assign Renderer references
        neckSagittalPlaneRenderers = neckSagittalPlane.GetComponentsInChildren<Renderer>();
        neckCoronalPlaneRenderers = neckCoronalPlane.GetComponentsInChildren<Renderer>();
        neckTwistPlaneRenderers = neckHorizontalPlane.GetComponentsInChildren<Renderer>();

        trunkSagittalPlaneRenderers = trunkSagittalPlane.GetComponentsInChildren<Renderer>();
        trunkCoronalPlaneRenderers = trunkCoronalPlane.GetComponentsInChildren<Renderer>();
        trunkTwistPlaneRenderers = trunkHorizontalPlane.GetComponentsInChildren<Renderer>();

        r_legSagittalPlaneRenderers = r_legSagittalPlane.GetComponentsInChildren<Renderer>();
        r_legCoronalPlaneRenderers = r_legCoronalPlane.GetComponentsInChildren<Renderer>();
        l_legSagittalPlaneRenderers = l_legSagittalPlane.GetComponentsInChildren<Renderer>();
        l_legCoronalPlaneRenderers = l_legCoronalPlane.GetComponentsInChildren<Renderer>();

        r_upperArmSagittalPlaneRenderers = r_upperArmSagittalPlane.GetComponentsInChildren<Renderer>();
        r_upperArmCoronalPlaneRenderers = r_upperArmCoronalPlane.GetComponentsInChildren<Renderer>();
        l_upperArmSagittalPlaneRenderers = l_upperArmSagittalPlane.GetComponentsInChildren<Renderer>();
        l_upperArmCoronalPlaneRenderers = l_upperArmCoronalPlane.GetComponentsInChildren<Renderer>();

        r_lowerArmSagittalPlaneRenderers = r_lowerArmSagittalPlane.GetComponentsInChildren<Renderer>();
        l_lowerArmSagittalPlaneRenderers = l_lowerArmSagittalPlane.GetComponentsInChildren<Renderer>();
        
        r_handSagittalPlaneRenderers = r_handSagittalPlane.GetComponentsInChildren<Renderer>();
        r_handArmCoronalPlaneRenderers = r_handArmCoronalPlane.GetComponentsInChildren<Renderer>();
        r_handTwistPlaneRenderers = r_handTwistPlane.GetComponentsInChildren<Renderer>();
        
        l_handSagittalPlaneRenderers = l_handSagittalPlane.GetComponentsInChildren<Renderer>();
        l_handArmCoronalPlaneRenderers = l_handArmCoronalPlane.GetComponentsInChildren<Renderer>();
        l_handTwistPlaneRenderers = l_handTwistPlane.GetComponentsInChildren<Renderer>();

        // Turn off all renderers at start
        ToggleRenderers(neckSagittalPlaneRenderers, false);
        ToggleRenderers(neckCoronalPlaneRenderers, false);
        ToggleRenderers(neckTwistPlaneRenderers, false);

        ToggleRenderers(trunkSagittalPlaneRenderers, false);
        ToggleRenderers(trunkCoronalPlaneRenderers, false);
        ToggleRenderers(trunkTwistPlaneRenderers, false);

        ToggleRenderers(r_legSagittalPlaneRenderers, false);
        ToggleRenderers(r_legCoronalPlaneRenderers, false);
        ToggleRenderers(l_legSagittalPlaneRenderers, false);
        ToggleRenderers(l_legCoronalPlaneRenderers, false);

        ToggleRenderers(r_upperArmSagittalPlaneRenderers, false);
        ToggleRenderers(r_upperArmCoronalPlaneRenderers, false);
        ToggleRenderers(l_upperArmSagittalPlaneRenderers, false);
        ToggleRenderers(l_upperArmCoronalPlaneRenderers, false);

        ToggleRenderers(r_lowerArmSagittalPlaneRenderers, false);
        ToggleRenderers(l_lowerArmSagittalPlaneRenderers, false);
        
        ToggleRenderers(r_handSagittalPlaneRenderers, false);
        ToggleRenderers(r_handArmCoronalPlaneRenderers, false);
        ToggleRenderers(r_handTwistPlaneRenderers, false);        
        ToggleRenderers(l_handSagittalPlaneRenderers, false);
        ToggleRenderers(l_handArmCoronalPlaneRenderers, false);
        ToggleRenderers(l_handTwistPlaneRenderers, false);
    }

    void Update()
    {
        // Set the plane's position to the midpoint of the upper arm and forearm
        // Damit signedangle die winkel sauber rauszieht muss die plane an das koeperteil angebracht werden dass wir untersuchen

        Vector3 midpointNeck = (neck.transform.position + head.transform.position) / 2;
        Vector3 midpointTrunk = (spine1.transform.position + spine2.transform.position) / 2;
        Vector3 midpointRightLeg = (r_calf.transform.position + r_foot.transform.position) / 2;
        Vector3 midpointLeftLeg = (l_calf.transform.position + l_foot.transform.position) / 2;


        Vector3 midpointRightUpperarm = (r_upperarm.transform.position + r_forearm.transform.position) / 2;
        //XOF Vector3 midpointRightUpperarm = (r_forearm.transform.position);
        // Die plane wird hier auf den unterarm gesetzt damit der unterarm parallel mit der plane verläuft
        // Die idee ist es den arm als den beweglichen arm des goniometers einzustellen und eine andere achse wird als festes gegenstück eingesetzt.
        // Diese feste Achse wird einfach die durch Vector2.up; festgelegt und mit der signedangle methode verrechnet
        Vector3 midpointRightForearm = (r_forearm.transform.position + r_hand.transform.position) / 2;
        Vector3 midpointRightHand = (r_hand.transform.position + r_middleFinger.transform.position) / 2;

        Vector3 midpointLeftUpperarm = (l_upperarm.transform.position + l_forearm.transform.position) / 2;
        Vector3 midpointLeftForearm = (l_forearm.transform.position + l_hand.transform.position) / 2;
        Vector3 midpointLeftHand = (l_hand.transform.position + l_middleFinger.transform.position) / 2;


        Vector3 neckSagittalOffset = new Vector3(0, 0, 0);
        Vector3 neckCoronalOffset = new Vector3(0, 0, 0);

        Vector3 r_legSagittalOffset = new Vector3(0, 0, 0);
        Vector3 r_legCoronalOffset = new Vector3(0, 0, 0);

        Vector3 trunkSagittalOffset = new Vector3(0, 0, 0);
        Vector3 trunkCoronalOffset = new Vector3(0, 0, 0);

        Vector3 r_ArmSagittalOffset = new Vector3(0, 0f, 0);
        Vector3 r_ArmCoronalOffset = new Vector3(0, 0, 0);
        Vector3 l_ArmSagittalOffset = new Vector3(0, 0, 0);
        Vector3 l_ArmCoronalOffset = new Vector3(0, 0, 0);

        Vector3 r_handSagittalOffset = new Vector3(0, 0, 0);
        Vector3 r_handCoronalOffset = new Vector3(0, 0, 0);
        Vector3 l_handSagittalOffset = new Vector3(0, 0, 0);
        Vector3 l_handCoronalOffset = new Vector3(0, 0, 0);

        // Dieser Code sorgt dafür dass sich die planes nicht um die eigene achse drehen wenn der avatar sich dreht.
        // Die planes kriegen einen pivot point zugewiesen sodass sie ihre relative position beibehalten koennen.        
        neckSagittalOffset = neck.transform.rotation * neckSagittalOffset;
        neckCoronalOffset = neck.transform.rotation * neckCoronalOffset;

        r_legSagittalOffset = r_thigh.transform.rotation * r_legSagittalOffset;
        r_legCoronalOffset = r_thigh.transform.rotation * r_legCoronalOffset;

        trunkSagittalOffset = spine1.transform.rotation * trunkSagittalOffset;
        trunkCoronalOffset = spine1.transform.rotation * trunkCoronalOffset;

        // Rotate the offset vector by the avatar's rotation
        r_ArmSagittalOffset = r_shoulder.transform.rotation * r_ArmSagittalOffset;
        r_ArmCoronalOffset = r_shoulder.transform.rotation * r_ArmCoronalOffset;
        l_ArmSagittalOffset = l_shoulder.transform.rotation * l_ArmSagittalOffset;
        l_ArmCoronalOffset = l_shoulder.transform.rotation * l_ArmCoronalOffset;

        r_handSagittalOffset = r_hand.transform.rotation * r_handSagittalOffset;
        r_handCoronalOffset = r_hand.transform.rotation * r_handCoronalOffset;
        l_handSagittalOffset = l_hand.transform.rotation * l_handSagittalOffset;
        l_handCoronalOffset = l_hand.transform.rotation * l_handCoronalOffset;

        //Die vorangegangenen Rechnungen fuer die aktuelle Position und Rotation werden hier angewandt
        neckSagittalPlane.transform.position = midpointNeck + neckSagittalOffset;
        neckCoronalPlane.transform.position = midpointNeck + neckCoronalOffset;

        trunkSagittalPlane.transform.position = midpointTrunk + trunkSagittalOffset;
        trunkCoronalPlane.transform.position = midpointTrunk + trunkCoronalOffset;

        r_legSagittalPlane.transform.position = midpointRightLeg + r_legSagittalOffset;
        r_legCoronalPlane.transform.position = midpointRightLeg + r_legCoronalOffset;

        l_legSagittalPlane.transform.position = midpointLeftLeg;
        l_legCoronalPlane.transform.position = midpointLeftLeg;

        r_upperArmSagittalPlane.transform.position = midpointRightUpperarm + r_ArmSagittalOffset;
        r_upperArmCoronalPlane.transform.position = midpointRightUpperarm + r_ArmCoronalOffset;
        r_lowerArmSagittalPlane.transform.position = midpointRightForearm + r_ArmSagittalOffset;

        r_handSagittalPlane.transform.position = midpointRightHand + r_handSagittalOffset;
        r_handArmCoronalPlane.transform.position = midpointRightHand + r_handCoronalOffset;

        l_upperArmSagittalPlane.transform.position = midpointLeftUpperarm + l_ArmSagittalOffset;
        l_upperArmCoronalPlane.transform.position = midpointLeftUpperarm + l_ArmCoronalOffset;
        l_lowerArmSagittalPlane.transform.position = midpointLeftForearm + l_ArmSagittalOffset;

        l_handSagittalPlane.transform.position = midpointLeftHand + l_handSagittalOffset;
        l_handArmCoronalPlane.transform.position = midpointLeftHand + l_handCoronalOffset;



        /** Calculation of the plane orientation **/
        Vector3 r_footCalfDirection = (r_calf.transform.position - r_foot.transform.position).normalized;
        Vector3 r_thighCalfDirection = (r_calf.transform.position - r_thigh.transform.position).normalized;
        Vector3 r_legDirection = Vector3.Cross(r_footCalfDirection, r_thighCalfDirection);

        //Debug.DrawRay(r_calf.transform.position, r_footCalfDirection, Color.magenta);
        //Debug.DrawRay(r_calf.transform.position, r_thighCalfDirection, Color.cyan);
        //Debug.DrawRay(r_calf.transform.position, r_legDirection, Color.black);

        Vector3 l_footCalfDirection = (l_calf.transform.position - l_foot.transform.position).normalized;
        Vector3 l_thighCalfDirection = (l_calf.transform.position - l_thigh.transform.position).normalized;
        Vector3 l_legDirection = Vector3.Cross(l_footCalfDirection, l_thighCalfDirection);


        // Calculate the direction of the upper arm and then of the lower arm
        Vector3 r_upperArmDirection = (r_upperarm.transform.position - r_shoulder.transform.position).normalized;
        Vector3 r_lowerArmDirection = (r_forearm.transform.position - r_upperarm.transform.position).normalized;
        // We want the plane's x-axis to be along the upper arm's direction, its y-axis to be perpendicular to the arm, and its z-axis to be the cross product of these two directions
        Vector3 r_planeRight = r_upperArmDirection; // planeRight
        Vector3 r_planeUp = Vector3.Cross(r_lowerArmDirection, r_upperArmDirection); // planeUp // This makes the plane's y-axis always perpendicular to the arm
        Vector3 r_planeForward = Vector3.Cross(r_planeUp, r_planeRight); // planeForward

        // Calculate the direction of the upper arm and then of the lower arm
        Vector3 l_upperArmDirection = (l_upperarm.transform.position - l_shoulder.transform.position).normalized;
        Vector3 l_lowerArmDirection = (l_forearm.transform.position - l_upperarm.transform.position).normalized;
        // We want the plane's x-axis to be along the upper arm's direction, its y-axis to be perpendicular to the arm, and its z-axis to be the cross product of these two directions
        Vector3 l_planeRight = l_upperArmDirection; // planeRight
        Vector3 l_planeUp = Vector3.Cross(l_lowerArmDirection, l_upperArmDirection); // planeUp // This makes the plane's y-axis always perpendicular to the arm
        Vector3 l_planeForward = Vector3.Cross(l_planeUp, l_planeRight); // planeForward

        Vector3 r_handForearmDirection = (r_forearm.transform.position - r_hand.transform.position).normalized;
        Vector3 r_upperarmForearmDirection = (r_forearm.transform.position - r_upperarm.transform.position).normalized;
        Vector3 r_planeHandTwist = Vector3.Cross(r_handForearmDirection, r_upperarmForearmDirection);

        Vector3 l_handForearmDirection = (l_forearm.transform.position - l_hand.transform.position).normalized;
        Vector3 l_upperarmForearmDirection = (l_forearm.transform.position - l_upperarm.transform.position).normalized;
        Vector3 l_planeHandTwist = Vector3.Cross(l_handForearmDirection, l_upperarmForearmDirection);

        // Draw the vectors
        //Debug.DrawRay(r_shoulder.transform.position, r_planeUp, Color.red);
        //Debug.DrawRay(r_shoulder.transform.position, r_planeRight, Color.green);
        //Debug.DrawRay(r_shoulder.transform.position, r_planeForward, Color.blue);
        //Debug.DrawRay(midpoint, planeRight, Color.cyan);
        //Debug.DrawRay(midpoint, lowerArmDirection, Color.red);
        //Debug.DrawRay(r_thigh.transform.position, calfDirection, Color.magenta);
        //Debug.DrawRay(r_foot.transform.position, footDirection, Color.cyan);
        //Debug.DrawRay(r_calf.transform.position, legDirection, Color.black);
        //Debug.DrawRay(midpoint, planeForward, Color.magenta);
        //Debug.DrawRay(midpoint, planeForwardCoronal, Color.white);
        //Debug.DrawRay(midpoint, planeUpCoronal, Color.green);

        // Construct the rotation of the plane using these vectors
        Quaternion rotationNeckSagittal = Quaternion.LookRotation(spine4.transform.right, spine4.transform.up);

        Quaternion rotationTrunkSagittal = Quaternion.LookRotation(hip.transform.right, Vector3.up);
        Quaternion rotationTrunkCoronal = Quaternion.LookRotation(spine1.transform.forward);

        Quaternion r_rotationRightLegSagittal = Quaternion.LookRotation(r_calf.transform.right);
        Quaternion r_rotationRightLegCoronal = Quaternion.LookRotation(r_legDirection, r_thigh.transform.up);
        Quaternion l_rotationRightLegSagittal = Quaternion.LookRotation(l_calf.transform.right);
        Quaternion l_rotationRightLegCoronal = Quaternion.LookRotation(l_legDirection, l_thigh.transform.up);

        Quaternion r_rotationLowerarmSagittal = Quaternion.LookRotation(r_planeForward, r_planeUp);//blue and red
        Quaternion r_rotationUpperarmSagittal = Quaternion.LookRotation(r_upperArmDirection, -r_shoulder.transform.forward); //green
        Quaternion r_rotationUpperarmCoronal = Quaternion.LookRotation(r_upperArmDirection, r_planeForward); //green
        Quaternion l_rotationLowerarmSagittal = Quaternion.LookRotation(l_planeForward, l_planeUp);
        Quaternion l_rotationUpperarmSagittal = Quaternion.LookRotation(l_upperArmDirection, l_shoulder.transform.forward);
        Quaternion l_rotationUpperarmCoronal = Quaternion.LookRotation(l_upperArmDirection, l_planeForward);

        Quaternion r_rotationRightHandBoth = Quaternion.LookRotation(r_planeHandTwist, r_forearm.transform.up);
        Quaternion l_rotationRightHandBoth = Quaternion.LookRotation(l_planeHandTwist, l_forearm.transform.up);

        Vector3 orthogonalUpwards = Vector3.Cross(r_forearm.transform.up, r_forearm.transform.right);
        Quaternion rotationRightHandTwistAss = Quaternion.LookRotation(r_forearm.transform.up, r_forearm.transform.right);

        // Calculate the angle by which the neck is tilted forward
        float tiltAngle = Vector3.Angle(neck.transform.forward, Vector3.up) - 90;
        // Create a counter-rotation Quaternion
        Quaternion counterRotation = Quaternion.AngleAxis(-tiltAngle, neck.transform.right);
        // Apply the counter-rotation to the neck's forward direction
        Vector3 adjustedForward = counterRotation * neck.transform.forward;
        // Now use the adjusted forward direction to set the plane's rotation
        Quaternion rotationNeckCoronal = Quaternion.LookRotation(adjustedForward);


        // rotate the plane so its rotation is set to the sagittal or coronal plane
        Quaternion additionalSagittalNeckRotation = Quaternion.Euler(90, 0, 0);
        Quaternion additionalCoronalNeckRotation = Quaternion.Euler(0, 0, 90);
        Quaternion additionalSagittalRightLegRotation = Quaternion.Euler(90, 0, 0);
        Quaternion additionalCoronalRightLegRotation = Quaternion.Euler(0, 0, 90);
        Quaternion additionalSagittalTrunkRotation = Quaternion.Euler(90, 0, 0);
        Quaternion additionalCoronalTrunkRotation = Quaternion.Euler(90, 0, 0);
        Quaternion additionalLowerarmRotation = Quaternion.Euler(0, 0, 90);
        Quaternion additionalCoronalUpperarmRotation = Quaternion.Euler(0, 0, 90);
        Quaternion additionalsagittalUpperarmRotation = Quaternion.Euler(90, 0, 0);
        Quaternion additionalSagittalRightHandRotation = Quaternion.Euler(0, 0, 90);
        Quaternion additionalCoronalRightHandRotation = Quaternion.Euler(90, 0, 0);

        neckSagittalPlane.transform.rotation = rotationNeckSagittal * additionalSagittalNeckRotation;
        neckCoronalPlane.transform.rotation = rotationNeckSagittal * additionalCoronalNeckRotation;

        trunkSagittalPlane.transform.rotation = rotationTrunkSagittal * additionalSagittalTrunkRotation;
        trunkCoronalPlane.transform.rotation = rotationTrunkCoronal * additionalCoronalTrunkRotation;

        r_legSagittalPlane.transform.rotation = r_rotationRightLegSagittal * additionalSagittalRightLegRotation;
        r_legCoronalPlane.transform.rotation = r_rotationRightLegCoronal * additionalCoronalRightLegRotation;
        l_legSagittalPlane.transform.rotation = l_rotationRightLegSagittal * additionalSagittalRightLegRotation;
        l_legCoronalPlane.transform.rotation = l_rotationRightLegCoronal * additionalCoronalRightLegRotation;

        r_upperArmSagittalPlane.transform.rotation = r_rotationUpperarmSagittal * additionalsagittalUpperarmRotation;
        r_upperArmCoronalPlane.transform.rotation = r_rotationUpperarmCoronal * additionalCoronalUpperarmRotation;
        r_lowerArmSagittalPlane.transform.rotation = r_rotationLowerarmSagittal * additionalLowerarmRotation;
        r_handSagittalPlane.transform.rotation = r_rotationRightHandBoth * additionalSagittalRightHandRotation;
        r_handArmCoronalPlane.transform.rotation = r_rotationRightHandBoth * additionalCoronalRightHandRotation;

        l_upperArmSagittalPlane.transform.rotation = l_rotationUpperarmSagittal * additionalsagittalUpperarmRotation;
        l_upperArmCoronalPlane.transform.rotation = l_rotationUpperarmCoronal * additionalCoronalUpperarmRotation;
        l_lowerArmSagittalPlane.transform.rotation = l_rotationLowerarmSagittal * additionalLowerarmRotation;
        l_handSagittalPlane.transform.rotation = l_rotationRightHandBoth * additionalSagittalRightHandRotation;
        l_handArmCoronalPlane.transform.rotation = l_rotationRightHandBoth * additionalCoronalRightHandRotation;


        // The plane's rotation is set such that it won't roll around its forward direction (the forearm won't roll), and its right direction is fixed along the upper arm

        Vector2 upReferenceDirection = Vector2.up; // This represents the direction of the forearm at the start
        Vector2 rightArmReferenceDirection = Vector2.right; // This represents the direction of the forearm at the start

        // Table A
        Vector3 neckRightLocalVector = neck.transform.right;
        Vector3 neckHorizontalPlaneLocalVector = neckHorizontalPlane.transform.right;
        Vector2 neckCurrentPositionSagittal = GetPointOnPlane(head.transform, neckSagittalPlane);
        Vector2 neckCurrentPositionCoronal = GetPointOnPlane(head.transform, neckCoronalPlane);

        Vector3 trunkRightLocalVector = spine4.transform.right;
        Vector3 trunkHorizontalPlaneLocalVector = trunkHorizontalPlane.transform.right;
        Vector2 trunkCurrentPositionSagittal = GetPointOnPlane(spine4.transform, trunkSagittalPlane);
        Vector2 trunkCurrentPositionCoronal = GetPointOnPlane(spine2.transform, trunkCoronalPlane);

        Vector2 r_legCurrentPositionSagittal = GetPointOnPlane(r_foot.transform, r_legSagittalPlane);
        Vector2 r_legCurrentPositionCoronal = GetPointOnPlane(r_foot.transform, r_legCoronalPlane);
        Vector2 l_legCurrentPositionSagittal = GetPointOnPlane(l_foot.transform, l_legSagittalPlane);
        Vector2 l_legCurrentPositionCoronal = GetPointOnPlane(l_foot.transform, l_legCoronalPlane);

        // Table B
        Vector2 r_upperarmCurrentPositionSagittal = GetPointOnPlane(r_forearm.transform, r_upperArmSagittalPlane);
        Vector2 r_upperarmCurrentPositionCoronal = GetPointOnPlane(r_forearm.transform, r_upperArmCoronalPlane);
        Vector2 r_lowerarmCurrentPositionSagittal = GetPointOnPlane(r_hand.transform, r_lowerArmSagittalPlane);
        Vector2 r_lowerarmCurrentPositionSagittalTransform = GetPointOnPlane(r_handTransform, r_lowerArmSagittalPlane);

        Vector2 l_upperarmCurrentPositionSagittal = GetPointOnPlane(l_forearm.transform, l_upperArmSagittalPlane);
        Vector2 l_upperarmCurrentPositionCoronal = GetPointOnPlane(l_forearm.transform, l_upperArmCoronalPlane);
        Vector2 l_lowerarmCurrentPositionSagittal = GetPointOnPlane(l_hand.transform, l_lowerArmSagittalPlane);

        Vector3 r_handRightLocalVector = r_hand.transform.right;
        Vector3 r_handHorizontalPlaneLocalVector = r_handTwistPlane.transform.right;
        Vector2 r_handCurrentPositionSagittal = GetPointOnPlane(r_middleFinger.transform, r_handSagittalPlane);
        Vector2 r_handCurrentPositionCoronal = GetPointOnPlane(r_middleFinger.transform, r_handArmCoronalPlane);

        Vector3 l_handRightLocalVector = l_hand.transform.right;
        Vector3 l_handHorizontalPlaneLocalVector = l_handTwistPlane.transform.right;
        Vector2 l_handCurrentPositionSagittal = GetPointOnPlane(l_middleFinger.transform, l_handSagittalPlane);
        Vector2 l_handCurrentPositionCoronal = GetPointOnPlane(l_middleFinger.transform, l_handArmCoronalPlane);

        // Calculate angles

        neckAngleFlexing = ((int)Vector2.SignedAngle(-upReferenceDirection, neckCurrentPositionSagittal));
        neckAngleAdduction = ((int)Vector2.SignedAngle(rightArmReferenceDirection, neckCurrentPositionCoronal));
        neckAngleTwist = ((int)Vector3.Angle(neckRightLocalVector, neckHorizontalPlaneLocalVector));

        trunkAngleFlexing = ((int)Vector2.SignedAngle(-upReferenceDirection, trunkCurrentPositionSagittal));
        trunkAngleAdduction = ((int)Vector2.SignedAngle(-upReferenceDirection, trunkCurrentPositionCoronal));
        trunkAngleTwist = ((int)Vector3.Angle(trunkRightLocalVector, trunkHorizontalPlaneLocalVector));

        r_legAngleFlexing = ((int)Vector2.SignedAngle(upReferenceDirection, r_legCurrentPositionSagittal));
        r_legAngleAdduction = ((int)Vector2.SignedAngle(rightArmReferenceDirection, r_legCurrentPositionCoronal));
        l_legAngleFlexing = ((int)Vector2.SignedAngle(upReferenceDirection, l_legCurrentPositionSagittal));
        l_legAngleAdduction = ((int)Vector2.SignedAngle(rightArmReferenceDirection, l_legCurrentPositionCoronal));

        //float upperArmAngle = Vector2.SignedAngle(upperArmCurrentPosition, upperArmCurrentPosition);
        r_upperArmAngleFlexing = ((int)Vector2.SignedAngle(upReferenceDirection, r_upperarmCurrentPositionSagittal));
        r_upperArmAngleAdduction = ((int)Vector2.SignedAngle(-rightArmReferenceDirection, r_upperarmCurrentPositionCoronal));
        r_lowerArmAngleFlexing = ((int)Vector2.SignedAngle(-upReferenceDirection, r_lowerarmCurrentPositionSagittal));
        lowerArmAngleFlexingTransform = ((int)Vector2.SignedAngle(-upReferenceDirection, r_lowerarmCurrentPositionSagittalTransform));
        l_upperArmAngleFlexing = ((int)Vector2.SignedAngle(upReferenceDirection, l_upperarmCurrentPositionSagittal));
        l_upperArmAngleAdduction = ((int)Vector2.SignedAngle(-rightArmReferenceDirection, l_upperarmCurrentPositionCoronal));
        l_lowerArmAngleFlexing = ((int)Vector2.SignedAngle(-upReferenceDirection, l_lowerarmCurrentPositionSagittal));


        r_handAngleFlexing = ((int)Vector2.SignedAngle(rightArmReferenceDirection, r_handCurrentPositionSagittal));
        r_handAngleAdduction = ((int)Vector2.SignedAngle(-upReferenceDirection, r_handCurrentPositionCoronal));
        r_handAngleTwist = ((int)Vector3.Angle(r_handRightLocalVector, r_handHorizontalPlaneLocalVector));
        l_handAngleFlexing = ((int)Vector2.SignedAngle(rightArmReferenceDirection, l_handCurrentPositionSagittal));
        l_handAngleAdduction = ((int)Vector2.SignedAngle(-upReferenceDirection, l_handCurrentPositionCoronal));
        l_handAngleTwist = ((int)Vector3.Angle(l_handRightLocalVector, l_handHorizontalPlaneLocalVector));


        // Key shortcuts
        if (isKeyInputEnabled)
        {
            // Reset the lower arm angle when space is pressed	
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetInitialAngles();
            }
            // Reset the lower arm angle when space is pressed	
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                ResetInitialAngles();
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            reconnectToExperimentController();
        }
        Debug.Log("Trunk " + trunkAngleFlexing);

        // table A
        finalFlex_NeckAngle = AdjustAngleByInitial(neckAngleFlexing, initialFlex_NeckAngle);
        finalAdd_NeckAngle = AdjustAngleByInitial(neckAngleAdduction, initialAdd_NeckAngle);
        finalTwist_NeckAngle = AdjustAngleByInitial(neckAngleTwist, initialTwist_NeckAngle);

        finalFlex_TrunkAngle = AdjustAngleByInitial(trunkAngleFlexing, initialFlex_TrunkAngle);
        finalAdd_TrunkAngle = AdjustAngleByInitial(trunkAngleAdduction, initialAdd_TrunkAngle);
        finalTwist_TrunkAngle = AdjustAngleByInitial(trunkAngleTwist, initialTwist_TrunkAngle);

        r_finalFlex_LegAngle = AdjustAngleByInitial(r_legAngleFlexing, r_initialFlex_LegAngle);
        l_finalFlex_LegAngle = AdjustAngleByInitial(l_legAngleFlexing, l_initialFlex_LegAngle);

        // table B
        r_finalFlex_UpperArm = -1 * AdjustAngleByInitial(r_upperArmAngleFlexing, r_initialFlex_UpperArm);
        r_finalAdd_UpperArm = AdjustAngleByInitial(r_upperArmAngleAdduction, r_initialAdd_UpperArm);
        l_finalFlex_UpperArm = AdjustAngleByInitial(l_upperArmAngleFlexing, l_initialFlex_UpperArm);
        l_finalAdd_UpperArm = AdjustAngleByInitial(l_upperArmAngleAdduction, l_initialAdd_UpperArm);

        r_finalFlex_LowerArm = -1 * AdjustAngleByInitial(r_lowerArmAngleFlexing, r_initialFlex_LowerArm);
        l_finalFlex_LowerArm = AdjustAngleByInitial(l_lowerArmAngleFlexing, l_initialFlex_LowerArm);

        r_finalFlex_Wrist = -1 * AdjustAngleByInitial(r_handAngleFlexing, r_initialFlex_Hand);
        r_finalAdd_Wrist = AdjustAngleByInitial(r_handAngleAdduction, r_initialAdd_Hand);
        r_finalTwist_Wrist = AdjustAngleByInitial(r_handAngleTwist, r_initialTwist_Hand);
        l_finalFlex_Wrist = AdjustAngleByInitial(l_handAngleFlexing, l_initialFlex_Hand);
        l_finalAdd_Wrist = AdjustAngleByInitial(l_handAngleAdduction, l_initialAdd_Hand);
        l_finalTwist_Wrist = AdjustAngleByInitial(l_handAngleTwist, l_initialTwist_Hand);

        try 
        {
            // table a
            angleTextsTable_A[0].text = finalFlex_NeckAngle + " °";
            angleTextsTable_A[1].text = finalAdd_NeckAngle + " °"; //"Neck Add Angle: " + 
            angleTextsTable_A[2].text = finalTwist_NeckAngle + " °";  // "Neck Twist Angle: " +
            
            angleTextsTable_A[3].text = finalFlex_TrunkAngle + " °"; // "Trunk Flex Angle: " + 
            angleTextsTable_A[4].text = finalAdd_TrunkAngle + " °"; //"Trunk Add Angle: " + 
            angleTextsTable_A[5].text = finalTwist_TrunkAngle + " °"; //"Trunk Twist Angle: " + 

            angleTextsTable_A[6].text = l_finalFlex_LegAngle + " °"; //"Left Leg Flex Angle: " + 
            angleTextsTable_A[7].text = r_finalFlex_LegAngle + " °"; //"Right Leg Flex Angle: " + 

            // table b
            angleTextsTable_B[0].text = l_finalFlex_UpperArm + " °"; //"Left UppArm Flex Angle: " + 
            angleTextsTable_B[1].text = l_finalAdd_UpperArm+ " °"; //"Left UppArm Add Angle: " +

            angleTextsTable_B[2].text = r_finalFlex_UpperArm + " °"; //"Left lowArm Flex Angle: " +
            angleTextsTable_B[3].text = r_finalAdd_UpperArm+ " °"; //"Left Wrist Flex Angle: " + 

            angleTextsTable_B[4].text = l_finalFlex_LowerArm  + " °";
            angleTextsTable_B[5].text = r_finalFlex_LowerArm + " °";

            angleTextsTable_B[6].text = l_finalFlex_Wrist + " °";
            angleTextsTable_B[7].text = l_finalAdd_Wrist  + " °";
            angleTextsTable_B[8].text = l_finalTwist_Wrist + " °";

            angleTextsTable_B[9].text = r_finalFlex_Wrist + " °";
            angleTextsTable_B[10].text = r_finalAdd_Wrist + " °";
            angleTextsTable_B[11].text = r_finalTwist_Wrist + " °";


            // table a
            int neckScore = REBA_TableABC_object.Step1_getNeckScore(flexTolerance, finalFlex_NeckAngle);
            int neckScore1a = REBA_TableABC_object.Step1a_sidebend_getNeckScore(adductionTolerance, finalAdd_NeckAngle) +
                REBA_TableABC_object.Step1a_twisted_getNeckScore(twistTolerance, finalTwist_NeckAngle);
            if (neckScore1a > 0) neckScore += 1;

            int trunkScore = REBA_TableABC_object.Step2_getTrunkScore(flexTolerance, finalFlex_TrunkAngle);
            int trunkScore2a = REBA_TableABC_object.Step2a_sidebend_getTrunkScore(adductionTolerance, finalAdd_TrunkAngle) +
                REBA_TableABC_object.Step2a_twisted_getTrunkScore(twistTolerance, finalTwist_TrunkAngle);
            if (trunkScore2a > 0) trunkScore += 1;

            int l_legScore = REBA_TableABC_object.Step3_getLegScore(flexTolerance, l_finalFlex_LegAngle);
            int r_legScore = REBA_TableABC_object.Step3_getLegScore(flexTolerance, r_finalFlex_LegAngle);
            int uni_bilateralScore = REBA_TableABC_object.Step3a_isLegBilateralOrUnilateral(isLeftFootTouching, isRightFootTouching);
            l_legScore += uni_bilateralScore;
            r_legScore += uni_bilateralScore;

            scoreTextsTable_A[0].text = neckScore.ToString();//"NeckScore: " + neckScore;
            scoreTextsTable_A[1].text = trunkScore.ToString();
            scoreTextsTable_A[2].text = l_legScore.ToString();
            scoreTextsTable_A[3].text = r_legScore.ToString();

            // table b
            int l_uppArmScore = REBA_TableABC_object.Step7_getUpperArmScore(flexTolerance, l_finalFlex_UpperArm) +
                REBA_TableABC_object.Step7b_sidebend_getUpperArmScore(adductionTolerance, l_finalAdd_UpperArm);
            int r_uppArmScore = REBA_TableABC_object.Step7_getUpperArmScore(flexTolerance, r_finalFlex_UpperArm) +
                REBA_TableABC_object.Step7b_sidebend_getUpperArmScore(adductionTolerance, r_finalAdd_UpperArm);
            int shoulderIsRaisedScore = REBA_TableABC_object.Step7b_isShoulderRaised(isLeftShoulderTouching, isRightShoulderTouching);
            l_uppArmScore += shoulderIsRaisedScore;
            r_uppArmScore += shoulderIsRaisedScore;

            int l_lowArmScore = REBA_TableABC_object.Step8_getLowerArmScore(flexTolerance, l_finalFlex_LowerArm);
            int r_lowArmScore = REBA_TableABC_object.Step8_getLowerArmScore(flexTolerance, r_finalFlex_LowerArm);

            int l_wristScore = REBA_TableABC_object.Step9_getWristScore(flexTolerance, l_finalFlex_Wrist);
            int l_wristScore9a = REBA_TableABC_object.Step9a_sidebend_getWristScore(adductionTolerance, l_finalAdd_Wrist) +
                REBA_TableABC_object.Step9a_twisted_getWristScore(twistTolerance, l_finalTwist_Wrist);
            if (l_wristScore9a > 0) l_wristScore += 1;

            int r_wristScore = REBA_TableABC_object.Step9_getWristScore(flexTolerance, r_finalFlex_Wrist);
            int r_wristScore9a = REBA_TableABC_object.Step9a_sidebend_getWristScore(adductionTolerance, r_finalAdd_Wrist) +
               REBA_TableABC_object.Step9a_twisted_getWristScore(twistTolerance, r_finalTwist_Wrist);
            if (r_wristScore9a > 0) r_wristScore += 1;

            scoreTextsTable_B[0].text = l_uppArmScore.ToString();
            scoreTextsTable_B[1].text = l_lowArmScore.ToString();
            scoreTextsTable_B[2].text = l_wristScore.ToString();
            scoreTextsTable_B[3].text = r_uppArmScore.ToString();
            scoreTextsTable_B[4].text = r_lowArmScore.ToString();
            scoreTextsTable_B[5].text = r_wristScore.ToString();

        
            int tempLegScore = Math.Max(l_legScore, r_legScore);
            int tempUppArmScore = Math.Max(l_uppArmScore, r_uppArmScore);
            int tempLowArmScore = Math.Max(l_lowArmScore, r_lowArmScore);
            int tempWristScore = Math.Max(l_wristScore, r_wristScore);

            forceLoadScore = 0;
            activityScore = 0;

            activityScore = REBA_TableABC_object.Step13a_isRapidlyChanging(isRapidlyChanging);

            Debug.Log("Table A " + (neckScore - 1) + " " + (tempLegScore - 1) + " " + (trunkScore - 1));
            Debug.Log("Table B " + (tempLowArmScore - 1) + " " + (tempWristScore - 1) + " " + (tempUppArmScore - 1));
            
            
            // Ergebnisse der Berechnung von Step 1 bestimmen Score von Tabelle A. int [Z-Achse (Stockwerk), Y-Achse, X-Achse] 
            // Bei Arrays zählen wir logisch deshalb immer mit -1 aufrufen.
            tableA_PostureScore = REBA_TableABC_object.tableA[neckScore - 1, trunkScore - 1, tempLegScore - 1];
            tableB_PostureScore = REBA_TableABC_object.tableB[tempLowArmScore - 1, tempUppArmScore - 1,  tempWristScore - 1];
            tableC_PostureScore = REBA_TableABC_object.tableC[(tableB_PostureScore + forceLoadScore) - 1, (tableA_PostureScore + couplingScore) - 1];

            finalScoreTexts_A[0].text = tableA_PostureScore.ToString();
            finalScoreTexts_A[1].text = forceLoadScore.ToString();
            finalScoreTexts_A[2].text = (tableA_PostureScore + forceLoadScore).ToString();

            finalScoreTexts_B[0].text = tableB_PostureScore.ToString();
            finalScoreTexts_B[1].text = couplingScore.ToString();
            finalScoreTexts_B[2].text = (tableB_PostureScore + couplingScore).ToString();

            finalScoreTexts_C[0].text = tableC_PostureScore.ToString();
            finalScoreTexts_C[1].text = activityScore.ToString();
            finalScoreTexts_C[2].text = (tableC_PostureScore + activityScore).ToString();

            bool legXlateral;//if false unilateral, if true Bilateral
            if (isLeftFootTouching && isRightFootTouching == true)
            {
                AdditionalScores_A[0].text = "Bilateral";
                legXlateral = true;
            }
            else
            {
                AdditionalScores_A[0].text = "Unitlateral";
                legXlateral = false;
            }

            AdditionalScores_A[1].text = "lbs";
            AdditionalScores_A[2].text = "false";

            if (isLeftShoulderTouching || isRightShoulderTouching == true)
                AdditionalScores_B[0].text = "true";
            else
                AdditionalScores_B[0].text = "false";

            AdditionalScores_B[1].text = "false";

            bool isShoulderRaised = shoulderIsRaisedScore == 1 ? true : false;
         
            //AdditionalScores_C[0].

            // Go to the smoothing process
            REBA_ScoreAnalysis_object.AddNewScore(tableC_PostureScore + activityScore);
            if (experimentController_object != null)
            {
                experimentController_object.updateRebaLogdata(finalFlex_NeckAngle, finalAdd_NeckAngle, finalTwist_NeckAngle,
               finalFlex_TrunkAngle, finalAdd_TrunkAngle, finalTwist_TrunkAngle, r_finalFlex_LegAngle, l_finalFlex_LegAngle,
               l_finalFlex_UpperArm, r_finalFlex_UpperArm, l_finalAdd_UpperArm, r_finalFlex_LowerArm, l_finalFlex_LowerArm,
               r_finalFlex_Wrist, r_finalAdd_Wrist, r_finalTwist_Wrist, l_finalFlex_Wrist, l_finalAdd_Wrist, l_finalTwist_Wrist,
               neckScore, tempLegScore, l_legScore, r_legScore, trunkScore, legXlateral, currentLoadInLBS, false, tableA_PostureScore,
               tempLowArmScore, l_lowArmScore, r_lowArmScore, tempUppArmScore, l_uppArmScore, r_uppArmScore, tempWristScore,
               l_wristScore, r_wristScore, isShoulderRaised, false, tableB_PostureScore, couplingScore, 0, activityScore,
               tableC_PostureScore, (tableC_PostureScore + activityScore));
            }
            
        }
        catch (Exception e)
        {
            Debug.Log("Error when trying to calculate the scores: " + e);
        }
        
    }

    private void reconnectToExperimentController()
    {
        GameObject experimentControllerObject = GameObject.FindGameObjectWithTag("EXPERIMENT_CONTROLLER");

        if (experimentControllerObject != null)
        {
            experimentController_object = experimentControllerObject.GetComponent<REBA_ExperimentController>();

        }
        else
        {
            Debug.LogError("EXPERIMENT_CONTROLLER GameObject not found!");
        }

    }

    private int AdjustAngleByInitial(int angle, int initialAngle)
    {
        if (initialAngle < 0)
            return angle + (-1 * initialAngle);
        else if (initialAngle > 0)
            return angle - initialAngle;
        else
            return angle;
    }

    public void SetInitialAngles()
    {
        // table A
        initialFlex_NeckAngle = neckAngleFlexing;
        initialAdd_NeckAngle = neckAngleAdduction;
        initialTwist_NeckAngle = neckAngleTwist;

        initialFlex_TrunkAngle = trunkAngleFlexing;
        initialAdd_TrunkAngle = trunkAngleAdduction;
        initialTwist_TrunkAngle = trunkAngleTwist;

        r_initialFlex_LegAngle = r_legAngleFlexing;
        l_initialFlex_LegAngle = l_legAngleFlexing;

        // table B
        r_initialFlex_UpperArm = r_upperArmAngleFlexing;
        r_initialAdd_UpperArm = r_upperArmAngleAdduction;
        l_initialFlex_UpperArm = l_upperArmAngleFlexing;
        l_initialAdd_UpperArm = l_upperArmAngleAdduction;

        r_initialFlex_LowerArm = r_lowerArmAngleFlexing;
        l_initialFlex_LowerArm = l_lowerArmAngleFlexing;

        r_initialFlex_Hand = r_handAngleFlexing;
        r_initialAdd_Hand = r_handAngleAdduction;
        r_initialTwist_Hand = r_handAngleTwist;
        l_initialFlex_Hand = l_handAngleFlexing;
        l_initialAdd_Hand = l_handAngleAdduction;
        l_initialTwist_Hand = l_handAngleTwist;
    }

    public void ResetInitialAngles()
    {
        // table A
        initialFlex_NeckAngle = 0;
        initialAdd_NeckAngle = 0;
        initialTwist_NeckAngle = 0;

        initialFlex_TrunkAngle = 0;
        initialAdd_TrunkAngle = 0;
        initialTwist_TrunkAngle = 0;

        r_initialFlex_LegAngle = 0;
        l_initialFlex_LegAngle = 0;

        // table B
        r_initialFlex_UpperArm = 0;
        r_initialAdd_UpperArm = 0;
        l_initialFlex_UpperArm = 0;
        l_initialAdd_UpperArm = 0;

        r_initialFlex_LowerArm = 0;
        l_initialFlex_LowerArm = 0;

        r_initialFlex_Hand = 0;
        r_initialAdd_Hand = 0;
        r_initialTwist_Hand = 0;
        l_initialFlex_Hand = 0;
        l_initialAdd_Hand = 0;
        l_initialTwist_Hand = 0;
    }

    public void ToggleKeyInput()
    {
        if (isKeyInputEnabled == true) 
        { 
            isKeyInputEnabled = false;
            Debug.Log("ToggleKeyInput set to false");
            toggleButtonText.text = "Shortcuts Disabled";
        }
        else
        {
            isKeyInputEnabled = true;
            Debug.Log("ToggleKeyInput set to true");
            toggleButtonText.text = "Shortcuts Enabled";
        }
    }

    private void ToggleRenderers(Renderer[] renderers, bool state)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = state;
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

        // Convert this point to the plane's local space
        Vector3 localPoint = plane.transform.InverseTransformPoint(closestPoint);

        // Use Debug.DrawLine to visualize the shortest line
        Debug.DrawLine(bone.position, closestPoint, Color.yellow);

        // Log local x and z
        //Debug.Log("Bone " + bone.name + " Local x: " + localPoint.x.ToString("F2") + " Local z: " + localPoint.z.ToString("F2"));

        // Return 2D coordinates on the plane
        return new Vector2(localPoint.x, localPoint.z);
    }
}
