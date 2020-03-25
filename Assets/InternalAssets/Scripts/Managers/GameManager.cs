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
        
        public bool IsGameRunning { get; set; }

        public Camera MainCamera { get; private set; }

        public EnemyController EnemyController { get; private set; }
        public LevelController LevelController { get; private set; }
        public WaveController WaveController { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public TowerController TowerController { get; private set; }

        private IUIManager _uIManager;

        private float _fixedHorizontalFOV = 60f;

        public void Init()
        {
            _uIManager = GameClient.Get<IUIManager>();

            MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            MainCamera.fieldOfView = 2 * Mathf.Atan(Mathf.Tan(_fixedHorizontalFOV * Mathf.Deg2Rad * 0.5f) / (MainCamera.aspect / (4f / 3f))) * Mathf.Rad2Deg;

            LevelController = new LevelController();
            EnemyController = new EnemyController();
            WaveController = new WaveController();
            PlayerController = new PlayerController();
            TowerController = new TowerController();
        }

        public void Dispose()
        {

        }

        public void Update()
        {
            if (IsGameRunning)
            {
                WaveController.Update();
                EnemyController.Update();
                TowerController.Update();
            }
        }

        public void PauseGame()
        {

        }

        public void RestartGame()
        {
            OnRestartGame?.Invoke();

            LevelController.Dispose();
            EnemyController.Dispose();

            StartGame();
        }

        public void StartGame()
        {
            IsGameRunning = true;

            OnStartGame?.Invoke();

            LevelController.StartLevel();
            WaveController.InitWaves();
            PlayerController.StartLevel();
            TowerController.StartLevel();
        }

        public void StopGame()
        {
            IsGameRunning = false;

            _uIManager.HidePopup<BuildTowerPopup>();
            _uIManager.HidePopup<TowerPopup>();

            WaveController.StopWaves();
        }

        public void CompleteLevel()
        {
            StopGame();
            _uIManager.DrawPopup<LevelCompletedPopup>();
        }

        public void GameOver()
        {
            StopGame();
            _uIManager.DrawPopup<GameOverPopup>();
        }
    }
}