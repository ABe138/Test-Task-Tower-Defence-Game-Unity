using UnityEngine;

[ExecuteInEditMode]
public class SceneUITextLayerFix : MonoBehaviour {

    [SerializeField]
    private string Layer = "SceneUI";
    [SerializeField]
    private int setOrder = 3;

    private void Awake()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.sortingLayerName = Layer;
        renderer.sortingOrder = setOrder;
    }
}
