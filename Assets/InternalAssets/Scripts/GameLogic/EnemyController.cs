using DP.TowerDefense.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class EnemyController
    {
        private IGameManager _gameManager;

        private List<Enemy> _enemies;
        private Enumerators.EnemyType[] _enemyTypes;

        public EnemyController()
        {
            var enemyTypes = Enum.GetValues(typeof(Enumerators.EnemyType));
            _enemyTypes = new Enumerators.EnemyType[enemyTypes.Length];
            for (int i = 0; i < _enemyTypes.Length; i++)
                _enemyTypes[i] = (Enumerators.EnemyType)enemyTypes.GetValue(i);
            
            _gameManager = GameClient.Get<IGameManager>();

            _enemies = new List<Enemy>();
        }

        public void Update()
        {
            for (int i = 0; i < _enemies.Count; i++)
                _enemies[i].Update();
        }

        public void SpawnRandomEnemy()
        {
            SpawnEnemy(_enemyTypes[UnityEngine.Random.Range(0, _enemyTypes.Length)]);
        }

        public void SpawnEnemy(Enumerators.EnemyType enemyType)
        {
            Enemy enemy;

            switch (enemyType)
            {
                case Enumerators.EnemyType.FIRST_TYPE:
                    enemy = new FirstTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints);
                    break;
                case Enumerators.EnemyType.SECOND_TYPE:
                    enemy = new SecondTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints);
                    break;
                case Enumerators.EnemyType.THIRD_TYPE:
                    enemy = new ThirdTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints);
                    break;
                case Enumerators.EnemyType.FOURTH_TYPE:
                    enemy = new FourthTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints);
                    break;
                default:
                    Debug.LogError("An enemy of this type does not exist!");
                    return;
            }

            enemy.OnEnemyReachedEndOfWayEvent += OnEnemyReachedEndOfWayEventHandler;
            _enemies.Add(enemy);
        }

        private void OnEnemyReachedEndOfWayEventHandler(Enemy enemy)
        {
            enemy.Dispose();
            _enemies.Remove(enemy);
        }
    }
}