using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class ThirdTypeEnemy : Enemy
    {
        private static GameObject prefab;

        static ThirdTypeEnemy()
        {
            prefab = GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameObject>(Constants.PATH_TO_GAMEPLAY_PREFABS + "Enemies/Enemy_3");
        }

        public ThirdTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints, Transform container) : base(prefab, enemySpawnPoint, wavepoints, container)
        {
        }
    }
}