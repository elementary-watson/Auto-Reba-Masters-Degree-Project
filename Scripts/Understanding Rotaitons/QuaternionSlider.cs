using UnityEngine;

public class QuaternionSlider : MonoBehaviour
{
    [Range(-180f, 180f)]
    public float x;
    [Range(-180f, 180f)]
    public float y;
    [Range(-180f, 180f)]
    public float z;
    [Range(-180f, 180f)]
    public float w;

    public GameObject cube;

    void Update()
    {
        // Normalize the quaternion to ensure it's valid
        Quaternion rotation = new Quaternion(x, y, z, w);
        rotation = Quaternion.Normalize(rotation);

        // Apply the rotation to the GameObject
        cube.transform.rotation = rotation;

        Debug.Log("PLAIN CubeRed: " + rotation);
        Debug.Log("THIS 0 CubeRed: " + rotation[0]);
        Debug.Log("THIS 1 CubeRed: " + rotation[1]);
        Debug.Log("THIS 2 CubeRed: " + rotation[2]);
        Debug.Log("THIS 3 CubeRed: " + rotation[3]);

        Debug.Log("W CubeRed: " + rotation.w);
        Debug.Log("x CubeRed: " + rotation.x);
        Debug.Log("y CubeRed: " + rotation.y);
        Debug.Log("z CubeRed: " + rotation.z);
        Debug.Log("eulerAngles CubeRed: " + rotation.eulerAngles);
        Debug.Log("normalized CubeRed: " + rotation.normalized);
        Debug.Log("Invers CubeRed: " + Quaternion.Inverse(cube.transform.rotation));
    }
}
