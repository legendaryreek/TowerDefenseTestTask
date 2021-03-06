﻿namespace DP.TowerDefense
{
    public interface ILoadObjectsManager
    {
        T GetObjectByPath<T>(string path) where T : UnityEngine.Object;
        T[] GetAllObjectsByPath<T>(string path) where T : UnityEngine.Object;

        string GetTextByPath(string path);
        void SetTextByPath(string path, string data);
    }
}