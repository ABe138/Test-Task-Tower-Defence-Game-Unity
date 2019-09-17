using UnityEngine;
using TMPro;

public class UICanvas : MonoBehaviour {

    public static UICanvas Instance;

    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private TextMeshProUGUI resultsText;
    [SerializeField]
    private GameObject resultsDialogBox;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateLivesValue(int value)
    {
        livesText.SetText("{0}", value);
    }

    public void UpdateGoldValue(int value)
    {
        goldText.SetText("{0}", value);
    }

    public void UpdateWaveText(int value)
    {
        waveText.SetText("Wave: {0}", value);
    }

    public void UpdateResultsText(string text)
    {
        resultsText.SetText(text);
        resultsDialogBox.SetActive(true);
    }
}
