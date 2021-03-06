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
    [Range(0, 10000)]
    private int maxDistance=750;

    //needed for calculations
    private Rigidbody rb;
    private Transform target;
    private Vector2 targetVector;
    private Vector2 forward;

    private GameObject camera;

    private bool moving = false;
    private Vector3 destination;

    private Vector3 moveTo;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        destination = transform.position;
        moveTo = destination.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target);

        if (Vector3.Distance(destination, transform.position) < 2)
        {
            destination = transform.forward * Vector3.Distance(target.position, transform.position) * 2;
            moveTo = destination.normalized;
        }

        rb.MovePosition(rb.position + speed * new Vector3(moveTo.x, moveTo.y, 0) * Time.fixedDeltaTime);
        

        /*if (Vector3.Distance(target.position,this.transform.position)>3)
        {
            
            targetVector = target.position;
            //forward = new Vector2(rb.transform.forward.x, rb.transform.forward.y);

            rb.MovePosition(rb.position+speed*new Vector3(transform.forward.x , transform.forward.y, 0)*Time.fixedDeltaTime);
            // rb.velocity = transform.forward * speed;

        }*/


        

        if (Vector3.Distance(target.position, transform.position) > maxDistance)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Movement()
    {
        moving = true;
        
        yield return new WaitForSeconds(2);
        moving = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Projectile"|| collision.gameObject.tag == "Player Projectile")
        {
            Debug.Log("hit");
            health -= damageTakenByProjectiles;
            if (health <= 0)
            {
                if(collision.gameObject.tag == "Player Projectile")
                    camera.GetComponent<CameraBehaviour>().InduceStress(1);
                Destroy(this.gameObject);
            }
            else
            {
                if (collision.gameObject.tag == "Player Projectile")
                    camera.GetComponent<CameraBehaviour>().InduceStress(1);
            }
        }
    }

}
