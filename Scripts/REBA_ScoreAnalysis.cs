using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class REBA_ScoreAnalysis : MonoBehaviour
{
    const int numberOfFrames = 30;

    public float alpha = 0.6f; // Weight for the current score
    public float peakThreshold = 5; // Threshold for detecting a peak
    private float smoothedScore = 0;
    private Queue<float> lastScores = new Queue<float>();
    private float timeSinceLastCheck = 0;

    private float lastUIUpdateTime = 0f;

    public REBA_uiScoreController uiController;
    public REBA_Calculation REBA_Calculation_object;
    public REBA_ExperimentController experimentController_object;

    private List<float> rebaScoreList = new List<float>();
    private float timeAccumulator = 0f;

    public float smoothedRebaScore2mean = 0;

    // Call this method every time you get a new REBA score
    public void AddNewScore(float newScore)
    {
        rebaScoreList.Add(newScore);

        // Update the time accumulator
        timeAccumulator += Time.deltaTime;

        // Check if 1 second has passed
        if (timeAccumulator >= 1f)
        {
            // We can either choose to calculate the median or mean
            //float median = CalculateMedian(valueList);
            smoothedRebaScore2mean = CalculateMean(rebaScoreList);
            DisplaySmoothedREBA(smoothedRebaScore2mean);
            if(experimentController_object != null)
                experimentController_object.updateRebaSmoothLogdata(smoothedRebaScore2mean);
            DetectPeak();
            // Reset the list and time accumulator for the next second
            rebaScoreList.Clear();
            timeAccumulator -= 1f; // Use -= to account for any extra time accumulated over 1 second
        }

    }

    private float CalculateMean(List<float> values)
    {
        float summation = values.Sum();

        return summation / values.Count;
    }

    private float CalculateMedian(List<float> values)
    {
        float summation = values.Sum();

        summation = summation/ values.Count;

        // Sort the list to find the median
        values.Sort();
        int middleIndex = values.Count / 2;
        if (values.Count % 2 == 0)
        {
            // Even number of items, average the two middle values
            return (values[middleIndex] + values[middleIndex - 1]) / 2f;
        }
        else
        {
            // Odd number of items, return the middle item
            return values[middleIndex];
        }

    }

    private void DisplaySmoothedREBA(float smoothedScoreREBA)
    {
        Debug.Log($"Mean REBA score value: {smoothedScoreREBA}");
        int roundedScore = Mathf.RoundToInt(smoothedScoreREBA);
        uiController.UpdateScoreIndicator(roundedScore);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            reconnectToExperimentController();
        }
        //timeSinceLastCheck += Time.deltaTime;

        //if (timeSinceLastCheck >= 0.5f)
        //{
        //    DetectPeak();
        //    timeSinceLastCheck = 0;
        //}
        //// Update the UI every second
        //if (Time.time - lastUIUpdateTime >= 1.0f)
        //{
        //    lastUIUpdateTime = Time.time;
        //    int roundedScore = Mathf.RoundToInt(smoothedScore); // Assuming you want to round the score to an integer
        //    Debug.Log("roundedScore: " + roundedScore);
        //    uiController.UpdateScoreIndicator(roundedScore);
        //}
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
                Debug.LogError("Experiment_Controller GameObject not found!");
            }
    }

    private void DetectPeak()
    {
        float maxScore = float.MinValue;
        float minScore = float.MaxValue;

        foreach (float score in rebaScoreList)
        {
            if (score > maxScore) maxScore = score;
            if (score < minScore) minScore = score;
        }

        if (maxScore - minScore >= peakThreshold)
        {
            Debug.Log("Peak detected!");
            REBA_Calculation_object.isRapidlyChanging = true;
        }
        else
        {
            REBA_Calculation_object.isRapidlyChanging = false;

        }
    }

    public float GetSmoothedScore()
    {
        return smoothedScore;
    }
}
