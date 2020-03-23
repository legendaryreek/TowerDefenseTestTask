using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class DataManager : IDataManager, IService
    {
        private const string SCRIPTABLE_OBJECTS_PATH = "ScriptableObjects";
        private Dictionary<string, ScriptableObject> _scriptableObjects;

        public void Init()
        {
            LoadAllScriptableObjects();
        }

        public void Dispose()
        {

        }

        public void Update()
        {

        }

        public T GetScriptableObject<T>() where T : ScriptableObject
        {
            var typeName = typeof(T).FullName;

            if (_scriptableObjects.ContainsKey(typeName))
                return (T)_scriptableObjects[typeName];

            Debug.LogError($"Not find {typeName} in Resources/{SCRIPTABLE_OBJECTS_PATH}");
            return null;
        }

        private void LoadAllScriptableObjects()
        {
            _scriptableObjects = new Dictionary<string, ScriptableObject>();

            var scrObjects = GameClient.Get<ILoadObjectsManager>().GetAllObjectsByPath<ScriptableObject>(SCRIPTABLE_OBJECTS_PATH);
            foreach (var scrObj in scrObjects)
            {
                var scrObjType = scrObj.GetType().FullName;
                if (scrObjType != null)
                {
                    Debug.Log(scrObjType);
                    _scriptableObjects[scrObjType] = scrObj;
                }
            }
        }

    }
}