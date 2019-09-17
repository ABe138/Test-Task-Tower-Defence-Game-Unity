using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [SerializeField]
    private int startingGold;
    [SerializeField]
    private int startingLives;

    [SerializeField]
    private List<SpawnWave> spawnWaves;

    private int goldRemaining;
    private int livesRemaining;

    private bool levelInProgress = false;

    private bool waitForNextWave = true;
    private float currentTime = 0;
    private float timeShift = 0;

    private int currentWave = 0;
    private int currentChunk = 0;

    private bool checkWinCondition = false;
    private bool defeat = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start ()
    {
        livesRemaining = startingLives;
        goldRemaining = startingGold;
        UICanvas.Instance.UpdateLivesValue(livesRemaining);
        UICanvas.Instance.UpdateGoldValue(goldRemaining);
        levelInProgress = true;
    }
	
	void Update ()
    {
        if (checkWinCondition) CheckWinCondition();
        if (!levelInProgress) return;
        currentTime += Time.deltaTime;
        if (waitForNextWave)
        {
            if(currentTime > spawnWaves[currentWave].waveDelay)
            {
                UICanvas.Instance.UpdateWaveText(currentWave + 1);
                waitForNextWave = false;
                currentTime = 0;
            }
            return;
        }
        if (currentTime > spawnWaves[currentWave].spawnChunks[currentChunk].spawnDelay + timeShift)
        {
            SpawnChunk chunk = spawnWaves[currentWave].spawnChunks[currentChunk];
            for(int i = 0; i < chunk.spawnAmount; i++)
            {
                chunk.spawnAt.SpawnEnemy(spawnWaves[currentWave].spawnChunks[currentChunk].spawnEnemy);
            }
            timeShift += chunk.spawnDelay;
            currentChunk++;
            if (currentChunk >= spawnWaves[currentWave].spawnChunks.Count)
            {
                waitForNextWave = true;
                currentWave++;
                currentTime = 0;
                timeShift = 0;
                currentChunk = 0;
                if (currentWave >= spawnWaves.Count)
                {
                    checkWinCondition = true;
                    levelInProgress = false;
                    currentWave = 0;
                }
            }
        }
    }

    private void CheckWinCondition()
    {
        if(!defeat)
        {
            if(EnemyPool.Instance.GetActiveCount() == 0)
            {
                InputHandler.Instance.InputSwitch(false);
                UICanvas.Instance.UpdateResultsText("Victory!");
            }
        }
    }

    public void AddGold(int amount)
    {
        goldRemaining += amount;
        UICanvas.Instance.UpdateGoldValue(goldRemaining);
    }

    public bool WithdrawGold(int amount)
    {
        if(amount <= goldRemaining)
        {
            goldRemaining -= amount;
            UICanvas.Instance.UpdateGoldValue(goldRemaining);
            return true;
        }
        return false;
    }

    public void DealDamage(int amount)
    {
        livesRemaining -= amount;
        if (livesRemaining < 0)
        {
            livesRemaining = 0;
            defeat = true;
            InputHandler.Instance.InputSwitch(false);
            UICanvas.Instance.UpdateResultsText("Defeat..");
        }
        UICanvas.Instance.UpdateLivesValue(livesRemaining);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    [System.Serializable]
    protected class SpawnWave
    {
        public float waveDelay;
        public List<SpawnChunk> spawnChunks;
    }

    [System.Serializable]
    protected class SpawnChunk
    {
        public float spawnDelay;
        public Enemy spawnEnemy;
        public int spawnAmount;
        public SpawnPoint spawnAt;
    }
}