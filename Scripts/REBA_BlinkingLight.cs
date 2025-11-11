using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_BlinkingLight : MonoBehaviour
{    
    public Light spotlight;  // Reference to the Light component
    public bool isMaintenanceLightActive = false;  // Condition for turning the light yellow
    // Condition for turning the light green
    // koennte genutzt werden um nach fertigen pipes zu suchen.
    public bool pipeFixed = false;      
    void Start()
    {
        // Start the blinking coroutine
        StartCoroutine(BlinkRedLight());
    }

    // Coroutine for blinking red light
    System.Collections.IEnumerator BlinkRedLight()
    {
        while (!isMaintenanceLightActive)
        {
            spotlight.color = Color.red;
            yield return new WaitForSeconds(1);  // Wait for 1 second

            spotlight.color = Color.black;  // Turn off the light
            yield return new WaitForSeconds(1);  // Wait for 1 second
        }
        Debug.Log("Yellow light on");
        // If conditionMet -> this specific lamp must be fixed, therefor yellow color
        spotlight.color = Color.yellow;
    }

    public void SetLightConditionToMaintenanceMode(bool isMaintenanceLightActive)
    {
        this.isMaintenanceLightActive = isMaintenanceLightActive;
    }

    public void SetPipeFixed(bool pipeFixed)
    {
        this.pipeFixed = pipeFixed;
        // If conditionMet and pipeFixed
        spotlight.color = Color.green;
    }
}
