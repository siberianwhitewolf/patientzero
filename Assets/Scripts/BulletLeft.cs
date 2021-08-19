using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLeft : MonoBehaviour
{
    public float speed = 20f;
    public new Rigidbody2D rigidbody2D;
    public int damage = 1;

    // Update is called once per frame
    void Start()
    {
        rigidbody2D.velocity = transform.right * -1 * speed;
        StartCoroutine("DestroyBulletAfterSeconds");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Debug.Log("Im on colission with: " + collision.gameObject.name);
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

        }
    }

    IEnumerator DestroyBulletAfterSeconds()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("I've been destroyed");
    }

}
