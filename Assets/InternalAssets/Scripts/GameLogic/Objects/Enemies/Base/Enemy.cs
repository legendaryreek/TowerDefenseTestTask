using DP.TowerDefense.Common;
using DP.TowerDefense.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class Enemy
    {
        public event Action<Enemy> OnEnemyReachedEndOfWayEvent;
        public event Action<Enemy> OnEnemyDiedEvent;
        
        public Transform SelfTransform { get; private set; }

        public bool IsAlive { get; private set; }

        public int DamageAmount { get; private set; } = 75;
        public int KillReward { get; private set; } = 5;

        public float TraveledDistance { get; private set; }

        private GameObject _selfObject;

        private float _speed = 1f;
        private int _health = 10;

        private Transform _targetWavepoint;
        private int _wavepointIndex;

        private float _distanceToTargetPoint = 0.25f;

        private Transform[] _wavepoints;

        public Enemy(Enumerators.EnemyType enemyType, Transform enemySpawnPoint, Transform[] wavepoints, Transform container)
        {
            _wavepoints = wavepoints;

            EnemySettings enemySettings = SettingsDataUtils.GetEnemySettingsByType(enemyType);
            SetEnemyParams(enemySettings);
            SpawnEnemy(enemySettings.prefab, enemySpawnPoint, container);
            _wavepointIndex = 0;
            _targetWavepoint = _wavepoints[0];
        }

        private void SetEnemyParams(EnemySettings enemySettings)
        {
            _speed = enemySettings.speed;
            _health = enemySettings.health;
            DamageAmount = enemySettings.damage;
            KillReward = UnityEngine.Random.Range(enemySettings.minKillReward, enemySettings.maxKillReward + 1);
        }

        public void Dispose()
        {
            GameObject.Destroy(_selfObject);
        }

        public void Update()
        {
            Move();
        }

        public void Damage(int damage)
        {
            _health -= damage;

            if (_health < 0)
                _health = 0;

            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            IsAlive = false;
            Dispose();
            OnEnemyDiedEvent?.Invoke(this);
        }

        private void Move()
        {
            Vector3 direction = _targetWavepoint.position - SelfTransform.position;
            SelfTransform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
            TraveledDistance += (direction.normalized * _speed * Time.deltaTime).magnitude;

            if (Vector3.Distance(_targetWavepoint.position, SelfTransform.position) <= _distanceToTargetPoint)
            {
                SetNextTargetWavePoint();
            }
        }

        private void SetNextTargetWavePoint()
        {
            _wavepointIndex++;

            if (_wavepointIndex >= _wavepoints.Length)
            {
                OnEnemyReachedEndOfWayEvent?.Invoke(this);
                return;
            }

            _targetWavepoint = _wavepoints[_wavepointIndex];
        }

        private void SpawnEnemy(GameObject prefab, Transform enemySpawnPoint, Transform container)
        {
            _selfObject = GameObject.Instantiate(prefab, enemySpawnPoint.position, enemySpawnPoint.rotation, container);
            SelfTransform = _selfObject.transform;
            TraveledDistance = 0f;
            IsAlive = true;
        }
    }
}