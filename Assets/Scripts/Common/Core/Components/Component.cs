using UnityEngine;

[System.Serializable]
public class CoreComponent : MonoBehaviour
{
    protected Core core;

    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();
    }
    protected virtual void OnDrawGizmos() { }
}