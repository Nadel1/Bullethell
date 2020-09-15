using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickupBehaviour : MonoBehaviour
{
    //Randomly evaluated
    private float multiplier;
    //evaluated based on the strength of the pickup
    private float lastingTime;

    private GameObject GameController;
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)(Time.realtimeSinceStartup*100000));
        multiplier = Random.Range(0.2f, 4);
        lastingTime = 10 - multiplier;
        GameController = GameObject.FindGameObjectWithTag("GameController");
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(Player.position, transform.position) >= 40)
        {
            GameController.GetComponent<PickUpSpawner>().PickUpDeleted();
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.GetComponent<ScoreSystem>().AddMultiplier(multiplier, lastingTime);
            GameController.GetComponent<PickUpSpawner>().PickUpDeleted();

            Destroy(this.gameObject);
        }
        
    }
}
