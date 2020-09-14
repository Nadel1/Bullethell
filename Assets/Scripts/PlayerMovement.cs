using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    public bool controller = false;
    public bool accelerateAxis = false;
    private float contrXDefault=0;
    private float contrYDefault=0;
    private bool accelerate = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!controller)
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetKey(KeyCode.W))
                rb.MovePosition(rb.position + speed * new Vector3(0, 1, 0) * Time.fixedDeltaTime);
            if(Input.GetKey(KeyCode.D))
                rb.MovePosition(rb.position + speed * new Vector3(1, 0, 0) * Time.fixedDeltaTime);
            if(Input.GetKey(KeyCode.S))
                rb.MovePosition(rb.position + speed * new Vector3(0, -1, 0) * Time.fixedDeltaTime);
            if(Input.GetKey(KeyCode.A))
                rb.MovePosition(rb.position + speed * new Vector3(-1, 0, 0) * Time.fixedDeltaTime);

        }
        else
        {
            float angle;
            Vector2 forward = new Vector2(rb.transform.right.x, rb.transform.right.y);

            //calculating rotation
            if (Input.GetAxis("VerticalY")>0.1f|| Input.GetAxis("VerticalY") <-0.1f || Input.GetAxis("HorizontalY") > 0.1f || Input.GetAxis("HorizontalY") < -0.1f)
            {
                contrXDefault = Input.GetAxis("VerticalY");
                contrYDefault = Input.GetAxis("HorizontalY");
                angle = Mathf.Atan2(Input.GetAxis("VerticalY"), Input.GetAxis("HorizontalY")) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                Debug.Log(contrXDefault);
                angle = Mathf.Atan2(contrXDefault, contrYDefault) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            //accelerating
            if (!accelerateAxis)
            {
                if (Input.GetButton("Gas"))
                {
                    rb.MovePosition(rb.position + speed * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.fixedDeltaTime);
                }
            }
            
            
           
                rb.MovePosition(rb.position + speed * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.fixedDeltaTime);
            //rb.MovePosition(rb.position + speed * new Vector3(transform.right.x, transform.right.y, 0) * Time.fixedDeltaTime);
        }
        
    }
}
