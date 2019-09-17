using System.Collections.Generic;
using UnityEngine;

//probably unnesessary generics, can't make generic monobehaviour pooler anyways
public abstract class ObjectPooler<T> : MonoBehaviour {

    [SerializeField]
    protected T dummy;
    [SerializeField]
    protected Transform root;
    protected List<T> poolable = new List<T>();

    public abstract T GetObject();

    protected abstract void Expand();
}
