using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class SecondTypeEnemy : Enemy
    {
        private static GameObject prefab;

        static SecondTypeEnemy()
        {
            prefab = GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameObject>(Constants.PATH_TO_GAMEPLAY_PREFABS + "Enemies/Enemy_2");
        }

        public SecondTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints, Transform container) : base(prefab, enemySpawnPoint, wavepoints, container)
        {
        }
    }
}