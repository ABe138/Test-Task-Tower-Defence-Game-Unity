using UnityEngine;

//adapts camera size depending on screen ratio. Handles camera movement.
public class CameraControls : MonoBehaviour {

    public int ScreenWorldWidth = 12;
    public int ScreenWorldHeight = 9;

    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private float cameraTopLimit;
    [SerializeField]
    private float cameraBotLimit;
    [SerializeField]
    private float smoothTime = 0.2f;
    [SerializeField]
    private Vector3 velocity = Vector3.zero;

    private Vector3 target;

    private void Awake()
    {
        float screenWHRatio = (float)Screen.width / Screen.height;
        gameCamera.orthographicSize = ScreenWorldWidth / screenWHRatio / 2;
        cameraTopLimit = (ScreenWorldHeight - (gameCamera.orthographicSize * 2)) / 2;
        cameraBotLimit = (-1) * cameraTopLimit;
        target = new Vector3(0, 0, -10);
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }

    public void UpdateTarget(Vector3 dragDirection)
    {
        float targetY = transform.position.y + dragDirection.y;
        if (targetY < cameraBotLimit) targetY = cameraBotLimit;
        if (targetY > cameraTopLimit) targetY = cameraTopLimit;
        target = new Vector3(transform.position.x, targetY, transform.position.z);
    }

}
