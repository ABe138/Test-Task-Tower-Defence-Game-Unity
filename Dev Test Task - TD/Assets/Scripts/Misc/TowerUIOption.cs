using UnityEngine;

public class TowerUIOption : TowerUIButton {

    private int optionId = 0;

    public void InitializeOption(int id)
    {
        optionId = id;
    }

    public void SetIcon(Sprite icon)
    {
        this.icon = icon;
        ResetIcon();
    }

    public override void OnTouch()
    {
        TowerUI.Instance.ReportOptionSelection(optionId);
    }
}
