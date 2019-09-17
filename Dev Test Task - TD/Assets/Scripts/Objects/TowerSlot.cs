using UnityEngine;

//slot to place tower, should be placed in the scene manually
public class TowerSlot : MonoBehaviour {

    [SerializeField]
    private Tower currentTower = null;
    public Tower ContainsTower { get { return currentTower; } }

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private TowerShooter towerShooter;
    [SerializeField]
    private SpriteRenderer rangeSpriteRenderer;

    private bool operational = false;

    private int sellValue = 0;
    public int SellValue { get { return sellValue; } private set { sellValue = value; } }

    private void Start()
    {
        IsometricLocomotion.SetPositionOnPlane(transform, transform.position);
        if (currentTower != null) UpgradeTo(currentTower);
        else ResetSlot();
    }

    private void Update()
    {
        if (!operational) return;
        towerShooter.Tick();
    }

    private void ResetSlot()
    {
        operational = false;
        currentTower = TowerConstants.Instance.DefaultTower;
        spriteRenderer.sprite = currentTower.Sprite;
        sellValue = 0;
    }

    public void UpgradeTo(Tower tower)
    {
        operational = true;
        spriteRenderer.sprite = tower.Sprite;
        sellValue += tower.Price;
        towerShooter.UpdateShooter(tower);
        currentTower = tower;
    }

    public void SellTower()
    {
        GameManager.Instance.AddGold(TowerConstants.Instance.GetSellPrice(sellValue));
        ResetSlot();
    }

    public bool Sellable()
    {
        return operational;
    }

    public void EnableRangeDisplay(bool enable)
    {
        if (operational) rangeSpriteRenderer.enabled = enable;
        else rangeSpriteRenderer.enabled = false;
    }
}
