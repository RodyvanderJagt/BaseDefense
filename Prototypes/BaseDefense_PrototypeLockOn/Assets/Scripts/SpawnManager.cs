using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnRangeX;
    [SerializeField] private float _spawnHorizonZ;

    [SerializeField] ObjectPool _helicopterPool;
    [SerializeField] ObjectPool _tankPool;

    [SerializeField] private float _spawnRateTank = 2f;
    [SerializeField] private float _spawnRateHelicopter = 2f;

    [SerializeField] private float _spawnDelay = 5f;



    private void Start()
    {
        InvokeRepeating(nameof(SpawnTank), _spawnDelay, _spawnRateTank);
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


    private Vector3 GenerateSpawnPosition(float spawnHeight)
    {
        return new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), spawnHeight, _spawnHorizonZ);
    }

}
