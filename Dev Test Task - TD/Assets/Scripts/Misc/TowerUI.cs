using UnityEngine;


public class TowerUI : MonoBehaviour {

    public static TowerUI Instance;

    private TowerSlot currentSlot;

    private int lastOptionSelected = -1;
    private bool requireSellConfirmation = false;

    [SerializeField]
    private int maxOptionsCount = 4;
    [SerializeField]
    private GameObject towerUIOptionButtonPrefab;
    [SerializeField]
    private TowerUISell towerSellOption;
    [SerializeField]
    private Sprite confirmationCheckSprite;
    public Sprite ConfirmCheckSprite { get { return confirmationCheckSprite; } private set { confirmationCheckSprite = value; } }
    private TowerUIOption[] towerUIOptions;

    private float upgradeOptionsShift = -90.0f;
    private float upgradeOptionsArc = 360.0f;
    private float upgradeCircleRadius = 0.9f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        towerUIOptions = new TowerUIOption[maxOptionsCount];
        for (int i = 0; i < maxOptionsCount; i++)
        {
            towerUIOptions[i] = Instantiate(towerUIOptionButtonPrefab).GetComponent<TowerUIOption>();
            towerUIOptions[i].InitializeOption(i);
            towerUIOptions[i].transform.parent = gameObject.transform;
            towerUIOptions[i].transform.localPosition = Vector3.zero;
        }
        HideTowerUI();
    }

    public void ShowTowerUI (TowerSlot towerSlot)
    {
        if (currentSlot != null) currentSlot.EnableRangeDisplay(false);
        gameObject.SetActive(true);
        currentSlot = towerSlot;
        currentSlot.EnableRangeDisplay(true);
        transform.position = currentSlot.transform.position;
        lastOptionSelected = -1;
        PlaceIcons();
    }

    public void HideTowerUI()
    {
        if (currentSlot != null) currentSlot.EnableRangeDisplay(false);
        gameObject.SetActive(false);
    }
    
    private void PlaceIcons()
    {
        Tower[] upgrades = currentSlot.ContainsTower.Upgrades;
        int optionsCount = Mathf.Min(towerUIOptions.Length, upgrades.Length);
        float arcInRad = upgradeOptionsArc * Mathf.Deg2Rad;
        float shiftInRad = upgradeOptionsShift * Mathf.Deg2Rad;
        float angularSpacing = arcInRad / (optionsCount + 1);
        for (int i = 0; i < optionsCount; i++)
        {
            float angle = arcInRad - angularSpacing * (i + 1.0f) + shiftInRad;
            towerUIOptions[i].SetIcon(upgrades[i].Icon);
            towerUIOptions[i].SetPrice(upgrades[i].Price);
            towerUIOptions[i].transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * upgradeCircleRadius + transform.position;
            towerUIOptions[i].gameObject.SetActive(true);
        }
        for (int i = optionsCount; i < towerUIOptions.Length; i++)
        {
            towerUIOptions[i].gameObject.SetActive(false);
        }
        towerSellOption.gameObject.SetActive(currentSlot.Sellable());
        towerSellOption.SetPrice(TowerConstants.Instance.GetSellPrice(currentSlot.SellValue));
        ResetIcons();
    }

    private void ResetIcons()
    {
        for(int i = 0; i < towerUIOptions.Length; i++)
        {
            towerUIOptions[i].ResetIcon();
        }
        towerSellOption.ResetIcon();
        lastOptionSelected = -1;
        requireSellConfirmation = false;
    }

    public void ReportOptionSelection(int optionId)
    {
        if(lastOptionSelected == optionId)
        {
            if(GameManager.Instance.WithdrawGold(currentSlot.ContainsTower.Upgrades[optionId].Price))
            {
                currentSlot.UpgradeTo(currentSlot.ContainsTower.Upgrades[optionId]);
                HideTowerUI();
                return;
            }
            return;
        }
        ResetIcons();
        towerUIOptions[optionId].Select();
        lastOptionSelected = optionId;
    }

    public void ReportSellSelection()
    {
        if(requireSellConfirmation)
        {
            currentSlot.SellTower();
            HideTowerUI();
            return;
        }
        ResetIcons();
        towerSellOption.Select();
        requireSellConfirmation = true;
    }

}
