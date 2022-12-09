using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 1;
    [SerializeField] protected float timeAlive = 5;
    protected float time = 0;
    public GameObject trail;
    public Pooling pooling;

    protected virtual void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        time += Time.deltaTime;
        if (time >= timeAlive)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().GetDamage();
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(trail);
        time = 0;
        pooling.returnToPool(gameObject);
    }
}
