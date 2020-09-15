using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Pickup which alters the score multiplier of player, spawns more frequently if current multiplier low")]
    private GameObject multiplierPickUp;

    [SerializeField]
    [Tooltip("Pickup which increases health of player, spawns for frequently if player health is low")]
    public GameObject healthPickUp;


    [SerializeField]
    [Tooltip("Spawn free zone around the player")]
    [Range(5, 100)]
    private float minSpawnRadius = 10;

    [SerializeField]
    [Tooltip("Short time out between spawning two enemies, decreases with further wave")]
    [Range(0, 2)]
    private float waitBetweenSpawns = 2;

    [SerializeField]
    [Tooltip("Maximum count of multiplier pickups that are spawned at the same time")]
    [Range(0, 300)]
    private int maxMultPickup=10;

    //counts how many multiplier pickups are currently spawned
    private int countMultiPickup = 0;
    private float spawnDistance;
    private float spawnDir;
 

    private bool spawning = false;
    private float dropMult;
    private float mult;

    private Transform playerPos;
    private bool waiting = false;
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        mult=GetComponent<ScoreSystem>().GetMultiplier();
        if (mult <= 1)
            dropMult = (0.25f * mult + 0.5f);
        else
            dropMult = ((1 / 16) * mult + 0.25f);
        float value=Random.Range(0, 1);
        if (value <= dropMult&&countMultiPickup<=maxMultPickup&&!waiting)
        {
            countMultiPickup++;
            StartCoroutine(SpawnPickUp());
        }

    }
    IEnumerator SpawnPickUp()
    {
        waiting = true;
        Random.InitState((int)Time.realtimeSinceStartup * 100000);
        spawnDistance = Random.Range(minSpawnRadius, minSpawnRadius * 1.5f);
        Random.InitState((int)Time.realtimeSinceStartup * 100000);
        spawnDir = Random.Range(0, 360);
        Vector2 spawn = new Vector2(Mathf.Sin(spawnDir), Mathf.Cos(spawnDir)) + new Vector2(transform.position.x, transform.position.y);
        Vector3 spawnAt = new Vector3(spawn.x, spawn.y);
        Instantiate(multiplierPickUp, spawnDistance * spawnAt, Quaternion.Euler(0,0,0));
        
        yield return new WaitForSeconds(waitBetweenSpawns);
        waiting = false;
    }
}
