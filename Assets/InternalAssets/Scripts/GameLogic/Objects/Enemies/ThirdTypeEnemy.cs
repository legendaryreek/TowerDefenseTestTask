using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class ThirdTypeEnemy : Enemy
    {
        private const Enumerators.EnemyType enemyType = Enumerators.EnemyType.THIRD_TYPE;

        public ThirdTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints, Transform container) : base(enemyType, enemySpawnPoint, wavepoints, container)
        {
        }
    }
}