using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_FootCollider : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the foot is colliding with the floor
        if (other.CompareTag("game_floor"))
        {
            Debug.Log(gameObject.name + " has entered the floor.");
            if(this.tag == "r_footCollider") 
                rebaCalculationScript.isRightFootTouching = true;
            else if (this.CompareTag("l_footCollider"))            
                rebaCalculationScript.isLeftFootTouching = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the foot is exiting the floor's collider
        if (other.CompareTag("game_floor"))
        {
            Debug.Log(gameObject.name + " has exited the floor.");
            if (this.tag == "r_footCollider")
                rebaCalculationScript.isRightFootTouching = false;
            else if (this.CompareTag("l_footCollider"))
                rebaCalculationScript.isLeftFootTouching = false;
        }
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

