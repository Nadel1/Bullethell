using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootFrom;
    public float waitBetweenShots = 0.5f;
    private bool alreadyWaiting = false;


    // Update is called once per frame
    void Update()
    {
        if (!alreadyWaiting && (Input.GetButton("Shoot") || Input.GetMouseButtonDown(0)))
        {
            Instantiate(projectile, shootFrom.position,shootFrom.rotation);
            StartCoroutine(WaitBetweenShots());
        }
    }
    IEnumerator WaitBetweenShots()
    {
        alreadyWaiting = true;
        yield return new WaitForSeconds(waitBetweenShots);
        alreadyWaiting = false;
    }
}
