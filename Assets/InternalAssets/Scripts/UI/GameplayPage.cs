using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DP.TowerDefense
{
    public class GameplayPage : IUIElement
    {
        private GameObject _selfPage;

        private TextMeshProUGUI _playerHealthCountText,
                                _playerCoinsCountText,
                                _currentWaveProgressText;

        public void Init()
        {
            _selfPage = MonoBehaviour.Instantiate(GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameObject>(Constants.PATH_TO_UI_PREFABS + "GameplayPage"));
            _selfPage.transform.SetParent(GameClient.Get<IUIManager>().PagesContainer, false);

            _playerHealthCountText = _selfPage.transform.Find("Health/Text_Value").GetComponent<TextMeshProUGUI>();
            _playerCoinsCountText = _selfPage.transform.Find("Coins/Text_Value").GetComponent<TextMeshProUGUI>();
            _currentWaveProgressText = _selfPage.transform.Find("Waves/Text_Value").GetComponent<TextMeshProUGUI>();

            IGameManager gameManager = GameClient.Get<IGameManager>();

            gameManager.PlayerController.OnHealthAmountChangedEvent += OnHealthAmountChangedEventHandler;
            gameManager.PlayerController.OnCoinsAmountChangedEvent += OnCoinsAmountChangedEventHandler;
            gameManager.WaveController.OnWaveChangedEvent += OnWaveChangedEventHandler;
        }

        public void Dispose()
        {

        }

        public void Hide()
        {
            _selfPage.SetActive(false);
        }

        public void Show()
        {
            _selfPage.SetActive(true);
        }

        public void Update()
        {

        }

        private void OnCoinsAmountChangedEventHandler(int coinsAmount)
        {
            _playerCoinsCountText.text = coinsAmount.ToString();
        }

        private void OnHealthAmountChangedEventHandler(int healthAmount)
        {
            _playerHealthCountText.text = healthAmount.ToString();
        }

        private void OnWaveChangedEventHandler(int currentWaveIndex, int waveAmount)
        {
            _currentWaveProgressText.text = currentWaveIndex + "/" + waveAmount;
        }
    }
}