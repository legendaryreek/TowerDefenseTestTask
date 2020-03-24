using DP.TowerDefense.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace DP.TowerDefense
{
    public interface IUIManager
    {
        Canvas Canvas { get; }
        CanvasScaler CanvasScaler { get; }
        RectTransform PopupsContainer { get; }
        RectTransform PagesContainer { get; }
        IUIElement CurrentPage { get; }

        void SetPage<T>(bool hideAll = false) where T : IUIElement;
        IUIElement GetPage<T>() where T : IUIElement;

        void DrawPopup<T>(object message = null, bool setMainPriority = false) where T : IUIPopup;
        void HidePopup<T>() where T : IUIPopup;
        IUIPopup GetPopup<T>() where T : IUIPopup;

        void HideAllPages();
        void HideAllPopups();
    }
}