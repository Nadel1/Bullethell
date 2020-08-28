using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootFrom;
    public float waitBetweenShots = 0.5f;
    private bool alreadyWaiting = false;

    // Update is called once per frame
    void Update()
    {
        if (!alreadyWaiting)
        {
            Instantiate(projectile, shootFrom.position, this.gameObject.transform.rotation);
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
