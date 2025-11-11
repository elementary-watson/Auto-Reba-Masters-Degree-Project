using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_shoulderCollider : MonoBehaviour
{
    public REBA_Calculation rebaCalculationScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // r_shoulderIntruder
        // l_shoulderDetector
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the foot is colliding with the floor
        if (other.CompareTag("l_shoulderDetector") || other.CompareTag("r_shoulderDetector"))
        {
            Debug.Log(gameObject.name + " has raised the shoulder.");
            if (this.tag == "r_shoulderIntruder")
                rebaCalculationScript.isRightShoulderTouching = true;
            else if (this.CompareTag("l_shoulderIntruder"))
                rebaCalculationScript.isLeftShoulderTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        // Check if the foot is colliding with the floor
        if (other.CompareTag("l_shoulderDetector") || other.CompareTag("r_shoulderDetector"))
        {
            Debug.Log(gameObject.name + " has loweered the shoulder.");
            if (this.tag == "r_shoulderIntruder")
                rebaCalculationScript.isRightShoulderTouching = false;
            else if (this.CompareTag("l_shoulderIntruder"))
                rebaCalculationScript.isLeftShoulderTouching = false;
        }
    }
}
