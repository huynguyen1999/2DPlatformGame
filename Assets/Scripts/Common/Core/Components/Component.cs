using UnityEngine;

[System.Serializable]
public class CoreComponent : MonoBehaviour
{
    protected Core core;

    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();
    }
    public virtual void Update() { }
    public virtual void FixedUpdate(){}
    protected virtual void OnDrawGizmos() { }
}