using UnityEngine;

//dummy for enemy unit
public class EnemyDummy : MonoBehaviour {

    private float speed;
    private SpriteSheet spriteSheet = null;
    private int health = 10;
    private int damage = 1;
    private int reward = 0;

    [SerializeField]
    private Animator dummyAnim;
    [SerializeField]
    private BoxCollider2D dummyCol;
    [SerializeField]
    private SpriteRenderer dummySprite;
    [SerializeField]
    private IsometricLocomotion locomotion;

    private Vector2 destination;
    private Vector2 moveOffset;

    private NavEntity currentNavEntity;

    private static float ReachThreshold = 0.03f;

    private float distanceRemaining;

    private bool dying = false;
    private float dyingTimer = 1.0f;
    private float currentDyingTime = 0;

    private void OnEnable()
    {
        dummyAnim.Update(Random.Range(0.0f, 1.0f));
        //ResetingDying OnDisable does not always reenables collider
        ResetDying();
    }

    private void Update()
    {
        //Added just for fun
        if(dying)
        {
            currentDyingTime += Time.deltaTime;
            Vector2 offset = (destination - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(offset.y, offset.x) + 2 * Mathf.PI * currentDyingTime / dyingTimer;
            if (spriteSheet != null) dummySprite.sprite = spriteSheet.GetSprite(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
            if (currentDyingTime > dyingTimer) ReturnToPool();
            return;
        }
        //moving object here
        Vector2 moveDir = destination - (Vector2)transform.position;
        float distance = moveDir.magnitude;
        moveDir = moveDir.normalized;
        if(spriteSheet != null) dummySprite.sprite = spriteSheet.GetSprite(moveDir);
        locomotion.PlanarMovement(moveDir * Time.deltaTime * speed);
        if (distance < ReachThreshold)
        {
            SetDestination(currentNavEntity.GetNext());
        }
        CalculateRemainingDistance();
    }

    public void Setup(Enemy enemy, Vector3 spawnPosition, Vector2 spawnOffset)
    {
        dying = false;
        speed = enemy.Speed;
        spriteSheet = enemy.SpriteSheet;
        health = enemy.Health;
        damage = enemy.Damage;
        transform.localScale = Vector3.one * enemy.Scale;
        reward = enemy.RollReward();
        moveOffset = spawnOffset;
        locomotion.ResetElevation();
        IsometricLocomotion.SetPositionOnPlane(transform, spawnPosition + (Vector3)moveOffset);
    }

    public void SetDestination(NavEntity navEntity)
    {
        if (navEntity == null)
        {
            GameManager.Instance.DealDamage(damage);
            ReturnToPool();
            return;
        }
        currentNavEntity = navEntity;
        destination = (Vector2)navEntity.transform.position + moveOffset * navEntity.GetRadius();
    }

    private void CalculateRemainingDistance()
    {
        if(currentNavEntity == null)
        {
            distanceRemaining = Mathf.Infinity;
            return;
        }
        float distance = 0;
        distance += (destination - (Vector2)transform.position).magnitude;
        NavEntity navEntity = currentNavEntity;
        while(navEntity.GetNext() != null)
        {
            distance += (navEntity.GetNext().transform.position - navEntity.transform.position).magnitude;
            navEntity = navEntity.GetNext();
        }
        distanceRemaining = distance;
    }

    public float GetGoalDistance()
    {
        return distanceRemaining;
    }

    public void TakeDamage(int damage)
    {
        if (dying) return;
        health -= damage;
        if (health <= 0) Die();
    }

    private void ResetDying()
    {
        dying = false;
        currentDyingTime = 0;
        dummyCol.enabled = true;
    }

    private void Die()
    {
        dying = true;
        dummyCol.enabled = false;
        GameManager.Instance.AddGold(reward);
    }

    private void ReturnToPool()
    {
        EnemyPool.Instance.Return(this);
    }
}
