using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class FourthTypeEnemy : Enemy
    {
        private static GameObject prefab;

        static FourthTypeEnemy()
        {
            prefab = GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameObject>(Constants.PATH_TO_GAMEPLAY_PREFABS + "Enemies/Enemy_4");
        }

        public FourthTypeEnemy(Transform enemySpawnPoint, Transform[] wavepoints) : base(prefab, enemySpawnPoint, wavepoints)
        {
        }
    }
}