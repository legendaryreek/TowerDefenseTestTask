using DP.TowerDefense.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DP.TowerDefense.Utils;

namespace DP.TowerDefense
{
    public class GameOverPopup : IUIPopup
    {
        public GameObject Self => _selfPopup;

        private GameObject _selfPopup;

        private IUIManager _uiManager;
        private ILoadObjectsManager _loadObjectsManager;
        private IGameManager _gameManager;
        
        private Button _restartButton;

        public void Init()
        {
            _uiManager = GameClient.Get<IUIManager>();
            _loadObjectsManager = GameClient.Get<ILoadObjectsManager>();
            _gameManager = GameClient.Get<IGameManager>();

            _selfPopup = MonoBehaviour.Instantiate(_loadObjectsManager.GetObjectByPath<GameObject>(Constants.PATH_TO_UI_PREFABS + "Popups/GameOverPopup"));
            _selfPopup.transform.SetParent(_uiManager.PopupsContainer.transform, false);

            _restartButton = _selfPopup.transform.Find("Button_Restart").GetComponent<Button>();
            _restartButton.onClick.AddListener(OnRestartButtonClickHandler);

            Hide();
        }

        private void OnRestartButtonClickHandler()
        {
            Hide();
            _gameManager.RestartGame();
        }

        public void Dispose()
        {

        }

        public void Update()
        {

        }

        public void Hide()
        {
            _selfPopup.SetActive(false);
        }

        public void Show()
        {
            _selfPopup.SetActive(true);
        }

        public void Show(object data)
        {
            Show();
        }

        public void SetMainPriority()
        {

        }
    }
}