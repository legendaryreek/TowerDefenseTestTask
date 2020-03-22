using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class PlayerController
    {
        public event Action OnPlayerDiedEvent;
        public event Action<int> OnCoinsAmountChangedEvent;

        public int Coins { get; private set; }
        public int Health { get; private set; }

        private IGameManager _gameManager;

        public PlayerController()
        {
            _gameManager = GameClient.Get<IGameManager>();
        }

        public void StartLevel()
        {
            Coins = _gameManager.LevelController.CurrentLevel.LevelSettings.playerCoinsAmount;
            Health = _gameManager.LevelController.CurrentLevel.LevelSettings.playerHealthAmount;
        }

        public void ChangeCoinsAmount(int coinsDelta)
        {
            Coins += coinsDelta;
            OnCoinsAmountChangedEvent?.Invoke(Coins);

            Debug.LogError("Coins: " + Coins);
        }

        public void Damage(int damageAmount)
        {
            Health -= damageAmount;
            Debug.LogError("Health: " + Health);

            if (Health <= 0)
                Die();
        }

        private void Die()
        {
            OnPlayerDiedEvent?.Invoke();
        }
    }
}