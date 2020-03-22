using System.IO;
using UnityEngine;

namespace DP.TowerDefense
{
    public class LoadObjectsManager : IService, ILoadObjectsManager
    {
        public void Dispose()
        {

        }

        public void Init()
        {

        }

        public void Update()
        {

        }

        public T GetObjectByPath<T>(string path) where T : Object
        {
            return LoadFromResources<T>(path);
        }

        public T[] GetAllObjectsByPath<T>(string path) where T : Object
        {
            return LoadAllFromResources<T>(path);
        }

        public string GetTextByPath(string path)
        {
            return File.ReadAllText(path);
        }

        public void SetTextByPath(string path, string data)
        {
            File.WriteAllText(path, data);
        }

        private T LoadFromResources<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        private T[] LoadAllFromResources<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
    }
}