﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Healthbar of an enemy")]
    [Range(50, 500)]
    private float health = 100;

    [SerializeField]
    [Tooltip("Damage taken by an incoming projectile")]
    [Range(20, 100)]
    private float damageTakenByProjectiles = 50;

    [SerializeField]
    [Tooltip("2d speed of enemy")]
    private float speed;

    [SerializeField]
    [Tooltip("Max distance an enemy can have from the player before it gets destroyed")]
    [Range(700, 10000)]
    private int maxDistance=750;

    //needed for calculations
    private Rigidbody rb;
    private Transform target;
    private Vector2 targetVector;
    private Vector2 forward;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        
    }
    private void Update()
    {
       

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target);
        
        /*if (transform.rotation.z<0)
        {
            transform.rotation = new Quaternion(0, 0, transform.rotation.z+180, transform.rotation.w);
        }*/
        {
            
            targetVector = target.position;
            //forward = new Vector2(rb.transform.forward.x, rb.transform.forward.y);

            rb.MovePosition(rb.position+speed*new Vector3(transform.forward.x , transform.forward.y, 0)*Time.fixedDeltaTime);
            // rb.velocity = transform.forward * speed;

        }

        
        /*
        Quaternion rotation = Quaternion.LookRotation
            (target.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);*/
        
        if (Vector2.Distance(target.position, transform.position) > maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("hit");
            health -= damageTakenByProjectiles;
            if (health <= 0) Destroy(this.gameObject);
        }
    }

}
