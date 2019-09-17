using UnityEngine;

//initial waypoint
public class SpawnPoint : NavEntity {

    public void SpawnEnemy (Enemy enemy)
    {
        EnemyDummy dummy = EnemyPool.Instance.GetObject();
        Vector2 randomOffset = new Vector2(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f)) * radius;
        dummy.Setup(enemy, gameObject.transform.position, randomOffset);
        dummy.SetDestination(nextNavEntity);
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float squareDim = 0.25f;
        Gizmos.DrawLine(gameObject.transform.position + new Vector3(-squareDim, squareDim, 0), gameObject.transform.position + new Vector3(squareDim, squareDim, 0));
        Gizmos.DrawLine(gameObject.transform.position + new Vector3(-squareDim, squareDim, 0), gameObject.transform.position + new Vector3(squareDim, -squareDim, 0));
        Gizmos.DrawLine(gameObject.transform.position + new Vector3(squareDim, -squareDim, 0), gameObject.transform.position + new Vector3(-squareDim, -squareDim, 0));
        if (nextNavEntity == null)
        {
            return;
        }
        Gizmos.DrawLine(gameObject.transform.position, nextNavEntity.gameObject.transform.position);
    }

}
