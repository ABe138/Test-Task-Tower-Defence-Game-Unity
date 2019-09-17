using UnityEngine;

[System.Serializable]
public class Projectile {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float trajectoryPeak;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private float scale;
    [SerializeField]
    private float impactScale;  //on impact effect scale + splash scale

    public float Speed { get { return speed; } }
    public float TrajectoryPeak { get { return trajectoryPeak; } }
    public Sprite Sprite { get { return sprite; } }
    public float Scale { get { return scale; } }
    public float ImpactScale { get { return impactScale; } }
}
