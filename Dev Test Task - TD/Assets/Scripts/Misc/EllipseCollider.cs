using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
public class EllipseCollider : MonoBehaviour {

    private float majorAxis = 1;
    private float minorAxis = 1;
    private static int iterations = 8;

    private PolygonCollider2D polygonCollider;

    private void Awake()
    {
        if(polygonCollider == null)
        {
            polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        }
        Rebuild();
    }

    public void Rebuild()
    {
        Vector2[] path = new Vector2[iterations];
        float angle;
        for (int i = 0; i < iterations; i++)
        {
            angle = 2 * Mathf.PI * ((float)i / (float)iterations);
            path[i] = new Vector3(Mathf.Cos(angle) * minorAxis, Mathf.Sin(angle) * majorAxis, 0);
        }
        polygonCollider.SetPath(0, path);
    }
}
