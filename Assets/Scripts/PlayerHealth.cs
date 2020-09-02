using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Healthpool of player")]
    [Range(100, 500)]
    private float health = 300;

    [SerializeField]
    [Tooltip("Damage taken by an incoming projectile")]
    [Range(20, 100)]
    private float damageTakenByProjectiles = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("hit");
            health -= damageTakenByProjectiles;
            if (health <= 0) Destroy(this.gameObject);
        }
    }
}
