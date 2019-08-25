using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour {

    public static PoolSystem Instance
    {
        get; private set;
    }
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    [SerializeField]
    GameObject prefab;
    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private  void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var InstanceToAdd = Instantiate(prefab);
            InstanceToAdd.transform.SetParent(transform);
            AddtoPool(InstanceToAdd);

        }
    }
    public GameObject getFromPool()
    {
        if(availableObjects.Count ==0)
        {
            GrowPool();
        }
        var instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
    public void AddtoPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

     
}
