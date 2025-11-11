using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public float lockedYRotation;

    void LateUpdate()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y = lockedYRotation;
        transform.rotation = Quaternion.Euler(currentRotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
