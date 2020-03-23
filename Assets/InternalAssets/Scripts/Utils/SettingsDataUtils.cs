using DP.TowerDefense.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DP.TowerDefense.Utils
{
    public static class SettingsDataUtils
    {
        public static TowerSettings GetTowerSettingsByType(Enumerators.TowerType towerType)
        {
            TowerSettings[] towerSettings = GameClient.Get<IDataManager>().GetScriptableObject<TowerSettingsData>().towerSettings;
            TowerSettings towerData = towerSettings.FirstOrDefault(x => x.towerType == towerType);

            if (towerData == null)
                Debug.LogError("No TowerSettings for tower type - " + towerType);

            return towerData;
        }
    }
}