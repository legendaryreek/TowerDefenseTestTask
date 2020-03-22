using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class GameManager : IGameManager, IService
    {
        public Action OnRestartGame { get; set; }
        public Action OnStartGame { get; set; }

        public GameSettings GameSettings { get; private set; }
        public bool IsGameRunning { get; set; }

        public EnemyController EnemyController { get; private set; }
        public LevelController LevelController { get; private set; }
        public WaveController WaveController { get; private set; }


        public void Init()
        {
            GameSettings = GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameSettings>("ScriptableObjects/GameSettings");
            
            LevelController = new LevelController();
            EnemyController = new EnemyController();
            WaveController = new WaveController();
        }

        public void Dispose()
        {

        }

        public void Update()
        {
            WaveController.Update();
            EnemyController.Update();
        }

        public void PauseGame()
        {

        }

        public void RestartGame()
        {
            OnRestartGame?.Invoke();
        }

        public void StartGame()
        {
            IsGameRunning = true;

            OnStartGame?.Invoke();

            LevelController.StartLevel();
            WaveController.InitWaves();
        }

        public void StopGame()
        {
            IsGameRunning = false;
        }
    }
}