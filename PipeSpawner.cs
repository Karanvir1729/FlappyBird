using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public Transform bird;
    public BirdController birdController;
    public float spawnInterval = 2f;
    public float minYGap = -2f;
    public float maxYGap = 2f;
    public float spawnDistance = 10f;

    private float timeSinceLastSpawn;

    void Start()
    {
        timeSinceLastSpawn = 0f;
    }

    void Update()
    {
        if (birdController.IsGameOver()) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnPipe();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnPipe()
    {
        float gapYPosition = Random.Range(minYGap, maxYGap);
        Vector3 spawnPosition = new Vector3(bird.position.x + spawnDistance, gapYPosition, 0);

        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }
}
