using QuestionnaireToolkit.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
//using Unity.VisualScripting.ReorderableList.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class REBA_ExperimentController : MonoBehaviour
{
    //public enum OrderOptions { BalancedLatinSquare, LatinSquare, Permutations, Random };
    public enum GenderOptions { Male, Female, Diverse, NonBinary, Other, DoNotWantToSpecify };
    public enum RecruitmentOption { Student, Staff, FamilyAndFriends, InvitedExternal, Other };

    //public enum EnvironmentType { Clinic, RepairStation, Warehouse };

    //private OrderOptions selectedOrder;

    [Header("Demographics")]
    public int SubjectID;
    public int Age;
    public int Height;
    public GenderOptions Gender;
    public RecruitmentOption Recruitment;

    [Header("Experimental Conditions")]
    public GameObject[] conditions; // Click on "Conditions" in Inspector, enter # conditions, assign game objects
    public int currentConditionNumber = 0;
    public string currentConditionName = "";

    public GameObject OVRCameraRigReference;

    //[Header("Experimental Order of Conditions")]
    //public OrderOptions ConditionOrder;

    [Header("Questionnaire Toolkit")]
    public GameObject UIHelpers;
    public GameObject QTQuestionnaireManager;
    private QTManager qtManager;
    private QTQuestionnaireManager qtQManager;
    private int currentQuestionnaireNumber = 0;
    [SerializeField] private bool trialRunning;

    [Header("Database")]
    public string dataBaseConditionPath = @"Assets/Database/Conditions/";
    public string dataBaseSceneOrderPath = @"Assets/Database/Sceneorder/";

    [Header("Logging Paths")]
    public string loggingPath = @"Assets/Results/Logs/";
    public string questionnairePath = @"Assets/Results/Questionnaires/";

    [Header("Logging Data")]

    [Header("Functionaliy")]
    [SerializeField] REBA_ScoreAnalysis REBA_ScoreAnalysis_object;

    [SerializeField] REBA_SceneSwitcher REBA_SceneSwitcher_object;
    [SerializeField] public REBA_AssemblyLine assemblyLine;
    [SerializeField] bool canAdjustForScene;//not in use
    [SerializeField] bool isReferencesConnected;

    [SerializeField] string sceneName;
    Scene currentScene;

    OVRCameraRig ovrCameraRig;
    OVRHeadsetEmulator ovrHeadsetEmulator;
    private StreamWriter fileWriter;

    [Header("Reconnect with these Gameobjects")]
    public GameObject REBA_HUD;
    public REBA_StudyGuider study_Guider_Object;
    public GameObject Foot_Positioning;

    public GameObject clinic; // equals to A
    public GameObject repairstation; // equals to B
    public GameObject warehouse; // equals to C

    #region reconnect gameobjects    
    public GameObject btn_startStudy;
    public GameObject btn_Questionnaire;

    public GameObject avatarPants;
    public GameObject avatarShorts;
    public GameObject avatarShirt;
    public GameObject avatarEyebrow;
    public GameObject avatarBody;
    public GameObject avatarEyelashes;
    public GameObject avatarEyes;
    public GameObject avatarMouth;
    public GameObject avatarTear;
    public GameObject avatarLeftShoe;
    public GameObject avatarRightShoe;
    
    public GameObject ClinicPuppet;
    public REBA_RepairsceneManager REBA_RepairsceneManager_object;
    public GameObject rebaBox;
    public GameObject weighingMachine;
    public GameObject assemblyGameobject;

    GameObject User_Camera;

    const string tag_avatarPants = "avatarPants";
    const string tag_avatarShorts = "avatarShorts";
    const string tag_avatarShirt = "avatarShirt";
    const string tag_avatarEyebrown = "avatarEyebrown";
    const string tag_avatarBody = "avatarBody";
    const string tag_avatarEyelashes = "avatarEyelashes";
    const string tag_avatarEyes = "avatarEyes";
    const string tag_avatarMouth = "avatarMouth";
    const string tag_avatarTear = "avatarTear";

    const string tag_avatarLeftShoe = "avatarLeftShoe";
    const string tag_avatarRightShoe = "avatarRightShoe";

    const string tag_REBA_HUD = "REBA_HUD";
    const string tag_User_Camera = "User_Camera";

    const string tag_clinic_scene = "clinic_scene";
    const string tag_ClinicPuppet = "ClinicPuppet";

    const string tag_repair_scene = "repair_scene";
    const string tag_Repairscene_Manager = "Repairscene_Manager";    

    const string tag_warehouse_scene = "warehouse_scene";
    const string tag_reba_box= "REBA_Box";
    const string tag_Weight_Machine = "Weight_Machine";

    const string tag_ASSEMBLY_LINE = "ASSEMBLY_LINE";
    const string tag_btn_startStudy = "btn_startStudy";
    const string tag_btn_Questionnaire = "btn_Questionnaire";
    const string tag_Study_Guider = "Study_Guider";
    const string tag_Foot_Positioning = "Foot_Positioning";
    #endregion


    [Header("Study Flow")]    
    
    public bool canLogData;
    //public Dictionary<int, int[]> participantConditions = new Dictionary<int, int[]>();
    private string[] currentParticipantConditions;
    private string[] currentSceneOrder;
    private int currentConditionIndex = 0;
    // checks wether to show the introductory panel
    public bool studyAtTheBeginning;
    private float taskTimeLimit = 300f; // 5 minutes in seconds
    char scene1;
    char scene2;
    char scene3;
    int currenSceneCounter;
    char currenSceneChar;
    bool hudEnabled;
    bool avatarEnabled;

    #region logging data
    // Angle variables
    public int finalFlex_NeckAngle;
    public int finalAdd_NeckAngle;
    public int finalTwist_NeckAngle;

    public int finalFlex_TrunkAngle;
    public int finalAdd_TrunkAngle;
    public int finalTwist_TrunkAngle;

    public int r_finalFlex_LegAngle;
    public int l_finalFlex_LegAngle;

    public int l_finalFlex_UpperArm;
    public int r_finalFlex_UpperArm;
    public int l_finalAdd_UpperArm;
    public int r_finalAdd_UpperArm;

    public int r_finalFlex_LowerArm;
    public int l_finalFlex_LowerArm;

    public int r_finalFlex_Wrist;
    public int r_finalAdd_Wrist;
    public int r_finalTwist_Wrist;
    public int l_finalFlex_Wrist;
    public int l_finalAdd_Wrist;
    public int l_finalTwist_Wrist;

    // Score variables
    public int neckScore;
    public int LegScore;
    public int l_legScore;
    public int r_legScore;
    public int trunkScore;
    public bool legXLateral; // Assuming float as it appears to represent a position or distance, common in biomechanics.
    public int load; // Assuming float as it likely represents a weight or force.
    public bool force; // Assuming float since forces are typically measured with decimal precision.
    public int tableScoreA;
    public int lowArmScore;
    public int l_lowArmScore;
    public int r_lowArmScore;
    public int uppArmScore;
    public int l_uppArmScore;
    public int r_uppArmScore;
    public int wristScore;
    public int l_wristScore;
    public int r_wristScore;

    // State variables
    public bool shoudlerRaised; // Assuming bool as it represents a true/false state.
    public bool armSupport; // Assuming bool as it represents a true/false state.
    public int tableScoreB;
    public int couplingScore;
    public int forceLoadScore;
    public int activityScore;
    public int tableScoreC;
    public int finalScoreREBA;
    public int smoothedScoreREBA;

    public bool isParticipantActivelyEngaged;
    #endregion

    void Start()
    {
        currenSceneCounter = 0;
        studyAtTheBeginning = true;
        smoothedScoreREBA = 0; // since the score takes a full second to be calculated for the first time we will initialize it 
        isReferencesConnected = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
        canLogData = false;
        User_Camera = GameObject.FindWithTag(tag_User_Camera);
        User_Camera.GetComponent<Camera>().enabled = false;
        //reconnectAllReferences();

    }
    
    void Update()
    {
        if (canLogData) { 
            //isParticipantActivelyEngaged = true;
            LogEvent();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            reconnectAllReferences();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            canLogData = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            canLogData = false;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            SetHUDVisibility(true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            SetHUDVisibility(false);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            SetAvatarVisibility(true);
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            SetAvatarVisibility(false);
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            clinic.SetActive(true);
            ClinicPuppet.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            repairstation.SetActive(true);
            REBA_RepairsceneManager_object.UpdateCurrentPipeIndicator();
        }        
        if (Input.GetKeyUp(KeyCode.O))
        {
            rebaBox.SetActive(true);
            weighingMachine.SetActive(true);            
            warehouse.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            clinic.SetActive(false);
            ClinicPuppet.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            repairstation.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            warehouse.SetActive(false);
            rebaBox.SetActive(false);
            weighingMachine.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            isParticipantActivelyEngaged = true;
        }
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            isParticipantActivelyEngaged = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            REBA_RepairsceneManager_object.UpdateCurrentPipeIndicator();
        }

    }

    public void startStudy()
    {
        Debug.Log("Start of the Study");
        
        ReadConditionsCSV();
        ReadParticipantSceneOrderCSV();
        //prepareNextCondition();

        // Wir nutzen diese Flag um zu checken ob das einfuehrungspanel gezeigt werden soll
        if (studyAtTheBeginning)
        {
            study_Guider_Object.ShowIntroductionPanel(studyAtTheBeginning);
            studyAtTheBeginning = false;
        }
        else
        {
            setupConditionsAndScene();
        }

        CreateLogFile();
        canLogData = true;
    }
    public void OnIntroductionPanelComplete()
    {
        // clinic equals to A
        // repairstation equals to B
        // warehouse equals to C

        isParticipantActivelyEngaged = true;

        // Activate the appropriate scene based on the scene code
        if (currenSceneChar == 'A')
        {
            clinic.SetActive(true);
            ClinicPuppet.SetActive(true);            
        }
        else if (currenSceneChar == 'B')
        {
            repairstation.SetActive(true);
            REBA_RepairsceneManager_object.UpdateCurrentPipeIndicator();
        }
        else if (currenSceneChar == 'C')
        {
            warehouse.SetActive(true);
            rebaBox.SetActive(true);
            weighingMachine.SetActive(true);
        }

    }
    public void setupConditionsAndScene()
    {
        clinic.SetActive(false);
        repairstation.SetActive(false);
        try
        {
            ClinicPuppet.SetActive(false);
            weighingMachine.SetActive(false);
            assemblyGameobject.GetComponent<REBA_AssemblyLine>().StopSpawningAndMovement();
        }
        catch (Exception e) { Debug.Log(e); }

        warehouse.SetActive(false);

        switch (currenSceneCounter)
        {
            case 0:
                ActivateSceneWithPanel(scene1);
                break;
            case 1:
                ActivateSceneWithPanel(scene2);
                break;
            case 2:
                ActivateSceneWithPanel(scene3);
                break;
        }
        currenSceneCounter++;
    }
    private void ActivateSceneWithPanel(char sceneCode)
    {
        // clinic equals to A
        // repairstation equals to B
        // warehouse equals to C

        // Activate the appropriate scene based on the scene code
        if (sceneCode == 'A')
        {
            study_Guider_Object.SetActiveTaskPanels(study_Guider_Object.clinicTaskPanels);
            currenSceneChar = 'A';
        }
        else if (sceneCode == 'B')
        {
            study_Guider_Object.SetActiveTaskPanels(study_Guider_Object.repairTaskPanels);
            currenSceneChar = 'B';
        }
        else if (sceneCode == 'C')
        {
            study_Guider_Object.SetActiveTaskPanels(study_Guider_Object.warehouseTaskPanels);
            currenSceneChar = 'C';
        }
    }
    public void OnTaskCompleted()
    {
        isParticipantActivelyEngaged = false;
        // Call this method when a task is completed or the time limit is reached
        if (currenSceneCounter < 3) // Assuming there are 3 tasks
        {
            setupConditionsAndScene(); // Prepare the next task
        }
        else
        {
            SwitchToQuestionnaire(); // All tasks completed, move to questionnaire
            canLogData = false;
        }
    }
    public void reconnectAllReferences()
    {
        try
        {           
            // If participant is doing a task -> false, else true
            isParticipantActivelyEngaged = false;

            avatarBody = GameObject.FindWithTag(tag_avatarBody);
            avatarEyebrow = GameObject.FindWithTag(tag_avatarEyebrown);
            avatarEyelashes = GameObject.FindWithTag(tag_avatarEyelashes);
            avatarEyes = GameObject.FindWithTag(tag_avatarEyes);
            avatarMouth = GameObject.FindWithTag(tag_avatarMouth);
            avatarPants = GameObject.FindWithTag(tag_avatarPants);
            avatarShirt = GameObject.FindWithTag(tag_avatarShirt);
            avatarShorts = GameObject.FindWithTag(tag_avatarShorts);
            avatarTear = GameObject.FindWithTag(tag_avatarTear);
            avatarLeftShoe = GameObject.FindWithTag(tag_avatarLeftShoe);
            avatarRightShoe = GameObject.FindWithTag(tag_avatarRightShoe);
            REBA_HUD = GameObject.FindWithTag(tag_REBA_HUD);
            clinic = GameObject.FindWithTag(tag_clinic_scene);
            repairstation = GameObject.FindWithTag(tag_repair_scene);
            warehouse = GameObject.FindWithTag(tag_warehouse_scene);

            btn_startStudy = GameObject.FindWithTag(tag_btn_startStudy);
            if(btn_startStudy != null)
                btn_startStudy.GetComponent<Button>().interactable = true;
            btn_Questionnaire = GameObject.FindWithTag(tag_btn_Questionnaire);
            if(btn_Questionnaire != null)
                btn_Questionnaire.GetComponent<Button>().interactable = true;
            assemblyGameobject = GameObject.FindWithTag(tag_ASSEMBLY_LINE);
            assemblyLine = assemblyGameobject.GetComponent<REBA_AssemblyLine>();

            Foot_Positioning = GameObject.FindWithTag(tag_Foot_Positioning);
            study_Guider_Object = GameObject.FindWithTag(tag_Study_Guider).GetComponent<REBA_StudyGuider>();
            study_Guider_Object.REBA_ExperimentController_object = this;

            rebaBox = GameObject.FindWithTag(tag_reba_box);
            weighingMachine = GameObject.FindWithTag(tag_Weight_Machine);
            ClinicPuppet = GameObject.FindWithTag(tag_ClinicPuppet);
            REBA_RepairsceneManager_object = GameObject.FindWithTag(tag_Repairscene_Manager).GetComponent<REBA_RepairsceneManager>();
            REBA_RepairsceneManager_object.REBA_ExperimentController_object = this;
            assemblyGameobject.GetComponent<REBA_AssemblyLine>().StopSpawningAndMovement();
            // Set everything inactive
            ClinicPuppet.SetActive(false);
            rebaBox.SetActive(false);
            weighingMachine.SetActive(false);
            clinic.SetActive(false);
            repairstation.SetActive(false);
            warehouse.SetActive(false);


            smoothedScoreREBA = 0; // since the score takes a full second to be calculated for the first time we will initialize it 

            SetHUDVisibility(false);
            SetAvatarVisibility(false);

            User_Camera.GetComponent<Camera>().enabled = true;

            Debug.Log("reconnectAllReferences successful");

        }
        catch (Exception e)
        {
            Debug.Log("reconnectAllReferences has failed \n" + e);
        }
    }
   
    public void ShowQuestionnaire()
    {
        Debug.Log("HERE ShowQuestionnaire");
        trialRunning = false;
        qtManager = QTQuestionnaireManager.GetComponent<QTManager>();
        qtQManager = qtManager.questionnaires[currentQuestionnaireNumber];
        qtQManager.resultsSavePath = questionnairePath;
        qtQManager.StartQuestionnaire();
    }

    void ReadConditionsCSV()
    {
        string path = Path.Combine(dataBaseConditionPath, "conditions.csv");
        string[] lines = File.ReadAllLines(path);
        // Skip the header row by starting from index 1
        for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
        {
            string line = lines[lineIndex];
            string[] values = line.Split(',');
            int id;
            if (int.TryParse(values[0].Trim(), out id) && id == SubjectID)
            {
                currentParticipantConditions = values;
                break;
            }
        }
        // Print the current order of the conditions
        if (currentParticipantConditions != null)
        {
            Debug.Log("Order of the conditions");
            string orderConditions = "";
            foreach (string condition in currentParticipantConditions)
            {
                orderConditions += condition + " ";                
            }
            Debug.Log(orderConditions);
        }
    }
    void ReadParticipantSceneOrderCSV ()
    {
        string path = Path.Combine(dataBaseSceneOrderPath, "experiment_data.csv");
        string[] lines = File.ReadAllLines(path);
        for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
        {
            string line = lines[lineIndex];
            string[] values = line.Split(',');
            int id;
            if (int.TryParse(values[0].Trim(), out id) && id == SubjectID)
            {
                Debug.Log("Found the scene experiment");
                currentSceneOrder = values;
                break;
            }
        }
    }

    public void prepareNextCondition()
    {
        // To enable the project to loop through all conditions we make use the currentConditionIndex
        // that increments through all conditions.
        if (currentConditionIndex < currentParticipantConditions.Length - 1)
        {
            string currentConditionCode = currentParticipantConditions[currentConditionIndex + 1];
            string sceneOrder = currentSceneOrder[currentConditionIndex + 1];

            // Parse condition code to set HUD and avatar visibility
            hudEnabled = (int.Parse(currentConditionCode) & 2) != 0;
            avatarEnabled = (int.Parse(currentConditionCode) & 1) != 0;
            Debug.Log("CurrentCondition Number: " + currentConditionCode);
            Debug.Log("Condition HUD " + hudEnabled + " Condition Avatar:" + avatarEnabled);
            // Extract individual scenes from the currentSceneOrder
            scene1 = sceneOrder[0]; // First scene
            scene2 = sceneOrder[1]; // Second scene
            scene3 = sceneOrder[2]; // Third scene            
            currentConditionIndex++;
        }
    }
    public void SetHUDVisibility(bool hudEnabled)
    {
        if (hudEnabled) REBA_HUD.SetActive(true);
        else if (!hudEnabled) REBA_HUD.SetActive(false);
    }
    public void SetAvatarVisibility(bool avatarEnabled)
    {
        if (avatarEnabled)
        {
            avatarPants.SetActive(true);
            avatarShorts.SetActive(true);
            avatarShirt.SetActive(true);
            avatarEyebrow.SetActive(true);
            avatarBody.SetActive(true);
            avatarEyelashes.SetActive(true);
            avatarEyes.SetActive(true);
            avatarMouth.SetActive(true);
            avatarTear.SetActive(true);
            avatarLeftShoe.SetActive(true);
            avatarRightShoe.SetActive(true);
        }
        else if (!avatarEnabled)
        {
            avatarPants.SetActive(false);
            avatarShorts.SetActive(false);
            avatarShirt.SetActive(false);
            avatarEyebrow.SetActive(false);
            avatarBody.SetActive(false);
            avatarEyelashes.SetActive(false);
            avatarEyes.SetActive(false);
            avatarMouth.SetActive(false);
            avatarTear.SetActive(false);
            avatarLeftShoe.SetActive(false);
            avatarRightShoe.SetActive(false);
        } 
    }
    
    // Setup this up once the panelstuff is done
    
    public void SwitchToStudy()
    {
        currentQuestionnaireNumber = 0;
        UIHelpers.SetActive(false);
        OVRCameraRigReference.SetActive(false);
        SceneManager.LoadScene("REBA_Study Experiment Scene");
        isReferencesConnected = false;
    }

    public void SwitchToQuestionnaire()
    {
        SceneManager.LoadScene("Question Scene");
    }
    public void updateRebaLogdata(int finalFlex_NeckAngle,int finalAdd_NeckAngle,int finalTwist_NeckAngle,
        int finalFlex_TrunkAngle,int finalAdd_TrunkAngle,int finalTwist_TrunkAngle,int r_finalFlex_LegAngle,
        int l_finalFlex_LegAngle,int l_finalFlex_UpperArm,int r_finalFlex_UpperArm,int l_finalAdd_UpperArm,
        int r_finalFlex_LowerArm,int l_finalFlex_LowerArm,int r_finalFlex_Wrist,int r_finalAdd_Wrist,
        int r_finalTwist_Wrist,int l_finalFlex_Wrist,int l_finalAdd_Wrist,int l_finalTwist_Wrist,
        int neckScore,int LegScore,int l_legScore,int r_legScore,int trunkScore,bool legXLateral,
        int load,bool force,int tableScoreA,int lowArmScore,int l_lowArmScore,int r_lowArmScore,
        int uppArmScore,int l_uppArmScore,int r_uppArmScore,int wristScore,int l_wristScore,
        int r_wristScore,bool shoudlerRaised,bool armSupport,int tableScoreB,int couplingScore,int forceLoadScore,
        int activityScore,int tableScoreC,int finalScoreREBA)
    {
        // Assign parameter values to class variables
        this.finalFlex_NeckAngle = finalFlex_NeckAngle;
        this.finalAdd_NeckAngle = finalAdd_NeckAngle;
        this.finalTwist_NeckAngle = finalTwist_NeckAngle;
        this.finalFlex_TrunkAngle = finalFlex_TrunkAngle;
        this.finalAdd_TrunkAngle = finalAdd_TrunkAngle;
        this.finalTwist_TrunkAngle = finalTwist_TrunkAngle;
        this.r_finalFlex_LegAngle = r_finalFlex_LegAngle;
        this.l_finalFlex_LegAngle = l_finalFlex_LegAngle;
        this.l_finalFlex_UpperArm = l_finalFlex_UpperArm;
        this.r_finalFlex_UpperArm = r_finalFlex_UpperArm;
        this.l_finalAdd_UpperArm = l_finalAdd_UpperArm;
        this.r_finalFlex_LowerArm = r_finalFlex_LowerArm;
        this.l_finalFlex_LowerArm = l_finalFlex_LowerArm;
        this.r_finalFlex_Wrist = r_finalFlex_Wrist;
        this.r_finalAdd_Wrist = r_finalAdd_Wrist;
        this.r_finalTwist_Wrist = r_finalTwist_Wrist;
        this.l_finalFlex_Wrist = l_finalFlex_Wrist;
        this.l_finalAdd_Wrist = l_finalAdd_Wrist;
        this.l_finalTwist_Wrist = l_finalTwist_Wrist;

        this.neckScore = neckScore;
        this.LegScore = LegScore;
        this.l_legScore = l_legScore;
        this.r_legScore = r_legScore;
        this.trunkScore = trunkScore;
        this.legXLateral = legXLateral;
        this.load = load;
        this.force = force;
        this.tableScoreA = tableScoreA;
        this.lowArmScore = lowArmScore;
        this.l_lowArmScore = l_lowArmScore;
        this.r_lowArmScore = r_lowArmScore;
        this.uppArmScore = uppArmScore;
        this.l_uppArmScore = l_uppArmScore;
        this.r_uppArmScore = r_uppArmScore;
        this.wristScore = wristScore;
        this.l_wristScore = l_wristScore;
        this.r_wristScore = r_wristScore;

        this.shoudlerRaised = shoudlerRaised;
        this.armSupport = armSupport;
        this.tableScoreB = tableScoreB;
        this.couplingScore = couplingScore;
        this.forceLoadScore = forceLoadScore;
        this.activityScore = activityScore;
        this.tableScoreC = tableScoreC;
        this.finalScoreREBA = finalScoreREBA;
    }


    public void updateRebaSmoothLogdata(float mean)
    {
        smoothedScoreREBA = Mathf.RoundToInt( mean);
    }
    public void CreateLogFile()
    {
        fileWriter = new StreamWriter(loggingPath + "eventLog_" + System.DateTime.Now.Ticks + ".csv");
        fileWriter.AutoFlush = true;
        fileWriter.WriteLine("TimeStamp;SubjectID;Gender;Age;Recruitment;currentConditionName;currentConditionNumber;" +
            "finalFlex_NeckAngle;finalAdd_NeckAngle;finalTwist_NeckAngle;finalFlex_TrunkAngle;" +
            "finalAdd_TrunkAngle;finalTwist_TrunkAngle;r_finalFlex_LegAngle;l_finalFlex_LegAngle;l_finalFlex_UpperArm;" +
            "r_finalFlex_UpperArm;l_finalAdd_UpperArm;r_finalFlex_LowerArm;l_finalFlex_LowerArm;" +
            "r_finalFlex_Wrist;r_finalAdd_Wrist;r_finalTwist_Wrist;l_finalFlex_Wrist;" +
            "l_finalAdd_Wrist;l_finalTwist_Wrist;" +
            "neckScore;LegScore;l_legScore;r_legScore;trunkScore;" +
            "legXLateral;load;force;tableScoreA;" +
            "lowArmScore;l_lowArmScore;r_lowArmScore;uppArmScore;" +
            "l_uppArmScore;r_uppArmScore;wristScore;l_wristScore;r_wristScore;" +
            "shoudlerRaised;armSupport;tableScoreB;couplingScore;" +
            "forceLoadScore;activityScore;tableScoreC;finalScoreREBA;smoothedScoreREBA;isParticipantActivelyEngaged");
    }

    private void LogEvent()
    {
        smoothedScoreREBA = Mathf.RoundToInt(REBA_ScoreAnalysis_object.smoothedRebaScore2mean);

        string fileOutput = System.DateTime.Now.Ticks + ";" + SubjectID + ";" + Gender + ";" + Age + ";" + Recruitment + ";" +
        currentConditionName + ";" + currentConditionNumber + ";" +
        finalFlex_NeckAngle + ";" + finalAdd_NeckAngle + ";" + finalTwist_NeckAngle + ";" + finalFlex_TrunkAngle + ";" + 
        finalAdd_TrunkAngle + ";" + finalTwist_TrunkAngle + ";" + r_finalFlex_LegAngle + ";" +l_finalFlex_LegAngle + ";" + l_finalFlex_UpperArm + ";" +
        r_finalFlex_UpperArm + ";" + l_finalAdd_UpperArm + ";" + r_finalFlex_LowerArm + ";" + l_finalFlex_LowerArm + ";" +
        r_finalFlex_Wrist + ";" + r_finalAdd_Wrist + ";" + r_finalTwist_Wrist + ";" + l_finalFlex_Wrist + ";" +
        l_finalAdd_Wrist + ";" + l_finalTwist_Wrist + ";" +
        neckScore + ";" + LegScore + ";" + l_legScore + ";" + r_legScore + ";" + trunkScore + ";" +
        legXLateral + ";" + load + ";" + force + ";" + tableScoreA + ";" +
        lowArmScore + ";" + l_lowArmScore + ";" + r_lowArmScore + ";" + uppArmScore + ";" +
        l_uppArmScore + ";" + r_uppArmScore + ";" + wristScore + ";" + l_wristScore + ";" + r_wristScore + ";" +
        shoudlerRaised + ";" + armSupport + ";" + tableScoreB + ";" + couplingScore + ";" +
        forceLoadScore + ";" + activityScore + ";" + tableScoreC + ";" + finalScoreREBA + ";" + smoothedScoreREBA + ";" + isParticipantActivelyEngaged;
        print(fileOutput);
        fileWriter.WriteLine(fileOutput);
    }

    public void NextCondition()
    {
        foreach (var questionnaire in qtManager.questionnaires)
        {
            questionnaire.ResetQuestionnaire();
            questionnaire.HideQuestionnaire();
        }
        currentQuestionnaireNumber = 0;
        //REBA_SceneSwitcher_object.SwitchToStudy();
        //canAdjustForScene = true;

        //if (ovrCameraRig != null)
        //{
        //    ovrCameraRig.enabled = false;
        //}
        //if (ovrHeadsetEmulator != null)
        //{
        //    ovrHeadsetEmulator.enabled = false;
        //}
    }
    // XOF Methode nicht in gebrauch und von Schwinds github projekt genommen zum vergleich
    public void nextQuestionnaire()
    {
        if (QTQuestionnaireManager)
        {
            print("Showing next questionnaire...");
            qtQManager.HideQuestionnaire();
            currentQuestionnaireNumber++;
            qtQManager = qtManager.questionnaires[currentQuestionnaireNumber];
            qtQManager.resultsSavePath = "/" + questionnairePath.TrimStart('/').TrimEnd('/') + "/";
            qtQManager.StartQuestionnaire();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = scene.name;

        if (sceneName == "REBA_Study Experiment Scene")
        {
            Debug.Log("Study Scene was loaded");
            if (currentConditionIndex > 3)
            {
                // #if UNITY_EDITOR is used to ensure that EditorApplication.isPlaying is only called in the Unity Editor environment.
                // This prevents any build errors when creating a build of your game.
                #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // Quit play mode in the Unity Editor
                #endif
            }
        }
        else if (sceneName == "Question Scene")
        {
            Debug.Log("Questionnaire Scene was loaded");
            //REBA_SceneSwitcher_object = FindAnyObjectByType<REBA_SceneSwitcher>();
            //qtManager = FindAnyObjectByType<QTManager>(); // GameObject must be active, or it won't be found
            ShowQuestionnaire();
            UIHelpers.SetActive(true);
            OVRCameraRigReference.SetActive(true);
            // Assuming the OVRCameraRig is a MonoBehaviour derived script
            ovrCameraRig = OVRCameraRigReference.GetComponent<OVRCameraRig>();
            ovrHeadsetEmulator = OVRCameraRigReference.GetComponent<OVRHeadsetEmulator>();
        }
    }

    public void ExitUnityApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Quit play mode in the Unity Editor
#endif
    }
    void OnDestroy()
    {
        // Unregister the OnSceneLoaded method from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
