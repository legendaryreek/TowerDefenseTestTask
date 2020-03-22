using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class LevelController
    {
        private GameObject[] _levelPrefabs;
        private GameObject _currentLevel;

        private int _currentLevelIndex;

        public LevelController()
        {
            _levelPrefabs = GameClient.Get<ILoadObjectsManager>().GetAllObjectsByPath<GameObject>(Constants.PATH_TO_GAMEPLAY_PREFABS + "Levels");
        }

        public void StartLevel()
        {
            SpawnLevel();
        }

        private void SpawnLevel()
        {
            _currentLevel = MonoBehaviour.Instantiate(_levelPrefabs[_currentLevelIndex]);
        }
    }
}