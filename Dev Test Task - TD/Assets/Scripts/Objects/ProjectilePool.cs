public class ProjectilePool : ObjectPooler<ProjectileDummy>
{
    public static ProjectilePool Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public override ProjectileDummy GetObject()
    {
        ProjectileDummy dummy = null;
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
        ProjectileDummy newEntity = Instantiate(dummy).GetComponent<ProjectileDummy>();
        newEntity.transform.SetParent(root);
        newEntity.gameObject.SetActive(false);
        poolable.Insert(0, newEntity);
    }
}
