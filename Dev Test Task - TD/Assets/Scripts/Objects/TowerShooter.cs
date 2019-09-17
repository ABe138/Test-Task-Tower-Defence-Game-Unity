using UnityEngine;

public class TowerShooter : MonoBehaviour {

    [SerializeField]
    private TargetSeeker targetSeeker;

    private float currentTime = 0;
    private float interval = 1;
    private bool ready = true;
    private Projectile projectile;
    private Vector2 shootingPoint;
    private float blastScale;
    private int damage;
    private bool splash;

    public void UpdateShooter(Tower tower)
    {
        ready = true;
        currentTime = 0;
        interval = tower.ShootInterval;
        projectile = tower.Projectile;
        targetSeeker.UpdateSeeker(tower.Range);
        shootingPoint = tower.ShootingPoint;
        blastScale = tower.BlastScale;
        damage = tower.Damage;
        splash = tower.Splash;
    }

    public void Tick()
    {
        if(ready)
        {
            EnemyDummy target = targetSeeker.PickTarget();
            if(target != null)
            {
                Shoot(target);
                ready = false;
            }
            else
            {
                return;
            }
        }
        currentTime += Time.deltaTime;
        if(currentTime > interval)
        {
            currentTime = 0;
            ready = true;
        }
    }

    private void Shoot(EnemyDummy target)
    {
        ProjectileDummy dummy = ProjectilePool.Instance.GetObject();
        dummy.Setup(projectile, gameObject.transform.position + (Vector3)shootingPoint, target, damage, splash);
        Transform smokeEffectTransform = SmokeEffectPool.Instance.GetObject().transform;
        smokeEffectTransform.localScale = Vector3.one * blastScale;
        IsometricLocomotion.SetPositionElevated(smokeEffectTransform, (Vector2)gameObject.transform.position + new Vector2(shootingPoint.x, 0), shootingPoint.y);
    }
}
