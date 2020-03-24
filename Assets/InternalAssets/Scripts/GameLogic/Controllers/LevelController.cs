using DP.TowerDefense.Utils;
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

        private LevelSettings[] _levelSettings;

        private int _currentLevelIndex;

        public LevelController()
        {
            _gameManager = GameClient.Get<IGameManager>();
            _currentLevelIndex = 0;

            _levelSettings = GameClient.Get<IDataManager>().GetScriptableObject<LevelSettingsData>().levels;
        }

        public void Dispose()
        {
            if (CurrentLevel != null)
                CurrentLevel.Dispose();

            CurrentLevel = null;
        }

        public void StartLevel()
        {
            SpawnLevel();
        }

        private void SpawnLevel()
        {
            CurrentLevel = new Level(_levelSettings[_currentLevelIndex]);
        }
    }
}