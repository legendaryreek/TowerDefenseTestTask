using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class TowerSlot
    {
        public bool IsEmpty { get; private set; }

        private GameObject _selfObject;
        private Transform _towerBuildPoint;

        private Tower _tower;

        public TowerSlot(GameObject gameObject)
        {
            _selfObject = gameObject;
            _towerBuildPoint = _selfObject.transform.Find("TowerSpawnPosition");

            IsEmpty = true;
        }

        public void Update()
        {
            if (_tower != null)
                _tower.Update();
        }

        public bool EqualsGO(GameObject other)
        {
            return _selfObject == other;
        }

        public void BuildTower()
        {
            IsEmpty = false;
            _tower = new Tower(GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameObject>(Constants.PATH_TO_GAMEPLAY_PREFABS + "Towers/Tower_1"), _towerBuildPoint, _selfObject.transform);
        }

        public void SellTower()
        {
            IsEmpty = true;
            _tower.Sell();
            _tower = null;
        }
    }
}