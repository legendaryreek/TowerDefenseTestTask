using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class FourthTypeTower : Tower
    {
        private const Enumerators.TowerType towerType = Enumerators.TowerType.FOURTH_TYPE;

        public FourthTypeTower(GameObject prefab, Transform spawnPoint, Transform container) : base(towerType, spawnPoint, container)
        {

        }
    }
}