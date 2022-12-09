using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBullet : Bullet
{
    [SerializeField] float expBulletRange = 3f;
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject baseObj;


    protected override void FixedUpdate()
    {
        baseObj.transform.position += baseObj.transform.forward * Time.deltaTime * speed;

        time += Time.deltaTime;
        if (time >= timeAlive)
        {
            Die();
        }
    }

    private void Explode()
    {
        ps.Play();
        foreach (EnemyController enemy in EnemyController.enemyControllers)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= expBulletRange)
            {
                enemy.GetDamage();
                enemy.GetDamage(); // exp bullets deal 2x dmg of normal bullet
            }
        }
        Shooting.isExpBulletActive = false;
        PlayerManager.Instance.RestartShotTimer();
    }

    public override void Die()
    {
        Destroy(trail);
        time = 0;

        gameObject.SetActive(false);
        Explode();
        pooling.StartCoroutine(pooling.ReturnToPoolWithDelay(baseObj, 1f));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
