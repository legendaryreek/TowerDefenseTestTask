using UnityEngine;

namespace DP.TowerDefense
{
    public interface IUIElement
    {
        void Init();
        void Show();
        void Hide();
        void Update();
        void Dispose();
    }
}