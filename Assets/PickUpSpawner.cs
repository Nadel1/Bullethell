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

    private float dropMult;
    private float mult;
    // Update is called once per frame
    void Update()
    {
        mult=GetComponent<ScoreSystem>().GetMultiplier();
        if (mult <= 1)
            dropMult = (0.25f * mult + 0.5f);
        else
            dropMult = ((1 / 16) * mult + 0.25f);
        Random.Range(0, 1);

    }
}
