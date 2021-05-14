using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Spawn pools")]
    [SerializeField] ObjectPool _helicopterPool;
    [SerializeField] ObjectPool _tankPool;

    [Header("Spawn Location")]
    [SerializeField] private float _spawnRangeX;
    [SerializeField] private float _spawnHorizonZ;
    [SerializeField] private float _spawnHeightY;

    [Header("Spawn rate")]
    [SerializeField] private float _spawnRateTank = 2f;
    [SerializeField] private float _spawnRateHelicopter = 2f;

    [SerializeField] private float _spawnDelay = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTank), _spawnDelay, _spawnRateTank);
        InvokeRepeating(nameof(SpawnHelicopter), _spawnDelay, _spawnRateHelicopter);
    }

    private void SpawnTank()
    {
        GameObject tankToSpawn = _tankPool.GetAvailableObject();

        if(tankToSpawn != null)
        {
            tankToSpawn.transform.position = GenerateSpawnPosition(0);
            tankToSpawn.gameObject.SetActive(true);

        }
    }

    private void SpawnHelicopter()
    {
        GameObject helicopterToSpawn = _helicopterPool.GetAvailableObject();

        if (helicopterToSpawn != null)
        {
            helicopterToSpawn.transform.position = GenerateSpawnPosition(GenerateSpawnHeight());
            helicopterToSpawn.gameObject.SetActive(true);
        }
    }


    private float GenerateSpawnHeight()
    {
        return Random.Range(_spawnHeightY * 0.75f, _spawnHeightY * 1.25f);
    }

    private Vector3 GenerateSpawnPosition(float spawnHeight)
    {
        return new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), spawnHeight, _spawnHorizonZ);
    }

}
