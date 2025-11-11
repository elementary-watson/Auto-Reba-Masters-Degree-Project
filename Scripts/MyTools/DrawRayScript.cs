using UnityEngine;

[ExecuteInEditMode]
public class DrawRayScript : MonoBehaviour
{
    public float rayLength = 1.0f;
    public Direction rayDirection = Direction.Up;
    public Color rayColor = Color.red;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Backward
    }

    private void Update()
    {
        Vector3 direction;

        switch (rayDirection)
        {
            case Direction.Up:
                direction = transform.up;
                break;
            case Direction.Down:
                direction = -transform.up;
                break;
            case Direction.Left:
                direction = -transform.right;
                break;
            case Direction.Right:
                direction = transform.right;
                break;
            case Direction.Forward:
                direction = transform.forward;
                break;
            case Direction.Backward:
                direction = -transform.forward;
                break;
            default:
                direction = transform.up;
                break;
        }

        Debug.DrawRay(transform.position, direction * rayLength, rayColor);
    }
}
