using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class LevelController
    {
        public Level CurrentLevel { get; private set; }

        private IGameManager _gameManager;

        private int _currentLevelIndex;

        public LevelController()
        {
            _gameManager = GameClient.Get<IGameManager>();
        }

        public void StartLevel()
        {
            SpawnLevel();
        }

        private void SpawnLevel()
        {
            CurrentLevel = new Level(_gameManager.GameSettings.levels[_currentLevelIndex]);
        }
    }

    public class Level
    {
        private GameObject _selfObject;
        private Transform _selfTransform;

        public LevelSettings LevelSettings { get; private set; }

        public Transform EnemySpawnPoint { get; private set; }
        public Transform[] EnemyWaypoints { get; private set; }
        public GameObject[] TowerSlots { get; private set; }

        public Level(LevelSettings levelSettings)
        {
            LevelSettings = levelSettings;

            _selfObject = GameObject.Instantiate(levelSettings.prefab);
            _selfTransform = _selfObject.transform;

            EnemySpawnPoint = _selfTransform.Find("EnemySpawnPoint");
            SetEnemyWaypoints();
            SetTowerSlots();
        }

        private void SetTowerSlots()
        {
            Transform towerSlotsContainer = _selfTransform.Find("TowerSlots");
            TowerSlots = new GameObject[towerSlotsContainer.childCount];

            for (int i = 0; i < TowerSlots.Length; i++)
                TowerSlots[i] = towerSlotsContainer.GetChild(i).gameObject;
        }

        private void SetEnemyWaypoints()
        {
            Transform enemyWaypointsContainer = _selfTransform.Find("EnemyWaypoints");
            EnemyWaypoints = new Transform[enemyWaypointsContainer.childCount];

            for (int i = 0; i < EnemyWaypoints.Length; i++)
                EnemyWaypoints[i] = enemyWaypointsContainer.GetChild(i);
        }
    }
}