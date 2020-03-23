using DP.TowerDefense.Common;
using System;
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
                Debug.LogError("No TowerSettings for tower with type - " + towerType);

            return towerData;
        }

        public static EnemySettings GetEnemySettingsByType(Enumerators.EnemyType enemyType)
        {
            EnemySettings[] enemySettings = GameClient.Get<IDataManager>().GetScriptableObject<EnemySettingsData>().enemySettings;
            EnemySettings enemyData = enemySettings.FirstOrDefault(x => x.enemyType == enemyType);

            if (enemyData == null)
                Debug.LogError("No EnemySettings for enemy with type - " + enemyType);

            return enemyData;
        }
    }
}