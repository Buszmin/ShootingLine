using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigCube : EnemyController
{
    protected override void Die()
    {
        foreach (EnemyController enemy in EnemyController.enemyControllers)
        {
            if (enemy.hp < (enemy.Data.StartHp * enemy.Data.HpMultiplayer) * 0.5f)
            {
                enemy.SetHp(enemy.Data.StartHp * enemy.Data.HpMultiplayer);
            }
        }

        base.Die();
    }
}
