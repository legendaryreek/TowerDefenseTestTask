﻿using DP.TowerDefense.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DP.TowerDefense.Utils;

namespace DP.TowerDefense
{
    public class TowerPopup : IUIPopup
    {
        public GameObject Self => _selfPopup;

        private GameObject _selfPopup;

        private IUIManager _uiManager;
        private ILoadObjectsManager _loadObjectsManager;
        private IGameManager _gameManager;

        private RectTransform _container;
        private Button _sellTowerButton;
        private TextMeshProUGUI _sellTowerButtonLabelText;

        private TowerSlot _selectedTowerSlot;

        private float _whiteSpaceToScreenBorders = 15f;

        public void Init()
        {
            _uiManager = GameClient.Get<IUIManager>();
            _loadObjectsManager = GameClient.Get<ILoadObjectsManager>();
            _gameManager = GameClient.Get<IGameManager>();

            _selfPopup = MonoBehaviour.Instantiate(_loadObjectsManager.GetObjectByPath<GameObject>(Constants.PATH_TO_UI_PREFABS + "Popups/TowerPopup"));
            _selfPopup.transform.SetParent(_uiManager.PopupsContainer.transform, false);

            _container = _selfPopup.transform.Find("Container").GetComponent<RectTransform>();

            _sellTowerButton = _container.Find("Button_SellTower").GetComponent<Button>();
            _sellTowerButton.onClick.AddListener(OnSellTowerButtonClickHandler);

            _sellTowerButtonLabelText = _sellTowerButton.transform.Find("Text_Label").GetComponent<TextMeshProUGUI>();

            Hide();
        }

        private void OnSellTowerButtonClickHandler()
        {
            if (_selectedTowerSlot != null)
            {
                if (!_selectedTowerSlot.IsEmpty)
                    _gameManager.TowerController.SellTower(_selectedTowerSlot);
                else
                    Debug.LogError("Selected TowerSlot is empty!");
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

            _selfPopup.SetActive(false);
        }

        public void Show()
        {
            _uiManager.HidePopup<BuildTowerPopup>();

            _selfPopup.SetActive(true);
        }

        public void Show(object data)
        {
            if (data != null && data is TowerPopupInfo towerPopupInfo)
            {
                if (_selectedTowerSlot == towerPopupInfo.selectedTowerSlot)
                {
                    Hide();
                    return;
                }

                Vector3 targetPos = Utilites.ScreenToCanvasPoint(_uiManager.Canvas, towerPopupInfo.towerSlotScreenPosition);

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
                _selectedTowerSlot = towerPopupInfo.selectedTowerSlot;
                _sellTowerButtonLabelText.text = "SELL" + "\n<b>" + SettingsDataUtils.GetTowerSettingsByType(_selectedTowerSlot.TowerType).buildPrice + "$</b>";
            }

            Show();
        }

        public void SetMainPriority()
        {

        }
    }

    public class TowerPopupInfo
    {
        public Vector3 towerSlotScreenPosition;
        public TowerSlot selectedTowerSlot;
    }
}