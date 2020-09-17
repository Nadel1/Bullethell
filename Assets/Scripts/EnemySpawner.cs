using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Enemy of type 1")]
    private GameObject enemy1;

    [SerializeField]
    [Tooltip("Enemy of type 2")]
    private GameObject enemy2;

    [SerializeField]
    [Tooltip("Spawn free zone around the player")]
    [Range(5, 100)]
    private float minSpawnRadius=10;

    [SerializeField]
    [Tooltip("Short time out between spawning two enemies, decreases with further wave")]
    [Range(0, 2)]
    private float waitBetweenSpawns = 2;

    private float spawnDistance;
    private float spawnDir;
    //track the position of player to spawn enemies accordingly
    private GameObject player;


    private bool spawning = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnEnemies());
        }
            
    }

    IEnumerator SpawnEnemies()
    {
        //block coroutine
        spawning = true;

        //randomly decide spawnposition
        Random.InitState((int)Time.realtimeSinceStartup * 100000);
        spawnDistance = Random.Range(minSpawnRadius, minSpawnRadius * 1.5f);
        Random.InitState((int)Time.realtimeSinceStartup * 100000);
        spawnDir = Random.Range(0, 360);
        Vector2 spawn = new Vector2(Mathf.Sin(spawnDir), Mathf.Cos(spawnDir)) + new Vector2(transform.position.x, transform.position.y);
        Vector3 spawnAt = new Vector3(spawn.x,spawn.y);

        //instantiate and unblock coroutine
        Instantiate(enemy1,spawnDistance*spawnAt, Quaternion.identity);

        yield return new WaitForSeconds(waitBetweenSpawns);
        spawning = false;
    }
}
