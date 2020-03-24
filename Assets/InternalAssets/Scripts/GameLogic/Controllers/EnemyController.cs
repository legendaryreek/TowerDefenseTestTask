using DP.TowerDefense.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class EnemyController
    {
        public List<Enemy> Enemies { get; private set; }

        private IGameManager _gameManager;
        private Enumerators.EnemyType[] _enemyTypes;

        private Transform _enemiesContainer;

        public EnemyController()
        {
            _enemiesContainer = new GameObject("EnemiesContainer").transform;

            var enemyTypes = Enum.GetValues(typeof(Enumerators.EnemyType));
            _enemyTypes = new Enumerators.EnemyType[enemyTypes.Length];
            for (int i = 0; i < _enemyTypes.Length; i++)
                _enemyTypes[i] = (Enumerators.EnemyType)enemyTypes.GetValue(i);
            
            _gameManager = GameClient.Get<IGameManager>();

            Enemies = new List<Enemy>();
        }

        public void Dispose()
        {
            foreach (var enemy in Enemies)
                enemy.Dispose();

            Enemies.Clear();
        }

        public void Update()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update();

                if (!Enemies[i].IsAlive)
                {
                    Enemies.RemoveAt(i);
                    i--;
                }
            }

            if (_gameManager.WaveController.IsLastWaveFinished && Enemies.Count == 0)
                _gameManager.CompleteLevel();
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
                    enemy = new FirstTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints, _enemiesContainer);
                    break;
                case Enumerators.EnemyType.SECOND_TYPE:
                    enemy = new SecondTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints, _enemiesContainer);
                    break;
                case Enumerators.EnemyType.THIRD_TYPE:
                    enemy = new ThirdTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints, _enemiesContainer);
                    break;
                case Enumerators.EnemyType.FOURTH_TYPE:
                    enemy = new FourthTypeEnemy(_gameManager.LevelController.CurrentLevel.EnemySpawnPoint, _gameManager.LevelController.CurrentLevel.EnemyWaypoints, _enemiesContainer);
                    break;
                default:
                    Debug.LogError("An enemy of this type does not exist!");
                    return;
            }

            enemy.OnEnemyReachedEndOfWayEvent += OnEnemyReachedEndOfWayEventHandler;
            enemy.OnEnemyDiedEvent += OnEnemyDiedEventHandler;
            Enemies.Add(enemy);
        }

        private void OnEnemyReachedEndOfWayEventHandler(Enemy enemy)
        {
            _gameManager.PlayerController.Damage(enemy.DamageAmount);
            KillEnemy(enemy);
        }

        private void OnEnemyDiedEventHandler(Enemy enemy)
        {
            KillEnemy(enemy);
        }

        private void KillEnemy(Enemy enemy)
        {
            _gameManager.PlayerController.ChangeCoinsAmount(enemy.KillReward);
            enemy.Dispose();
            Enemies.Remove(enemy);
        }
    }
}