using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retratDistance;
    private GameObject player;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject bullet;


    private void Start()
    {
       player = FindObjectOfType<playerStats>().gameObject;
        
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<playerStats>().playerCurrentStatus != "Muerto")
        {

            float distance = Vector2.Distance(transform.position, player.transform.position);

            if ( distance > stoppingDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            }

            else if (distance < stoppingDistance &&  distance > retratDistance)
            {
                transform.position = this.transform.position;
            }

            else if (distance < retratDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("enemy-spit");
                timeBtwShots = startTimeBtwShots;
            }

            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
