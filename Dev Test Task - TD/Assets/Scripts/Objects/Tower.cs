using UnityEngine;

[CreateAssetMenu(fileName = "new_tower", menuName = "Custom Assets/Tower")]
public class Tower : ScriptableObject {

    [SerializeField]
    private int price;
    [SerializeField]
    private float range;
    [SerializeField]
    private float shootInterval;
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool splash;    //if it does not splash, it just hits
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private Tower[] upgrades;
    [SerializeField]
    private Vector2 shootingPoint;  //offset for projectile initial placement
    [SerializeField]
    private float blastScale;   //smoke effect scale on attack
    [SerializeField]
    private Projectile projectile;
    
    public int Price { get { return price; } }
    public float Range { get { return range; } }
    public float ShootInterval { get { return shootInterval; } }
    public int Damage { get { return damage; } }
    public bool Splash { get { return splash; } }
    public Sprite Sprite { get { return sprite; } }
    public Sprite Icon { get { return icon; } }
    public Tower[] Upgrades { get { return upgrades; } }
    public Vector2 ShootingPoint { get { return shootingPoint; } }
    public float BlastScale { get { return blastScale; } }
    public Projectile Projectile { get { return projectile; } }
}
