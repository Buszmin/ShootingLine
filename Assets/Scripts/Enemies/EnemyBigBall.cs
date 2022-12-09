using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigBall : EnemyController
{
    public override void GetDamage()
    {

        foreach (EnemyController enemy in EnemyController.enemyControllers)
        {
            if (enemy.Data.Type == EnemyData.EnemyType.smallBall || enemy.Data.Type == EnemyData.EnemyType.bigBall)
            {
                enemy.speedBonus -= enemy.speedBonus * 0.1f;
            }
        }

        base.GetDamage();
    }
}
