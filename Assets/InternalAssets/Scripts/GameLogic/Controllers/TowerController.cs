using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DP.TowerDefense.Common;
using DP.TowerDefense.Utils;
using UnityEngine.EventSystems;

namespace DP.TowerDefense
{
    public class TowerController
    {
        private IGameManager _gameManager;
        private IUIManager _uiManager;

        private int _towerSlotMaskLayerIndex;

        private TowerSlot[] _towerSlots;

        private bool _isRaycastAllowed;

        public TowerController()
        {
            _gameManager = GameClient.Get<IGameManager>();
            _uiManager = GameClient.Get<IUIManager>();

            _towerSlotMaskLayerIndex = LayerMask.GetMask("TowerSlot");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                _isRaycastAllowed = !Utilites.IsPointerOverUIElement(Input.mousePosition);

            if (Input.GetKeyUp(KeyCode.Mouse0))
                SendRaycastToTowerSlot();

            for (int i = 0; i < _towerSlots.Length; i++)
                _towerSlots[i].Update();
        }

        public void StartLevel()
        {
            _towerSlots = new TowerSlot[_gameManager.LevelController.CurrentLevel.TowerSlots.Length];

            for (int i = 0; i < _towerSlots.Length; i++)
            {
                _towerSlots[i] = new TowerSlot(_gameManager.LevelController.CurrentLevel.TowerSlots[i]);
            }
        }

        private void SendRaycastToTowerSlot()
        {
            bool isPointerOverUI = Utilites.IsPointerOverUIElement(Input.mousePosition); //EventSystem.current.IsPointerOverGameObject();

            if (isPointerOverUI || !_isRaycastAllowed)
                return;

            RaycastHit hit;
            Ray ray = _gameManager.MainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, _towerSlotMaskLayerIndex))
            {
                if (hit.collider.transform.parent)
                {
                    var towerSlot = _towerSlots.FirstOrDefault(x => x.EqualsGO(hit.collider.transform.parent.gameObject));

                    if (towerSlot != null)
                        SelectTowerSlot(towerSlot);
                }                
            }
        }

        private void SelectTowerSlot(TowerSlot towerSlot)
        {
            if (towerSlot.IsEmpty)
            {
                _uiManager.DrawPopup<BuildTowerPopup>(new BuildTowerPopupInfo()
                {
                    selectedTowerSlot = towerSlot,
                    towerSlotScreenPosition = _gameManager.MainCamera.WorldToScreenPoint(towerSlot.TowerSlotPosition),
                });
            }
            else
            {
                _uiManager.DrawPopup<TowerPopup>(new TowerPopupInfo()
                {
                    selectedTowerSlot = towerSlot,
                    towerSlotScreenPosition = _gameManager.MainCamera.WorldToScreenPoint(towerSlot.TowerSlotPosition),
                });
            }
        }

        public void BuildTower(TowerSlot selectedTowerSlot, Enumerators.TowerType towerType)
        {
            TowerSettings towerSettings = SettingsDataUtils.GetTowerSettingsByType(towerType);
            _gameManager.PlayerController.ChangeCoinsAmount(-towerSettings.buildPrice);
            selectedTowerSlot.BuildTower(towerType);
        }

        public void SellTower(TowerSlot selectedTowerSlot)
        {
            TowerSettings towerSettings = SettingsDataUtils.GetTowerSettingsByType(selectedTowerSlot.TowerType);
            _gameManager.PlayerController.ChangeCoinsAmount(towerSettings.buildPrice);
            selectedTowerSlot.SellTower();
        }
    }
}