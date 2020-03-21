namespace DP.TowerDefense
{
    public class GameClient : ServiceLocatorBase
    {
        private static object _sync = new object();

        private static GameClient _Instance;
        public static GameClient Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_sync)
                    {
                        _Instance = new GameClient();
                    }
                }

                return _Instance;
            }
        }

        public static bool IsDebugMode = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameClient"/> class.
        /// </summary>
        internal GameClient() : base()
        {
#if UNITY_EDITOR
            IsDebugMode = true;
#endif

            AddService<ILoadObjectsManager>(new LoadObjectsManager());
            AddService<IAppStateManager>(new AppStateManager());
            AddService<IUIManager>(new UIManager());
            AddService<IGameManager>(new GameManager());
        }

        public static T Get<T>()
        {
            return Instance.GetService<T>();
        }
    }
}