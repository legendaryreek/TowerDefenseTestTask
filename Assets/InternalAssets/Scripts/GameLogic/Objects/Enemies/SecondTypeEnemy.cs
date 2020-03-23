using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class SecondTypeEnemy : Enemy
    {
        private const Enumerators.EnemyType enemyType = Enumerators.EnemyType.SECOND_TYPE;

        public SecondTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints, Transform container) : base(enemyType, enemySpawnPoint, wavepoints, container)
        {
        }
    }
}