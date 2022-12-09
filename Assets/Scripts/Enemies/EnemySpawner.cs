using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [Range(10f, 200f)][SerializeField] float zSpawnPos;
    [Range(-100f, 0f)][SerializeField] float xMinSpawnPos;
    [Range(0, 100f)][SerializeField] float xMaxSpawnPos;
    [Range(0.01f, 10f)][SerializeField] float minSpawnRate;
    [Range(2f, 20f)][SerializeField] float maxSpawnRate;

    [Header("Spawn Chances")]
    [Range(0f, 100f)][SerializeField] float smallCubeChance;
    [Range(0f, 100f)][SerializeField] float bigCubeChance;
    [Range(0f, 100f)][SerializeField] float smallBallChance;
    [Range(0f, 100f)][SerializeField] float bigBallChance;

    [Header("Polling Comonents")]
    [SerializeField] Pooling smallCubePooling;
    [SerializeField] Pooling bigCubePooling;
    [SerializeField] Pooling smallBallPooling;
    [SerializeField] Pooling bigBallPooling;

    float timer = 0;
    float nextSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        if (smallCubeChance + bigCubeChance + smallBallChance + bigBallChance != 100)
        {
            Debug.LogError("Chances of spawning all the enemies must be a total of 100%");
        }

        nextSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= nextSpawnRate)
        {
            nextSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            timer = 0;
            SpawnNewEnemy();
        }
    }

    private void SpawnNewEnemy()
    {
        float randomSpawn = Random.Range(0f, 100f);

        if (randomSpawn <= smallCubeChance)
        {
            SpawnEnemy(smallCubePooling);
        }
        else if (randomSpawn <= smallCubeChance + bigCubeChance)
        {
            SpawnEnemy(bigCubePooling);
        }
        else if (randomSpawn <= smallCubeChance + bigCubeChance + smallBallChance)
        {
            SpawnEnemy(smallBallPooling);
        }
        else if (randomSpawn <= smallCubeChance + bigCubeChance + smallBallChance + bigBallChance)
        {
            SpawnEnemy(bigBallPooling);
        }
    }

    private void SpawnEnemy(Pooling pooling)
    {
        GameObject newEnemy = pooling.activateNext();
        
        newEnemy.transform.GetChild(0).GetComponent<Renderer>().material.color = Random.ColorHSV();
        newEnemy.transform.GetChild(0).GetComponent<EnemyController>().Revive();
        newEnemy.transform.position = new Vector3(Random.Range(xMinSpawnPos, xMaxSpawnPos), -1.5f, zSpawnPos);
    }
}
