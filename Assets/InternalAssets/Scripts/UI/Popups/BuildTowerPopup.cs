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
    public class BuildTowerPopup : IUIPopup
    {
        public GameObject Self => _selfPopup;

        private GameObject _selfPopup;

        private IUIManager _uiManager;
        private ILoadObjectsManager _loadObjectsManager;
        private IGameManager _gameManager;

        private RectTransform _container;
        private RectTransform _buildTowerButtonsContainer;
        private GameObject _buildTowerButtonPrefab;

        private TowerSlot _selectedTowerSlot;

        private List<BuildTowerButton> _buildTowerButtons;

        private float _whiteSpaceToScreenBorders = 15f;

        public void Init()
        {
            _uiManager = GameClient.Get<IUIManager>();
            _loadObjectsManager = GameClient.Get<ILoadObjectsManager>();
            _gameManager = GameClient.Get<IGameManager>();

            _selfPopup = MonoBehaviour.Instantiate(_loadObjectsManager.GetObjectByPath<GameObject>(Constants.PATH_TO_UI_PREFABS + "Popups/BuildTowerPopup"));
            _selfPopup.transform.SetParent(_uiManager.PopupsContainer.transform, false);

            _container = _selfPopup.transform.Find("Container").GetComponent<RectTransform>();
            _buildTowerButtonsContainer = _selfPopup.transform.Find("Container/TowerButtons").GetComponent<RectTransform>();
            _buildTowerButtonPrefab = _loadObjectsManager.GetObjectByPath<GameObject>(Constants.PATH_TO_UI_PREFABS + "Elements/Button_BuildTower");

            _buildTowerButtons = new List<BuildTowerButton>();

            foreach (var towerData in GameClient.Get<IDataManager>().GetScriptableObject<TowerSettingsData>().towerSettings)
                CreateButton(towerData);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_container);

            Hide();
        }

        private void CreateButton(TowerSettings towerSettings)
        {
            Button button = MonoBehaviour.Instantiate(_buildTowerButtonPrefab, _buildTowerButtonsContainer, false).GetComponent<Button>();
            button.transform.Find("Text_Label").GetComponent<TextMeshProUGUI>().text = towerSettings.towerType + "\n" + "<b>" + towerSettings.buildPrice + "$</b>";
            button.onClick.AddListener(() => OnBuildTowerButtonClickHandler(towerSettings.towerType));
            _buildTowerButtons.Add(new BuildTowerButton(button, towerSettings));
        }

        private void OnBuildTowerButtonClickHandler(Enumerators.TowerType towerType)
        {
            if (_selectedTowerSlot != null)
            {
                if (_selectedTowerSlot.IsEmpty)
                    _gameManager.TowerController.BuildTower(_selectedTowerSlot, towerType);
                else
                    Debug.LogError("Selected TowerSlot is not empty!");
            }
            else
                Debug.LogError("_selectedTowerSlot value is null!");

            Hide();
        }

        public void Dispose()
        {

        }

        public void Update()
        {

        }

        public void Hide()
        {
            _selectedTowerSlot = null;
            _gameManager.PlayerController.OnCoinsAmountChangedEvent -= OnCoinsAmountChangedEventHandler;

            _selfPopup.SetActive(false);
        }

        public void Show()
        {
            _uiManager.HidePopup<TowerPopup>();

            _selfPopup.SetActive(true);

            _gameManager.PlayerController.OnCoinsAmountChangedEvent += OnCoinsAmountChangedEventHandler;

            SetBuildButtonsEnablesState(_gameManager.PlayerController.Coins);
        }
        
        public void Show(object data)
        {
            if (data != null && data is BuildTowerPopupInfo buildTowerPopupInfo)
            {
                if (_selectedTowerSlot == buildTowerPopupInfo.selectedTowerSlot)
                {
                    Hide();
                    return;
                }

                Vector3 targetPos = Utilites.ScreenToCanvasPoint(_uiManager.Canvas, buildTowerPopupInfo.towerSlotScreenPosition);
                
                float canvasRectWidth = _uiManager.Canvas.GetComponent<RectTransform>().rect.width;
                float canvasRectHeight = _uiManager.Canvas.GetComponent<RectTransform>().rect.height;

                if (targetPos.x + (_container.rect.width / 2f) > canvasRectWidth - _whiteSpaceToScreenBorders)
                    targetPos.x = canvasRectWidth - (_container.rect.width / 2f) - _whiteSpaceToScreenBorders;
                else if (targetPos.x - (_container.rect.width / 2f) < 0f + _whiteSpaceToScreenBorders)
                    targetPos.x = (_container.rect.width / 2f) + _whiteSpaceToScreenBorders;

                if (targetPos.y + (_container.rect.height / 2f) > canvasRectHeight - _whiteSpaceToScreenBorders)
                    targetPos.y = canvasRectHeight - (_container.rect.height / 2f) - _whiteSpaceToScreenBorders;
                else if (targetPos.y - (_container.rect.height / 2f) < 0f + _whiteSpaceToScreenBorders)
                    targetPos.y = (_container.rect.height / 2f) + _whiteSpaceToScreenBorders;

                _container.anchoredPosition = targetPos;
                _selectedTowerSlot = buildTowerPopupInfo.selectedTowerSlot;
            }

            Show();
        }
        
        public void SetMainPriority()
        {

        }

        private void OnCoinsAmountChangedEventHandler(int currentPlayerCoinsAmount)
        {
            SetBuildButtonsEnablesState(currentPlayerCoinsAmount);
        }

        private void SetBuildButtonsEnablesState(int currentPlayerCoinsAmount)
        {
            foreach (var buildTowerButton in _buildTowerButtons)
                buildTowerButton.SetInteractableState(currentPlayerCoinsAmount);
        }
    }

    public class BuildTowerPopupInfo
    {
        public Vector3 towerSlotScreenPosition;
        public TowerSlot selectedTowerSlot;
    }

    public class BuildTowerButton
    {
        private Button button;
        private TowerSettings towerSettings;

        public BuildTowerButton(Button button, TowerSettings towerSettings)
        {
            this.button = button;
            this.towerSettings = towerSettings;
        }

        public void SetInteractableState(int playerCoinsAmount)
        {
            button.interactable = playerCoinsAmount >= towerSettings.buildPrice;
        }
    }
}