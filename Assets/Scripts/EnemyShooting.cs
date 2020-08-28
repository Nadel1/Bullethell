using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootFrom;

    public Transform shootFrom1;
    public Transform shootFrom2;
    public Transform shootFrom3;
    public Transform shootFrom4;
    private Transform[] shootingArray;
    public float waitBetweenShots = 0.5f;
    public float waitBetweenTwoShots = 0.1f;
    public float waitBetweenTwoWaves = 1;
    private bool alreadyWaiting = false;
    private bool waitingBetweenShots = false;
    [SerializeField]
    [Tooltip("Shooting mode of the enemy")]
    [Range(0, 5)]
    private int shootingMode;

    private void Start()
    {
        shootingArray = new Transform[5];
        shootingArray[0] = shootFrom;
        shootingArray[1] = shootFrom1;
        shootingArray[2] = shootFrom2;
        shootingArray[3] = shootFrom3;
        shootingArray[4] = shootFrom4;
    }
    // Update is called once per frame
    void Update()
    {
        switch (shootingMode)
        {
            case 0:
                if (!alreadyWaiting)
                {
                    Instantiate(projectile, shootFrom.position, this.gameObject.transform.rotation);
                    StartCoroutine(WaitBetweenShots());
                }
                break;
            case 1:
                if (!alreadyWaiting)
                {
                    Instantiate(projectile, shootFrom.position, this.gameObject.transform.rotation);
                    StartCoroutine(WaitBetweenShots());
                }
                break;
            case 2:
                if (!alreadyWaiting)
                {
                    
                    Instantiate(projectile, shootingArray[1].position, this.gameObject.transform.rotation);
                    Instantiate(projectile, shootingArray[3].position, this.gameObject.transform.rotation);
                    StartCoroutine(WaitBetweenShots());
                }
                break;
            case 3:
                if (!alreadyWaiting)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Quaternion shoot = Quaternion.Euler(0, 0, i * 90);
                        Instantiate(projectile, this.gameObject.transform.position, shoot);
                    }
                    
                    StartCoroutine(WaitBetweenShots());
                }
                break;
            case 4:
                if (!alreadyWaiting)
                {
                    alreadyWaiting = true;
                    StartCoroutine(Waves());
                    
                }
                break;
            case 5:
                if (!alreadyWaiting)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(projectile, shootingArray[i].position, this.gameObject.transform.rotation);
                    }
                    StartCoroutine(WaitBetweenShots());
                }
                break;
        }
    }

    IEnumerator Waves()
    {
        alreadyWaiting = true;
         for (int i = 0; i < 8; i++)
         {
            Quaternion shoot = Quaternion.Euler(0, 0, i * 45);
            Instantiate(projectile, this.gameObject.transform.position, shoot);
         }
        yield return new WaitForSeconds(waitBetweenTwoShots);
        for (int i = 0; i < 8; i++)
        {
            Quaternion shoot = Quaternion.Euler(0, 0, i * 45);
            Instantiate(projectile, this.gameObject.transform.position, shoot);
        }
        yield return new WaitForSeconds(waitBetweenTwoWaves);
        alreadyWaiting = false;
    }
    IEnumerator WaitBetweenWaves()
    {
       
            waitingBetweenShots = true;
            yield return new WaitForSeconds(waitBetweenTwoShots);
            
        
    }


    IEnumerator WaitBetweenTwoShots()
    {
        alreadyWaiting = true;
        waitingBetweenShots = true;
        yield return new WaitForSeconds(waitBetweenTwoShots);
        waitingBetweenShots = false;
    }
    
    IEnumerator WaitBetweenShots()
    {
        alreadyWaiting = true;
        yield return new WaitForSeconds(waitBetweenShots);
        alreadyWaiting = false;
        waitingBetweenShots = false;
    }
}
