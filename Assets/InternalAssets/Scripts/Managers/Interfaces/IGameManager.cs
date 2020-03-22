namespace DP.TowerDefense
{
    public interface IGameManager
    {
        GameSettings GameSettings { get; }
        bool IsGameRunning { get; }

        EnemyController EnemyController { get; }
        LevelController LevelController { get; }
        WaveController WaveController { get; }
        PlayerController PlayerController { get; }

        void PauseGame();
        void RestartGame();
        void StartGame();
        void StopGame();
    }
}