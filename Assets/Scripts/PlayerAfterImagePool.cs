using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton
public class PlayerAfterImagePool : MonoBehaviour
{
    public GameObject AfterImagePrefab;
    public int PoolCapacity;
    private Queue<GameObject> _availableObjects = new();

    public static PlayerAfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i <= PoolCapacity; i++)
        {
            var instanceToAdd = Instantiate(AfterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
            if (_availableObjects.Count >= PoolCapacity)
            {
                return;
            }
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        if (_availableObjects.Count >= PoolCapacity)
        {
            Destroy(instance);
            return;
        }
        _availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (_availableObjects.Count == 0)
        {
            GrowPool();
        }
        var instance = _availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
