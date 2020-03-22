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

        private LevelController _levelController;

        public void Init()
        {
            _levelController = new LevelController();
        }

        public void Dispose()
        {

        }

        public void Update()
        {

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

            _levelController.StartLevel();
        }

        public void StopGame()
        {
            IsGameRunning = false;
        }
    }
}