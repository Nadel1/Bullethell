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
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)(Time.realtimeSinceStartup*100000));
        multiplier = Random.Range(0.2f, 4);
        lastingTime = 10 - multiplier;
        GameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.GetComponent<ScoreSystem>().AddMultiplier(multiplier, lastingTime);

            Destroy(this.gameObject);
        }
        
    }
}
