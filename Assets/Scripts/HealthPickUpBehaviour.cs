using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpBehaviour : MonoBehaviour
{
    //Randomly evaluated
    private float effect=50;

    private GameObject GameController;
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
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
            Player.gameObject.GetComponent<PlayerHealth>().AddHealth(effect);
            GameController.GetComponent<PickUpSpawner>().PickUpDeleted();

            Destroy(this.gameObject);
        }

    }
}
