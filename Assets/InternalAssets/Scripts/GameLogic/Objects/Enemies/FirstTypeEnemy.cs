using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class FirstTypeEnemy : Enemy
    {
        private static GameObject prefab;

        static FirstTypeEnemy()
        {
            prefab = GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameObject>(Constants.PATH_TO_GAMEPLAY_PREFABS + "Enemies/Enemy_1");
        }

        public FirstTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints, Transform container) : base(prefab, enemySpawnPoint, wavepoints, container)
        {
        }
    }
}