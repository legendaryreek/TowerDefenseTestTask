namespace DP.TowerDefense.Common
{
    public class Enumerators
    {
        public enum AppState
        {
            Undefined,

            AppStart,
            Main,
            Gameplay,
        }

        public enum EnemyType
        {
            FIRST_TYPE,
            SECOND_TYPE,
            THIRD_TYPE,
            FOURTH_TYPE,
        }

        public enum WaveState
        {
            SPAWNING,
            WAIT_NEXT_WAVE,
            LAST_WAVE_FINISHED,
        }
    }
}