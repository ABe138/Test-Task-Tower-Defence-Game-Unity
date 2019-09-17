public class TowerUISell : TowerUIButton
{
    public override void OnTouch()
    {
        TowerUI.Instance.ReportSellSelection();
    }
}
