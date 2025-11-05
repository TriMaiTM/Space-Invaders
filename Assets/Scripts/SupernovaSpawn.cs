using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SupernovaSpawn : MonoBehaviour
{
    [SerializeField] private Transform minimumPosition;
    [SerializeField] private Transform maximumPosition;
    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

    [System.Serializable]
    public class Wave
    {
        public GameObject prefab;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnedObject;
    }

    void Update()
    {
        waves[waveNumber].spawnTimer += Time.deltaTime * SpaceshipController.Instance.boost;
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0;
            SpawnSupernova();
        }
        if (waves[waveNumber].spawnedObject >= waves[waveNumber].objectsPerWave)
        {
            waves[waveNumber].spawnedObject = 0;
            waveNumber++;
            if (waveNumber >= waves.Count)
            {
                waveNumber = 0; 
            }
        }
    }

    private void SpawnSupernova()
    {
        Instantiate(waves[waveNumber].prefab, RandomSpawnPoint(), transform.rotation);
        waves[waveNumber].spawnedObject++;
    }
    
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;
        spawnPoint.x = Random.Range(minimumPosition.position.x, maximumPosition.position.x);
        spawnPoint.y = minimumPosition.position.y;
        return spawnPoint;
    }
}
