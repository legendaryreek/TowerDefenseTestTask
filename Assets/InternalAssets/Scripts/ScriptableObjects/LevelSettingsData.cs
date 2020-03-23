using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DP.TowerDefense
{
    [CreateAssetMenu(fileName = "LevelSettingsData", menuName = "Level Settings Data")]
    public class LevelSettingsData : ScriptableObject
    {
        public LevelSettings[] levels;
    }
    
    [Serializable]
    public class LevelSettings
    {
        public GameObject prefab;
        public int playerCoinsAmount;
        public int playerHealthAmount;
        public WaveSettings[] waves;
    }

    [Serializable]
    public class WaveSettings
    {
        public float duration;
        public float delayBeforeStartWave;
        public float delayBetweenSpawning;
    }
}