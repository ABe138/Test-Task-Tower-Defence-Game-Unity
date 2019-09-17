using UnityEngine;
using System.Collections.Generic;

//tower targets container
public class TargetSeeker : MonoBehaviour {

    private List<EnemyDummy> targets = new List<EnemyDummy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyDummy something = collision.GetComponent<EnemyDummy>();
        if(something != null && !targets.Contains(something))
        {
            targets.Add(something);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyDummy something = collision.GetComponent<EnemyDummy>();
        if (something != null && targets.Contains(something))
        {
            targets.Remove(something);
        }
    }
    
    public void UpdateSeeker (float range)
    {
        transform.localScale = new Vector3(range, range * 2 / 3, 1);
    }

    public EnemyDummy PickTarget()
    {
        float minDistance = Mathf.Infinity;
        EnemyDummy picked = null;
        for(int i = targets.Count - 1; i > -1; i--)
        {
            EnemyDummy target = targets[i];
            if (!target.gameObject.activeInHierarchy)
            {
                targets.RemoveAt(i);
                continue;
            }
            if (target.GetGoalDistance() < minDistance)
            {
                minDistance = target.GetGoalDistance();
                picked = target;
            }
        }
        return picked;
    }
}
