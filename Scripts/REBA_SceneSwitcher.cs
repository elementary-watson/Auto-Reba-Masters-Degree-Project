using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class REBA_SceneSwitcher : MonoBehaviour
{
    public void SwitchToStudy()
    {
        SceneManager.LoadScene("REBA_Study Experiment Scene");
    }

    public void SwitchToQuestionnaire()
    {
        SceneManager.LoadScene("Question Scene");
    }
}
