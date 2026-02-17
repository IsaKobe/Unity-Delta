using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool
{
    [SerializeField] GameObject Prefab;
    [SerializeField] List<GameObject> active;
    [SerializeField] List<GameObject> free;


    public ObjectPool(GameObject prefab, int InitSize)
    {
        Prefab = prefab;
        for (int i = 0; i < InitSize; i++)
        {
            CreatePrefab();
        }
    }

    void CreatePrefab()
    {
        GameObject pref = GameObject.Instantiate(Prefab);
        free.Add(pref);
        pref.SetActive(false);
        Projectile proj = pref.GetComponent<Projectile>();
        proj.returnToPool = ReturnObject;
    }

    public GameObject GetObject(Vector3 pos)
    {
        if(free.Count == 0)
        {
            CreatePrefab();
        }

        int i = free.Count - 1;
        GameObject pref = free[i];
        pref.transform.position = pos;
        free.RemoveAt(i);
        pref.SetActive(true);
        return pref;
    }

    void ReturnObject(GameObject pref)
    {
        active.Remove(pref);
        free.Add(pref);
        pref.SetActive(false);
    }
}
