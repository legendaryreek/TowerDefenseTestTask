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

        public bool IsActive { get; private set; }

        public int DamageAmount { get; private set; }
        public int KillReward { get; private set; }

        public float TraveledDistance { get; private set; }

        private GameObject _selfObject;

        private float _speed;
        private int _health;

        private Transform _targetWaypoint;
        private int _waypointIndex;

        private float _distanceToTargetPoint = 0.25f;

        private Transform[] _waypoints;

        public Enemy(Enumerators.EnemyType enemyType, Transform enemySpawnPoint, Transform[] wavepoints, Transform container)
        {
            _waypoints = wavepoints;

            EnemySettings enemySettings = SettingsDataUtils.GetEnemySettingsByType(enemyType);
            SetEnemyParams(enemySettings);
            SpawnEnemy(enemySettings.prefab, enemySpawnPoint, container);
            _waypointIndex = 0;
            _targetWaypoint = _waypoints[0];
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
            IsActive = false;
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
            Dispose();
            OnEnemyDiedEvent?.Invoke(this);
        }

        private void Move()
        {
            Vector3 direction = _targetWaypoint.position - SelfTransform.position;
            float moveDistance = (direction.normalized * _speed * Time.deltaTime).magnitude;

            MoveToTargetWaypoint(moveDistance);
        }

        private void MoveToTargetWaypoint(float moveDistance)
        {
            float distanceToTargetWaypoint = Vector3.Distance(_targetWaypoint.position, SelfTransform.position);

            SelfTransform.position = Vector3.MoveTowards(SelfTransform.position, _targetWaypoint.position, moveDistance);
            TraveledDistance += moveDistance;

            if (moveDistance >= distanceToTargetWaypoint)
            {
                if (IsLastWaypoint())
                {
                    OnEnemyReachedEndOfWayEvent?.Invoke(this);
                    return;
                }
                else
                {
                    NextTargetWaypoint();
                    MoveToTargetWaypoint(moveDistance - distanceToTargetWaypoint);
                }
            }
        }

        private bool IsLastWaypoint()
        {
            return _waypointIndex + 1 >= _waypoints.Length;
        }

        private void NextTargetWaypoint()
        {
            _waypointIndex++;
            _targetWaypoint = _waypoints[_waypointIndex];
        }

        private void SpawnEnemy(GameObject prefab, Transform enemySpawnPoint, Transform container)
        {
            _selfObject = GameObject.Instantiate(prefab, enemySpawnPoint.position, enemySpawnPoint.rotation, container);
            SelfTransform = _selfObject.transform;
            TraveledDistance = 0f;
            IsActive = true;
        }
    }
}