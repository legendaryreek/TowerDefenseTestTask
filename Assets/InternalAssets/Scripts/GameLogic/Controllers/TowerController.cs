using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DP.TowerDefense
{
    public class TowerController
    {
        private IGameManager _gameManager;

        private int _towerSlotMaskLayerIndex;

        private TowerSlot[] _towerSlots;

        public TowerController()
        {
            _gameManager = GameClient.Get<IGameManager>();
            _towerSlotMaskLayerIndex = LayerMask.GetMask("TowerSlot");
        }

        public void Update()
        {
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
            RaycastHit hit;
            Ray ray = _gameManager.MainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, _towerSlotMaskLayerIndex))
            {
                if (hit.collider.transform.parent)
                {
                    var towerSlot = _towerSlots.FirstOrDefault(x => x.EqualsGO(hit.collider.transform.parent.gameObject));

                    if (towerSlot != null)
                    {
                        if (towerSlot.IsEmpty)
                        {
                            Debug.LogError("Build Tower");

                            towerSlot.BuildTower();
                        }
                        else
                        {
                            Debug.LogError("Sell Tower");

                            towerSlot.SellTower();
                        }
                    }
                }                
            }
        }
    }
}