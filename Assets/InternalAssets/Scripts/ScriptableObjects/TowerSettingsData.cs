using DP.TowerDefense.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    [CreateAssetMenu(fileName = "TowerSettingsData", menuName = "Tower Settings Data")]
    public class TowerSettingsData : ScriptableObject
    {
        public TowerSettings[] towerSettings;
    }

    [Serializable]
    public class TowerSettings
    {
        public Enumerators.TowerType towerType;
        public GameObject prefab;
        public int buiildPrice;
        public float range;
        public float shootInterval;
        public int damage;
    }
}