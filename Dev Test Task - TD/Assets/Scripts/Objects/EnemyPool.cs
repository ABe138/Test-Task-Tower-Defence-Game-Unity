public class EnemyPool : ObjectPooler<EnemyDummy> {

    public static EnemyPool Instance;

    private int activeCount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public override EnemyDummy GetObject()
    {
        EnemyDummy dummy = null;
        int count = poolable.Count;
        for (int i = 0; i < count; i++)
        {
            if (!poolable[i].gameObject.activeInHierarchy) dummy = poolable[i];
        }
        if (dummy == null)
        {
            Expand();
            dummy = poolable[0];
        }
        dummy.gameObject.SetActive(true);
        activeCount++;
        return dummy;
    }

    protected override void Expand()
    {
        EnemyDummy newEntity = Instantiate(dummy).GetComponent<EnemyDummy>();
        newEntity.transform.SetParent(root);
        newEntity.gameObject.SetActive(false);
        poolable.Insert(0, newEntity);
    }

    public void Return(EnemyDummy dummy)
    {
        dummy.gameObject.SetActive(false);
        activeCount--;
    }

    public int GetActiveCount()
    {
        return activeCount;
    }
}
