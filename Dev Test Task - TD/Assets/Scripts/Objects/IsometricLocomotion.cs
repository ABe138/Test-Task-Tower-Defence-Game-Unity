using UnityEngine;

//move objects without breaking z-order
public class IsometricLocomotion : MonoBehaviour {

    float currentElevation = 0;

    public static void SetPositionElevated(Transform transform, Vector2 position, float elevate)
    {
        Vector3 newPos = new Vector3(position.x, position.y + elevate, position.y);
        transform.position = newPos;
    }

    public static void SetPositionOnPlane(Transform transform, Vector2 position)
    {
        float yPos = position.y;
        Vector3 newPos = new Vector3(position.x, yPos, yPos);
        transform.position = newPos;
    }

    public void ResetElevation()
    {
        currentElevation = 0;
    }

    public void PlanarMovement(Vector2 movement, float elevate = 0)
    {
        Vector3 newPos = transform.position + (Vector3)movement;
        currentElevation += elevate;
        newPos.y += elevate;
        newPos.z = newPos.y - currentElevation;
        transform.position = newPos;
    }
}
