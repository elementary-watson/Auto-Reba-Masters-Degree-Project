using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))  // Press P key to capture screenshot
        {
            ScreenCapture.CaptureScreenshot("SomeLevel.png", 4);
            Debug.Log("Capture!");
        }
    }
}
