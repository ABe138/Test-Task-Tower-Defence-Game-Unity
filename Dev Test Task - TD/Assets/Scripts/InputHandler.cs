using UnityEngine;

//crossplatform input
public class InputHandler : MonoBehaviour {

    public static InputHandler Instance;

    public static Camera MainCamera;

    public LayerMask raycastMask;

    [SerializeField]
    private CameraControls cameraControls;

    private Touch initialTouch;
    private Vector2 initialInputScreenPos;
    private Vector2 currentInputScreenPos;

    private ITappable tappable;
    private Collider2D touchedCol;
    private Collider2D compareCol;

    private bool justTouched = false;
    private bool touching = false;
    private bool justReleased = false;

    private bool allowInputs = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        MainCamera = Camera.main;
    }

    public void InputSwitch(bool enable)
    {
        allowInputs = enable;
    }

    private void Update()
    {
        if (!allowInputs)
        {
            return;
        }
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            initialInputScreenPos = Input.mousePosition;
            currentInputScreenPos = initialInputScreenPos;
            touchedCol = null;
            justTouched = true;
            touching = true;
        }
        else if (Input.GetMouseButton(0))
        {
            currentInputScreenPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touching = false;
            justReleased = true;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            initialTouch = Input.touches[0];
        }
        if(initialTouch.phase == TouchPhase.Began)
        {
            initialInputScreenPos = initialTouch.position;
            currentInputScreenPos = initialInputScreenPos;
            touchedCol = null;
            justTouched = true;
            touching = true;
        }
        else if(initialTouch.phase == TouchPhase.Moved)
        {
            currentInputScreenPos = initialTouch.position;
        }
        if(initialTouch.phase == TouchPhase.Ended && touching)
        {
            touching = false;
            justReleased = true;
        }
#endif
        if (justTouched)
        {
            justTouched = false;
            Ray ray = MainCamera.ScreenPointToRay(initialInputScreenPos);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, MainCamera.farClipPlane, raycastMask);
            if (hit.collider != null)
            {
                touchedCol = hit.collider;
                tappable = touchedCol.GetComponent<ITappable>();
                if (tappable != null)
                {
                    tappable.OnTouch();
                }
            }
        }
        if (justReleased)
        {
            justReleased = false;
            if (touchedCol != null)
            {
                Ray ray = MainCamera.ScreenPointToRay(currentInputScreenPos);
                compareCol = Physics2D.GetRayIntersection(ray, MainCamera.farClipPlane, raycastMask).collider;
                bool inBounds = false;
                if (compareCol != null)
                {
                    if (touchedCol == compareCol)
                    {
                        inBounds = true;
                    }
                }
                if (tappable != null)
                {
                    tappable.OnTap(inBounds);
                }
            }
        }
        if(touching)
        {
            cameraControls.UpdateTarget(MainCamera.ScreenToWorldPoint(initialInputScreenPos) - MainCamera.ScreenToWorldPoint(currentInputScreenPos));
        }
    }

}
