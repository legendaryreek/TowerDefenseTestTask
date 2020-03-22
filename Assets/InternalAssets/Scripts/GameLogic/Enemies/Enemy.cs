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

        public int DamageAmount { get; private set; } = 5;
        public int CoinsAmount { get; private set; } = 5;

        private GameObject _selfObject;
        private Transform _selfTransform;

        private float _speed = 1f;
        private int _health;

        private Transform _targetWavepoint;
        private int _wavepointIndex;

        private float _distanceToTargetPoint = 0.25f;

        private Transform[] _wavepoints;

        public Enemy(GameObject prefab, Transform enemySpawnPoint, Transform[] wavepoints)
        {
            _wavepoints = wavepoints;

            SpawnEnemy(prefab, enemySpawnPoint);
            _wavepointIndex = 0;
            _targetWavepoint = _wavepoints[0];
        }

        public void Dispose()
        {
            GameObject.Destroy(_selfObject);
        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 direction = _targetWavepoint.position - _selfTransform.position;
            _selfTransform.Translate(direction.normalized * _speed * Time.deltaTime);

            if (Vector3.Distance(_targetWavepoint.position, _selfTransform.position) <= _distanceToTargetPoint)
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

        private void SpawnEnemy(GameObject prefab, Transform enemySpawnPoint)
        {
            _selfObject = GameObject.Instantiate(prefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
            _selfTransform = _selfObject.transform;
        }
    }
}