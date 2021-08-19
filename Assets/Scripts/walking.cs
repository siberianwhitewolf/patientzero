using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{
    public string targetName = "Player";
    public new Rigidbody2D rigidbody2D;
    public float walkingSpeed = 2f;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null) { 
        target = GameObject.Find(targetName).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

      /*  Vector2 auxVelocity = rigidbody2D.velocity;
        auxVelocity.x = transform.right.x * -1 * walkingSpeed;
        rigidbody2D.velocity = auxVelocity;
        if (target.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }*/
    }

    private void FixedUpdate()
    {
        Vector3 directionToTarget = target.position - this.transform.position;
        directionToTarget.Normalize();
        rigidbody2D.MovePosition(transform.position + directionToTarget * walkingSpeed * Time.fixedDeltaTime);
    }
}
