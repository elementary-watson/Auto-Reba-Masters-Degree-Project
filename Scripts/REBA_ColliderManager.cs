using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_ColliderManager : MonoBehaviour
{
    public REBA_Calculation rebaCalculationScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("game_floor"))
        {
            if (this.CompareTag("r_footCollider"))
            {
                rebaCalculationScript.isRightFootTouching = true;
            }
            else if (this.CompareTag("l_footCollider"))
            {
                rebaCalculationScript.isLeftFootTouching = true;
            }
        }
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("game_floor"))
        {
            if (this.CompareTag("r_footCollider"))
            {
                rebaCalculationScript.isRightFootTouching = false;
            }
            else if (this.CompareTag("l_footCollider"))
            {
                rebaCalculationScript.isLeftFootTouching = false;
            }
        }
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
    }
}
