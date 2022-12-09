using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallCube : EnemyController
{
    protected override void CrossTheLine()
    {
        foreach(EnemyController enemy in EnemyController.enemyControllers)
        {
            if(enemy.Data.Type == EnemyData.EnemyType.smallCube || enemy.Data.Type == EnemyData.EnemyType.bigCube)
            {
                enemy.SetHp(enemy.hp + 0.1f * (enemy.Data.StartHp * enemy.Data.HpMultiplayer));
            }
        }

        base.CrossTheLine();
    }
}
