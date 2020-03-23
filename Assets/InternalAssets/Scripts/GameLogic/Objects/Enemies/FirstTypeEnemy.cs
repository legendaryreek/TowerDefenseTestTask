using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class FirstTypeEnemy : Enemy
    {
        private const Enumerators.EnemyType enemyType = Enumerators.EnemyType.FIRST_TYPE;

        public FirstTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints, Transform container) : base(enemyType, enemySpawnPoint, wavepoints, container)
        {
        }
    }
}