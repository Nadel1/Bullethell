using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform follow;
    public float moveSpeed = 0.1f;
    private Vector3 moveTo;
    // Update is called once per frame
    void FixedUpdate()
    {
        moveTo = new Vector3(follow.position.x, follow.position.y, -30);
        transform.position = Vector3.Lerp(transform.position, moveTo, moveSpeed*Time.fixedDeltaTime);
    }
}
