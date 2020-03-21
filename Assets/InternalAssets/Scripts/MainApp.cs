using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DP.TowerDefense
{
    public class MainApp : MonoBehaviour
    {
        public event Action LateUpdateEvent;
        public event Action FixedUpdateEvent;

        private static MainApp _Instance;
        public static MainApp Instance
        {
            get { return _Instance; }
            private set { _Instance = value; }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            if (Instance == this)
            {
                GameClient.Instance.InitServices();
                GameClient.Get<IAppStateManager>().ChangeAppState(Common.Enumerators.AppState.AppStart);         
            }
        }

        private void Update()
        {
            if (Instance == this)
                GameClient.Instance.Update();
        }

        private void LateUpdate()
        {
            if (Instance == this)
            {
                if (LateUpdateEvent != null)
                    LateUpdateEvent();
            }
        }

        private void FixedUpdate()
        {
            if (Instance == this)
            {
                if (FixedUpdateEvent != null)
                    FixedUpdateEvent();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
                GameClient.Instance.Dispose();
        }
    }
}