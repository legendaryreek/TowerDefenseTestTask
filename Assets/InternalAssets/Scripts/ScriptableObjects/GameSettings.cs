using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DP.TowerDefense
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public LevelSettings[] levels;
    }
    
    [Serializable]
    public class LevelSettings
    {
        public GameObject prefab;
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