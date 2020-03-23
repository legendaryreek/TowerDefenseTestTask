using UnityEngine;

namespace DP.TowerDefense
{
    public interface IGameManager
    {
        bool IsGameRunning { get; }

        Camera MainCamera { get; }

        EnemyController EnemyController { get; }
        LevelController LevelController { get; }
        WaveController WaveController { get; }
        PlayerController PlayerController { get; }
        TowerController TowerController { get; }

        void PauseGame();
        void RestartGame();
        void StartGame();
        void StopGame();
        void CompleteLevel();
        void GameOver();
    }
}