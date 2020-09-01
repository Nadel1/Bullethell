using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Healthbar of an enemy")]
    [Range(50, 500)]
    private float health = 100;

    [SerializeField]
    [Tooltip("2d speed of enemy")]
    private Vector2 speed;

    [SerializeField]
    [Tooltip("Max distance an enemy can have from the player before it gets destroyed")]
    [Range(700, 10000)]
    private int maxDistance=750;

    //needed for calculations
    private Rigidbody2D rb;
    private Transform target;
    private Vector2 targetVector;
    private Vector2 forward;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Vector2.Distance(target.position, transform.position) > 3)
        {
            
            targetVector = target.position;
            forward = new Vector2(rb.transform.right.x, rb.transform.right.y);
            rb.MovePosition(rb.position + forward * speed * Time.fixedDeltaTime);

        }
        Quaternion rotation = Quaternion.LookRotation
            (target.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        if(Vector2.Distance(target.position, transform.position) > maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("hit");
            health -= 50;
            if (health <= 0) Destroy(this.gameObject);
        }
    }

}
