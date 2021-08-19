using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    public Transform target;
    public string targetName = "Player";
    public new Rigidbody2D rigidbody2D;
    public float speed = 2;
    bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = GameObject.Find(targetName).transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInRange) {
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.Normalize();
        rigidbody2D.MovePosition(transform.position + directionToTarget * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

        }
    }

   

}
