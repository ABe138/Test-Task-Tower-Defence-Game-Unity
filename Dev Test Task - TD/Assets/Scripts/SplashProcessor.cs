using UnityEngine;
using System.Collections.Generic;

public class SplashProcessor : MonoBehaviour {

    public static SplashProcessor Instance;

    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private int nonAllocSize = 10;

    private Collider2D[] splashables;
    private List<SplashRequest> pendingSplashRequests = new List<SplashRequest>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        splashables = new Collider2D[nonAllocSize];
    }

    private void FixedUpdate()
    {
        int requestsCount = pendingSplashRequests.Count;
        if (requestsCount > 0)
        {
            for (int i = 0; i < requestsCount; i++)
            {
                SplashRequest splashRequest = pendingSplashRequests[i];
                int captured = Physics2D.OverlapAreaNonAlloc(splashRequest.center - splashRequest.size, splashRequest.center + splashRequest.size, splashables, enemyLayer);
                for(int j = 0; j < captured; j++)
                {
                    if(splashables[j] != null)
                    {
                        splashables[j].GetComponent<EnemyDummy>().TakeDamage(splashRequest.damage);
                    }
                }
            }
            pendingSplashRequests.Clear();
        }
    }

    public void SplashArea(Vector2 center, Vector2 size, int damage)
    {
        pendingSplashRequests.Add(new SplashRequest(center, size, damage));
    }

    protected class SplashRequest
    {
        public Vector2 center;
        public Vector2 size;
        public int damage;

        public SplashRequest(Vector2 center, Vector2 size, int damage)
        {
            this.center = center;
            this.size = size;
            this.damage = damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < pendingSplashRequests.Count; i++)
        {
            Gizmos.DrawCube(pendingSplashRequests[i].center, pendingSplashRequests[i].size);
        }
    }

}
