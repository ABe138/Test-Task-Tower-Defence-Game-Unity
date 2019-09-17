using UnityEngine;

//simple fx destroyer
public class EffectLifetime : MonoBehaviour {

    [SerializeField]
    private float lifeTime = 0.3f;

    private float currentTime = 0;

    private void OnEnable()
    {
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime) gameObject.SetActive(false);
    }
}
