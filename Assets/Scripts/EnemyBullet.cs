using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public new Rigidbody2D rigidbody2D;
    private Transform player;
    private Vector2 target;
    public int damage = 1;
    private Animator animator;

    // Update is called once per frame

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        animator = this.GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            animator.SetTrigger("explode");
            
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats player = collision.gameObject.GetComponent<playerStats>();
            if (player != null)
            {
                Debug.Log("Im on colission with player");
                animator.SetTrigger("explode");
                collision.gameObject.GetComponent<playerStats>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }


    

}
