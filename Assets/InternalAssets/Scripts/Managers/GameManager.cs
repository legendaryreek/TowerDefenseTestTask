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

        public void Init()
        {

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
        }

        public void StopGame()
        {
            IsGameRunning = false;
        }
    }
}