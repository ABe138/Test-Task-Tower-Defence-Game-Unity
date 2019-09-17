using UnityEngine;

public class TowerConstants : MonoBehaviour {

    public static TowerConstants Instance;

    [SerializeField]
    private Tower defaultTower;
    public Tower DefaultTower { get { return defaultTower; } private set { defaultTower = value; } }

    [SerializeField]
    [Range(0,1)]
    private float sellTax;

    public int GetSellPrice(int initialPrice)
    {
        return Mathf.FloorToInt(initialPrice * (1 - sellTax));
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
