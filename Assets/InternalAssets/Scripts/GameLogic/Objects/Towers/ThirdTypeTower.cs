using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class ThirdTypeTower : Tower
    {
        private const Enumerators.TowerType towerType = Enumerators.TowerType.THIRD_TYPE;

        public ThirdTypeTower(GameObject prefab, Transform spawnPoint, Transform container) : base(towerType, spawnPoint, container)
        {

        }
    }
}