﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private int _poolDepth;

    private readonly List<GameObject> _pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _poolDepth; i++)
        {
            GameObject pooledObject = Instantiate(_gameObjectPrefab);
            pooledObject.SetActive(false);
            _pool.Add(pooledObject);
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i].activeInHierarchy == false)
                return _pool[i];
        }
        return null; 
    }
}