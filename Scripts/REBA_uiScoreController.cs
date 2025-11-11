using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class REBA_uiScoreController : MonoBehaviour
{
    float firstPosition = -130.4f;
    float lastPosition = 168.6f;
    int totalPositions = 15;
    List<float> positionXValues = new List<float>();

    [Header("References")]
    [SerializeField] private RectTransform barRectTransform;
    [SerializeField] private Image barFill;
    [SerializeField] private RawImage rebaBarSegments_rawImage;
    [SerializeField] private RectTransform rebaBarSegment_rectTransform;
    [SerializeField] private TextMeshProUGUI colorMode;
    [SerializeField] private RectTransform scoreIndicatorRectTransform;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI feedback;

    [Header("Colors")]
    [SerializeField] private Color negligibleRiskColor;
    [SerializeField] private Color lowRiskColor;
    [SerializeField] private Color mediumRiskColor;
    [SerializeField] private Color highRiskColor;
    [SerializeField] private Color veryHighRiskColor;

    [Header("Functionalities")]
    private float segmentWidth;
    [SerializeField] private bool canUpdateBarFillColor;

    private void Awake()
    {
        segmentWidth = barRectTransform.rect.width / 14f;        
        canUpdateBarFillColor = false;
        barFill.color = new Color(barFill.color.r, barFill.color.g, barFill.color.b, 0f);

        // Calculate the total distance and new step size
        float totalDistance = lastPosition - firstPosition;
        float newStepSize = totalDistance / (totalPositions - 1);

        // Calculate and store the x-values
        for (int i = 0; i < totalPositions; i++)
        {
            float xPos = firstPosition + (i * newStepSize);
            positionXValues.Add(xPos);
        }
    }
    private void Start()
    {
    }

    public void switchBarColorDesign()
    {
        if (canUpdateBarFillColor == false) 
        {
            barFill.color = new Color(barFill.color.r, barFill.color.g, barFill.color.b, 1f);
            rebaBarSegments_rawImage.color = new Color(rebaBarSegments_rawImage.color.r, rebaBarSegments_rawImage.color.g, rebaBarSegments_rawImage.color.b, 0f);
            canUpdateBarFillColor = true;
            colorMode.text = "Coloring Mode: Dynamic";
        }
        else
        {
            barFill.color = new Color(barFill.color.r, barFill.color.g, barFill.color.b, 0f);
            rebaBarSegments_rawImage.color = new Color(rebaBarSegments_rawImage.color.r, rebaBarSegments_rawImage.color.g, rebaBarSegments_rawImage.color.b, 1f);
            canUpdateBarFillColor = false;
            colorMode.text = "Coloring Mode: Static";
        }

    }

    public void UpdateScoreIndicator(int score)
    {
        //float scaleFactor = rebaBarSegments_rawImage.texture.width / rebaBarSegment_rectTransform.rect.width;
        //float scaledSegmentWidth = segmentWidth * scaleFactor;

        Vector2 targetPosition = new Vector2(positionXValues[score-1], 47);
        StopAllCoroutines();
        StartCoroutine(MoveToPosition(targetPosition, score));
    }

    void UpdateColor(int score)
    {
        Debug.Log("Color " + score);
        if (score == 1)
        {
            barFill.color = negligibleRiskColor;
        }
        else if (score >= 2 && score <= 3)
        {
            barFill.color = lowRiskColor;
        }
        else if (score >= 4 && score <= 7)
        {
            barFill.color = mediumRiskColor;
        }
        else if (score >= 8 && score <= 10)
        {
            barFill.color = highRiskColor;
        }
        else if (score >= 11)
        {
            barFill.color = veryHighRiskColor;
        }
    }

    void UpdateFeedback(int score)
    {
        if (score == 1)
        {
            feedback.text = "Result: 1 - negligible risk";
        }
        else if (score >= 2 && score <= 3)
        {
            feedback.text = "Result: " + score.ToString() + " - low risk";
        }
        else if (score >= 4 && score <= 7)
        {
            feedback.text = "Result: " + score.ToString() + " - medium risk";
        }
        else if (score >= 8 && score <= 10)
        {
            feedback.text = "Result: " + score.ToString() + " - high risk";
        }
        else if (score >= 11)
        {
            feedback.text = "Result: " + score.ToString() + " - very high risk";
        }
    }

    private System.Collections.IEnumerator MoveToPosition(Vector2 targetPosition, int score)
    {

        float elapsedTime = 0f;
        float duration = 0.5f;
        Vector2 startingPosition = scoreIndicatorRectTransform.anchoredPosition;

        while (elapsedTime < duration)
        {
            scoreIndicatorRectTransform.anchoredPosition = Vector2.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (canUpdateBarFillColor)
        {
            UpdateColor(score);
        }
        UpdateFeedback(score);
        scoreText.text = score.ToString();
        scoreIndicatorRectTransform.anchoredPosition = targetPosition;
    }
}
