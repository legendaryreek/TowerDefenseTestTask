using DP.TowerDefense.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    [CreateAssetMenu(fileName = "EnemySettingsData", menuName = "Enemy Settings Data")]
    public class EnemySettingsData : ScriptableObject
    {
        public EnemySettings[] enemySettings;
    }

    [Serializable]
    public class EnemySettings
    {
        public Enumerators.EnemyType enemyType;
        public GameObject prefab;
        public int health;
        public float speed;
        public int damage;
        public int minKillReward;
        public int maxKillReward;
    }
}