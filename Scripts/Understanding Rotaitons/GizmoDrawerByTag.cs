using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GizmoDrawerByTag : MonoBehaviour
{
    // Change this tag to whatever tag you're using for the objects you want to visualize
    private const string tag = "DrawGizmo";

    // Set a scale for the gizmo lines
    [SerializeField]
    private float gizmoLength = 0.5f;
    [SerializeField]
    private float gizmoSize = 0.1f;

    private void OnDrawGizmos()
    {
        // Get all objects with the specified tag
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objects)
        {
            // Draw a small red sphere at each object's position
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(obj.transform.position, gizmoSize);

            // Draw a line for each axis
            Gizmos.color = Color.red;
            Gizmos.DrawRay(obj.transform.position, obj.transform.right * gizmoLength);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(obj.transform.position, obj.transform.up * gizmoLength);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(obj.transform.position, obj.transform.forward * gizmoLength);
        }
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
