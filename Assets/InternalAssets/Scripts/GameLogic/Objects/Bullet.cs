using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class Bullet
    {
        public event Action<Bullet, Enemy> OnBulletHitTargetEvent;

        public bool IsDisposed { get; private set; }

        private static GameObject prefab;

        private GameObject _selfObject;
        private Transform _selfTransform;

        private Enemy _targetEnemy;

        private float _speed = 4f;

        static Bullet()
        {
            prefab = GameClient.Get<ILoadObjectsManager>().GetObjectByPath<GameObject>(Constants.PATH_TO_GAMEPLAY_PREFABS + "Bullet");

        }

        public Bullet(Enemy targetEnemy, Transform spawnPoint)
        {
            _targetEnemy = targetEnemy;
            SpawnBullet(prefab, spawnPoint);
        }

        public void Dispose()
        {
            IsDisposed = true;
            GameObject.Destroy(_selfObject);
        }

        public void Update()
        {
            if (!_targetEnemy.SelfTransform)
            {
                Dispose();
                return;
            }

            Move();
        }

        private void Move()
        {
            Vector3 direction = _targetEnemy.SelfTransform.position - _selfTransform.position;
            float moveDistance = _speed * Time.deltaTime;

            if (direction.magnitude <= moveDistance)
                HitTarget();

            _selfTransform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
        }

        private void HitTarget()
        {
            OnBulletHitTargetEvent?.Invoke(this, _targetEnemy);
            Dispose();
        }

        private void SpawnBullet(GameObject prefab, Transform spawnPoint)
        {
            _selfObject = GameObject.Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            _selfTransform = _selfObject.transform;
        }
    }
}