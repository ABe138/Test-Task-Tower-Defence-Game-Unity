using UnityEngine;

//dummy for projectile
public class ProjectileDummy : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private IsometricLocomotion locomotion;

    //mb these should be rearranged somehow
    private EnemyDummy target;
    private float speed;
    private float trajectory;
    private float impactScale;
    private int damage;
    private bool splash;
    private float distanceTraveled = 0;
    private float distanceToTarget;
    private Vector2 destination;
    Vector2 moveDirection;
    

    public void Setup(Projectile projectile, Vector3 projectileSpawnPosition, EnemyDummy target, int projectileDamage, bool splash)
    {
        //yeah
        this.target = target;
        damage = projectileDamage;
        speed = projectile.Speed;
        this.splash = splash;
        trajectory = projectile.TrajectoryPeak;
        spriteRenderer.sprite = projectile.Sprite;
        transform.localScale = Vector3.one * projectile.Scale;
        impactScale = projectile.ImpactScale;
        destination = target.transform.position;
        locomotion.ResetElevation();
        IsometricLocomotion.SetPositionOnPlane(transform, projectileSpawnPosition);
        distanceToTarget = ((Vector2)transform.position - destination).magnitude;
        moveDirection = (destination - (Vector2)transform.position).normalized;
        distanceTraveled = 0;
    }

    private void Update()
    {
        float moveDistance = Time.deltaTime * speed;
        distanceTraveled += Time.deltaTime * speed;
        float traveledPercent = distanceTraveled / distanceToTarget;
        if (traveledPercent >= 1)
        {
            Transform smokeTransform = SmokeEffectPool.Instance.GetObject().transform;
            IsometricLocomotion.SetPositionOnPlane(smokeTransform, transform.position);
            smokeTransform.localScale = Vector3.one * impactScale;
            //probably should calculate isometric ranges somewhere else
            if (splash) SplashProcessor.Instance.SplashArea(transform.position, new Vector2(impactScale, impactScale * 2 / 3), damage);
            else target.TakeDamage(damage);
            gameObject.SetActive(false);
            return;
        }
        locomotion.PlanarMovement(moveDirection * moveDistance, (trajectory - 2 * trajectory * traveledPercent) * Time.deltaTime);
    }
}
