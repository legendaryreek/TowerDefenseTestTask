using DP.TowerDefense.Common;
using UnityEngine;


namespace DP.TowerDefense
{
    public sealed class AppStateManager : IService, IAppStateManager
    {
        private IUIManager _uiManager;

        public Enumerators.AppState AppState { get; set; } = Enumerators.AppState.Undefined;

        public void Dispose()
        {

        }

        public void Init()
        {
            _uiManager = GameClient.Get<IUIManager>();
        }

        public void Update()
        {

        }

        public void ChangeAppState(Enumerators.AppState stateTo)
        {
            if (AppState == stateTo)
                return;

            AppState = stateTo;

            switch (stateTo)
            {
                case Enumerators.AppState.AppStart:
                    break;
                case Enumerators.AppState.Main:
                    break;
                case Enumerators.AppState.Gameplay:
                    break;
            }
        }
    }
}