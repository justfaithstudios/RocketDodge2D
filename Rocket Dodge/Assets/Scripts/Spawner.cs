using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Vector2 secondsBetweenSpawnMinMax;
    float nextSpawnTime;
    public GameObject fallingBlockPrefab;
    Vector2 screenHalfSize;
    private float startTime;
    private float animationDuration = 3.0f;

    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;
    void Start()
    {
        screenHalfSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        startTime = Time.time;

    }


    void Update()
    {

        if (Time.time - startTime < animationDuration)
        {

            return;
        }


        if (Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnMinMax.y, secondsBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
            Debug.Log(secondsBetweenSpawns);
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSize.x, screenHalfSize.x), screenHalfSize.y + spawnSize);
            GameObject newBlock = (GameObject)Instantiate(fallingBlockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            newBlock.transform.localScale = Vector2.one * spawnSize; // random size for falling cubes
        }

    }

}
