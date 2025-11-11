using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_StudyGuider : MonoBehaviour
{
    public REBA_ControllerInput REBA_ControllerInput_object;
    public REBA_ExperimentController REBA_ExperimentController_object;
    public bool buttonReleased;
    public GameObject[] initialPanels; 
    public GameObject[] repairTaskPanels; 
    public GameObject[] warehouseTaskPanels; 
    public GameObject[] clinicTaskPanels;

    private GameObject[] activeTaskPanels; // Holds the panels of the current task
    private int currentPanelIndex = 0; // Tracks the current panel index

    bool isInPanelMode;
    bool isFirstIntroPanel;

    // Start is called before the first frame update
    void Start()
    {
        isInPanelMode = false;
        buttonReleased = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInPanelMode)
        {
            if ((REBA_ControllerInput_object.rightControllerGrabIsPressed || REBA_ControllerInput_object.leftControllerGrabIsPressed) && buttonReleased)
            {
                buttonReleased = false;
                GoToNextPanel();
            }
            // Check if the button is released on both controllers
            if (!REBA_ControllerInput_object.rightControllerGrabIsPressed && !REBA_ControllerInput_object.leftControllerGrabIsPressed)
            {
                buttonReleased = true; // Reset buttonReleased to true when button is released
            }
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            SetActiveTaskPanels(clinicTaskPanels);
            isInPanelMode = true;
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            SetActiveTaskPanels(repairTaskPanels);
            isInPanelMode = true;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            SetActiveTaskPanels(warehouseTaskPanels);
            isInPanelMode = true;
        }

    }

    public void SetActiveTaskPanels(GameObject[] taskPanels)
    {
        // hier sind die aktiven Panels
        activeTaskPanels = taskPanels;
        currentPanelIndex = 0;

        // Initially turn off all panels except the first one
        foreach (var panel in activeTaskPanels)
        {
            panel.SetActive(false);
        }

        if (activeTaskPanels.Length > 0)
            activeTaskPanels[0].SetActive(true);

        // Allow to click through panels
        isInPanelMode = true;

    }

    public void GoToNextPanel()
    {
        // Turn off the current panel
        if (currentPanelIndex < activeTaskPanels.Length)
        {
            activeTaskPanels[currentPanelIndex].SetActive(false);
        }

        // Increment the panel index and check if it's the last panel
        currentPanelIndex++;
        if (currentPanelIndex >= activeTaskPanels.Length)
        {
            currentPanelIndex = 0; // Or signal to REBA_ExperimentController that task is ready to start
            foreach (var panel in activeTaskPanels)
            {
                panel.SetActive(false);
            }
            // Last panel erreicht
            isInPanelMode = false;
        }
        else
        {
            // Turn on the next panel
            activeTaskPanels[currentPanelIndex].SetActive(true);
        }
    }

    public void ShowIntroductionPanel(bool isTheFirstPanel)
    {
        if (isTheFirstPanel) { isFirstIntroPanel = true; }
        SetActiveTaskPanels(initialPanels);
    }

}
