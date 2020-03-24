using DP.TowerDefense.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP.TowerDefense
{
    public class UIManager : IService, IUIManager
    {        
        public List<IUIElement> Pages { get { return _uiPages; } }

        private List<IUIElement> _uiPages;
        private List<IUIPopup> _uiPopups;

        public IUIElement CurrentPage { get; private set; }

        public Canvas Canvas { get; private set; }
        public CanvasScaler CanvasScaler { get; private set; }
        public RectTransform PopupsContainer { get; private set; }
        public RectTransform PagesContainer { get; private set; }
        
        public void Dispose()
        {
            foreach (var page in _uiPages)
                page.Dispose();

            foreach (var popup in _uiPopups)
                popup.Dispose();
        }

        public void Init()
        {
            Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            CanvasScaler = Canvas.GetComponent<CanvasScaler>();

            PagesContainer = Canvas.transform.Find("PagesContainer").GetComponent<RectTransform>();
            PopupsContainer = Canvas.transform.Find("PopupsContainer").GetComponent<RectTransform>();

            _uiPages = new List<IUIElement>();
            _uiPages.Add(new GameplayPage());

            foreach (var page in _uiPages)
                page.Init();
            
            _uiPopups = new List<IUIPopup>();
            _uiPopups.Add(new BuildTowerPopup());

            foreach (var popup in _uiPopups)
                popup.Init();
        }

        public void Update()
        {
            foreach (var page in _uiPages)
                page.Update();

            foreach (var popup in _uiPopups)
                popup.Update();
        }

        public void HideAllPages()
        {
            foreach (var _page in _uiPages)
                _page.Hide();
        }

        public void HideAllPopups()
        {
            foreach (var _popup in _uiPopups)
                _popup.Hide();
        }

        public void SetPage<T>(bool hideAll = false) where T : IUIElement
        {
            if (hideAll)
            {
                HideAllPages();
            }
            else
            {
                if (CurrentPage != null)
                    CurrentPage.Hide();
            }

            foreach (var _page in _uiPages)
            {
                if (_page is T)
                {
                    CurrentPage = _page;
                    break;
                }
            }

            CurrentPage.Show();
        }
        
        public void DrawPopup<T>(object message = null, bool setMainPriority = false) where T : IUIPopup
        {
            IUIPopup popup = null;

            foreach (var uiPopup in _uiPopups)
            {
                if (uiPopup is T)
                {
                    popup = uiPopup;
                    break;
                }
            }

            if (setMainPriority)
                popup.SetMainPriority();

            if (message == null)
                popup.Show();
            else
                popup.Show(message);
        }

        public void HidePopup<T>() where T : IUIPopup
        {
            foreach (var _popup in _uiPopups)
            {
                if (_popup is T)
                {
                    _popup.Hide();
                    break;
                }
            }
        }

        public IUIPopup GetPopup<T>() where T : IUIPopup
        {
            IUIPopup popup = null;

            foreach (var _popup in _uiPopups)
            {
                if (_popup is T)
                {
                    popup = _popup;
                    break;
                }
            }

            return popup;
        }

        public IUIElement GetPage<T>() where T : IUIElement
        {
            IUIElement page = null;

            foreach (var _page in _uiPages)
            {
                if (_page is T)
                {
                    page = _page;
                    break;
                }
            }

            return page;
        }
    }
}