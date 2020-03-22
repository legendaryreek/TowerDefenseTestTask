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
        public event Action<int> OnHealthAmountChangedEvent;

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
            
            OnCoinsAmountChangedEvent?.Invoke(Coins);
            OnHealthAmountChangedEvent?.Invoke(Health);
        }

        public void ChangeCoinsAmount(int coinsDelta)
        {
            Coins += coinsDelta;
            OnCoinsAmountChangedEvent?.Invoke(Coins);
        }

        public void Damage(int damageAmount)
        {
            Health -= damageAmount;
            OnHealthAmountChangedEvent?.Invoke(Health);
            
            if (Health <= 0)
                Die();
        }

        private void Die()
        {
            OnPlayerDiedEvent?.Invoke();
        }
    }
}