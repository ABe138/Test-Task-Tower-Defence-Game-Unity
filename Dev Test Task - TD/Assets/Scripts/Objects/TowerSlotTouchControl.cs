using UnityEngine;

public class TowerSlotTouchControl : MonoBehaviour, ITappable {

    [SerializeField]
    private TowerSlot myTowerSlot;

    public void OnTap(bool inBounds)
    {
        if(inBounds)
        {
            TowerUI.Instance.ShowTowerUI(myTowerSlot);
        }
    }

    public void OnTouch()
    {

    }

}
