using DP.TowerDefense.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class WaveController
    {
        public event Action<int, int> OnWaveChangedEvent;

        public bool IsLastWaveFinished { get; private set; }

        private IGameManager _gameManager;

        private float _countdown;

        private int _currentWaveIndex;
        private Enumerators.WaveState _waveState;

        private Coroutine _spawnWaveCoroutine;

        private WaveSettings _currentWaveSettings;

        public WaveController()
        {
            _gameManager = GameClient.Get<IGameManager>();
        }

        public void StopWaves()
        {
            StopSpawnCoroutine();
        }

        public void Update()
        {
            if (_gameManager.IsGameRunning)
            {
                if (_waveState == Enumerators.WaveState.LAST_WAVE_FINISHED)
                    return;

                if (_countdown <= 0f)
                {
                    switch (_waveState)
                    {
                        case Enumerators.WaveState.WAIT_NEXT_WAVE:
                            StartWave();
                            break;
                        case Enumerators.WaveState.SPAWNING:
                            StopSpawnCoroutine();

                            if (_currentWaveIndex + 1 >= _gameManager.LevelController.CurrentLevel.LevelSettings.waves.Length)
                            {
                                IsLastWaveFinished = true;
                                _waveState = Enumerators.WaveState.LAST_WAVE_FINISHED;
                                break;
                            }

                            NextWave();
                            break;
                    }
                }

                _countdown -= Time.deltaTime;
            }
        }

        private void StopSpawnCoroutine()
        {
            if (_spawnWaveCoroutine != null)
                MainApp.Instance.StopCoroutine(_spawnWaveCoroutine);
        }

        private void StartWave()
        {
            _waveState = Enumerators.WaveState.SPAWNING;
            _countdown = _currentWaveSettings.duration;
            _spawnWaveCoroutine = MainApp.Instance.StartCoroutine(SpawnWave());
        }

        private void NextWave()
        {
            _waveState = Enumerators.WaveState.WAIT_NEXT_WAVE;
            _currentWaveIndex++;
            _currentWaveSettings = _gameManager.LevelController.CurrentLevel.LevelSettings.waves[_currentWaveIndex];
            _countdown = _currentWaveSettings.delayBeforeStartWave;

            OnWaveChangedEvent?.Invoke(_currentWaveIndex + 1, _gameManager.LevelController.CurrentLevel.LevelSettings.waves.Length);
        }

        private IEnumerator SpawnWave()
        {
            while (true)
            {
                _gameManager.EnemyController.SpawnRandomEnemy();
                yield return new WaitForSeconds(_currentWaveSettings.delayBetweenSpawning);
            }
        }

        public void InitWaves()
        {
            _currentWaveIndex = 0;
            _currentWaveSettings = _gameManager.LevelController.CurrentLevel.LevelSettings.waves[_currentWaveIndex];
            _countdown = _currentWaveSettings.delayBeforeStartWave;
            _waveState = Enumerators.WaveState.WAIT_NEXT_WAVE;
            IsLastWaveFinished = false;

            OnWaveChangedEvent?.Invoke(_currentWaveIndex + 1, _gameManager.LevelController.CurrentLevel.LevelSettings.waves.Length);
        }
    }
}