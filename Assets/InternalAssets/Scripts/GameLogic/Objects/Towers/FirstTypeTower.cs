using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class FirstTypeTower : Tower
    {
        private const Enumerators.TowerType towerType = Enumerators.TowerType.FIRST_TYPE;

        public FirstTypeTower(GameObject prefab, Transform spawnPoint, Transform container) : base(towerType, spawnPoint, container)
        {

        }
    }
}