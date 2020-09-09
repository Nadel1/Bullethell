using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    public float aliveTime = 5;
    private Vector2 forward;
    //for the player projectile acceleration happens on the right vector
    public bool player = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(SelfDestruct());
        forward = new Vector2(rb.transform.forward.x, rb.transform.forward.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
        if (!player) { rb.MovePosition(rb.position + speed * new Vector3(transform.forward.x, transform.forward.y, 0) * Time.fixedDeltaTime); }
        else { rb.MovePosition(rb.position + speed * new Vector3(transform.right.x, transform.right.y, 0) * Time.fixedDeltaTime); }
        
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(aliveTime);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy"|| collision.gameObject.tag == "Wall") Destroy(this.gameObject);
    }
}
