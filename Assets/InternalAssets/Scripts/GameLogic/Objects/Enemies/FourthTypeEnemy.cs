using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class FourthTypeEnemy : Enemy
    {
        private const Enumerators.EnemyType enemyType = Enumerators.EnemyType.FOURTH_TYPE;

        public FourthTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints, Transform container) : base(enemyType, enemySpawnPoint, wavepoints, container)
        {
        }
    }
}