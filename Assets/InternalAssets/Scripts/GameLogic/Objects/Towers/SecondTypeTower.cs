using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class SecondTypeTower : Tower
    {
        private const Enumerators.TowerType towerType = Enumerators.TowerType.SECOND_TYPE;
        
        public SecondTypeTower(GameObject prefab, Transform spawnPoint, Transform container) : base(towerType, spawnPoint, container)
        {

        }
    }
}