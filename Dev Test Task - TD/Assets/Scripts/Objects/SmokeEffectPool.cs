public class SmokeEffectPool : ObjectPooler<EffectLifetime>
{
    public static SmokeEffectPool Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public override EffectLifetime GetObject()
    {
        EffectLifetime dummy = null;
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
        return dummy;
    }

    protected override void Expand()
    {
        EffectLifetime newEntity = Instantiate(dummy).GetComponent<EffectLifetime>();
        newEntity.transform.SetParent(root);
        newEntity.gameObject.SetActive(false);
        poolable.Insert(0, newEntity);
    }
}
