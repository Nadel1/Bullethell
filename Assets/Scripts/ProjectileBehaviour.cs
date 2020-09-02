using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    public float aliveTime = 5;
    private Vector2 forward;
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
        rb.MovePosition(rb.position + transform.forward* speed * Time.fixedDeltaTime);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(aliveTime);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") Destroy(this.gameObject);
    }
}
