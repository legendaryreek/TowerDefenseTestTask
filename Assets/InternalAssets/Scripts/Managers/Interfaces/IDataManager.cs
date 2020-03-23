using UnityEngine;

namespace DP.TowerDefense
{
    public interface IDataManager
    {
        T GetScriptableObject<T>() where T : ScriptableObject;
    }
}