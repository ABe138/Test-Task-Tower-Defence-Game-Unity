using UnityEngine;

//waypoint
public class NavEntity : MonoBehaviour {

    [SerializeField]
    protected NavEntity nextNavEntity;
    [SerializeField]
    protected float radius = 0.5f;

    public NavEntity GetNext()
    {
        return nextNavEntity;
    }

    public float GetRadius()
    {
        return radius;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 lineOffset = Vector2.one * 0.25f;
        Gizmos.DrawLine(gameObject.transform.position - (Vector3)lineOffset, gameObject.transform.position + (Vector3)lineOffset);
        lineOffset.y *= -1;
        Gizmos.DrawLine(gameObject.transform.position - (Vector3)lineOffset, gameObject.transform.position + (Vector3)lineOffset);
        Gizmos.DrawLine(gameObject.transform.position - Vector3.right * radius, gameObject.transform.position + Vector3.right * radius);
        Gizmos.DrawLine(gameObject.transform.position - Vector3.up * radius, gameObject.transform.position + Vector3.up * radius);
        if (nextNavEntity == null)
        {
            return;
        }
        Gizmos.DrawLine(gameObject.transform.position, nextNavEntity.gameObject.transform.position);
    }
}
