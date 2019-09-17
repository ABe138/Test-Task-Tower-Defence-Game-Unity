using UnityEngine;

[CreateAssetMenu(fileName = "new_enemy", menuName = "Custom Assets/Enemy")]
public class Enemy : ScriptableObject {

    [SerializeField]
    private int health;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float scale = 1;
    [SerializeField]
    private int minReward;
    [SerializeField]
    private int maxReward;
    [SerializeField]
    private SpriteSheet spriteSheet;

    public int Health { get { return health; } }
    public float Speed { get { return speed; } }
    public int Damage { get { return damage; } }
    public float Scale { get { return scale; } }
    public SpriteSheet SpriteSheet { get { return spriteSheet; } }

    public int RollReward()
    {
        return Random.Range(minReward, maxReward + 1);
    }

    
}
