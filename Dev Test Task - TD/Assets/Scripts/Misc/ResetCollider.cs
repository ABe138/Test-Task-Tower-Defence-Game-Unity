using UnityEngine;

//some place to click to deselect
public class ResetCollider : MonoBehaviour, ITappable
{
    public void OnTap(bool inBounds = false)
    {
        TowerUI.Instance.HideTowerUI();
    }

    public void OnTouch()
    {

    }
}
