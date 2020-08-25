using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rb;
    public float aliveTime = 5;
    private Vector2 forward;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(SelfDestruct());
        forward = new Vector2(rb.transform.right.x, rb.transform.right.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + forward * speed * Time.fixedDeltaTime);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(aliveTime);
        Destroy(this.gameObject);
    }
}
