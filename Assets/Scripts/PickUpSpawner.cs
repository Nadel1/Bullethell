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
    [Range(0, 20)]
    private float waitBetweenSpawns = 2;

    [SerializeField]
    [Tooltip("Maximum count of multiplier pickups that are spawned at the same time")]
    [Range(0, 300)]
    private int maxMultPickup=10;

    //counts how many multiplier pickups are currently spawned
    private int countMultiPickup = 0;
    private int countHealthPickup = 0;
    private float spawnDistance;
    private float spawnDir;
 

    private bool spawning = false;
    private float dropMult;
    private float mult;

    private Transform playerPos;
    private bool waiting = false;
    private bool waitingHealth = false;

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
        Random.InitState((int)(Time.realtimeSinceStartup*1000000));
        float value=Random.Range(0, 1);
        if (value <= 1.25f-dropMult&&countMultiPickup<=maxMultPickup&&!waiting)
        {
            countMultiPickup++;
            StartCoroutine(SpawnPickUp(multiplierPickUp));
        }
        Random.InitState((int)(Time.realtimeSinceStartup * 1000000));
        value = Random.Range(0, 1);
        if(value>0.2f-playerPos.gameObject.GetComponent<PlayerHealth>().GetHealth()/playerPos.gameObject.GetComponent<PlayerHealth>()
            .GetStartHeath()&&!waitingHealth)
        {
            countHealthPickup++;
            StartCoroutine(SpawnHealthPickUp(healthPickUp));
        }

    }
    IEnumerator SpawnPickUp(GameObject pickUp)
    {
        waiting = true;
        Random.InitState((int)Time.realtimeSinceStartup * 100000);
        spawnDistance = Random.Range(minSpawnRadius, minSpawnRadius * 1.5f);
        Random.InitState((int)Time.realtimeSinceStartup * 100000);
        spawnDir = Random.Range(0, 360);
        Vector2 spawn = new Vector2(Mathf.Sin(spawnDir), Mathf.Cos(spawnDir)) + new Vector2(transform.position.x, transform.position.y);
        Vector3 spawnAt = new Vector3(spawn.x, spawn.y);
        Instantiate(pickUp, spawnDistance * spawnAt, Quaternion.Euler(0,0,0));
        
        yield return new WaitForSeconds(waitBetweenSpawns);
        waiting = false;
    }

    IEnumerator SpawnHealthPickUp(GameObject pickUp)
    {
        waitingHealth = true;
        Random.InitState((int)Time.realtimeSinceStartup * 1000);
        spawnDistance = Random.Range(minSpawnRadius, minSpawnRadius * 1.5f);
        Random.InitState((int)Time.realtimeSinceStartup * 100+1234444);
        spawnDir = Random.Range(0, 360);
        Vector2 spawn = new Vector2(Mathf.Sin(spawnDir), Mathf.Cos(spawnDir)) + new Vector2(transform.position.x, transform.position.y);
        Vector3 spawnAt = new Vector3(spawn.x, spawn.y);
        Instantiate(pickUp, spawnDistance * spawnAt, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(waitBetweenSpawns*2);
        waitingHealth = false;
    }

    public void PickUpDeleted()
    {
        countMultiPickup--;
    }
}
