using DP.TowerDefense.Common;
using DP.TowerDefense.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP.TowerDefense
{
    public class Tower
    {
        public Enumerators.TowerType TowerType { get; private set; }
        public int DamageAmount { get; private set; } = 5;

        private IGameManager _gameManager;

        private GameObject _selfObject;
        private Transform _selfTransform;
        private Transform _gunTransform;

        private List<Bullet> _bullets;

        private float _range;
        private float _shootInterval;
        private int _damage;
        private int _buildPrice;
        private float _countdownToShoot;

        private bool _isCoolsDown;

        public Tower(Enumerators.TowerType towerType, Transform spawnPoint, Transform container)
        {
            TowerType = towerType;
            _bullets = new List<Bullet>();

            _gameManager = GameClient.Get<IGameManager>();

            TowerSettings towerSettings = SettingsDataUtils.GetTowerSettingsByType(towerType);
            SetTowerParams(towerSettings);
            SpawnTower(towerSettings.prefab, spawnPoint, container);
        }

        public void Dispose()
        {
            GameObject.Destroy(_selfObject);

            foreach (var bullet in _bullets)
                bullet.Dispose();
        }

        public void Update()
        {
            CoolsDown();
            FindTarget();

            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].Update();

                if (_bullets[i].IsDisposed)
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Sell()
        {
            Dispose();
        }

        private void SetTowerParams(TowerSettings towerSettings)
        {
            _range = towerSettings.range;
            _shootInterval = towerSettings.shootInterval;
            _damage = towerSettings.damage;
            _buildPrice = towerSettings.buildPrice;
        }

        private void SpawnTower(GameObject prefab, Transform spawnPoint, Transform container)
        {
            _selfObject = GameObject.Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, container);
            _selfTransform = _selfObject.transform;
            _gunTransform = _selfTransform.Find("Body/Gun");
        }

        private void CoolsDown()
        {
            if (_countdownToShoot <= 0f)
                _isCoolsDown = false;

            _countdownToShoot -= Time.deltaTime;
        }

        private void FindTarget()
        {
            if (_gameManager.EnemyController.Enemies.Count <= 0)
                return;

            List<Enemy> sortedEnemies = new List<Enemy>(_gameManager.EnemyController.Enemies);
            sortedEnemies.Sort((x, y) => (x.TraveledDistance < y.TraveledDistance) ? 1 : 0);
            
            foreach (var enemy in sortedEnemies)
            {
                if (Vector3.Distance(enemy.SelfTransform.position, _selfTransform.position) <= _range)
                {
                    _selfTransform.rotation = Quaternion.LookRotation(enemy.SelfTransform.position - _selfTransform.position, _selfTransform.up);

                    if (!_isCoolsDown)
                        Shoot(enemy);

                    break;
                }
            }
        }

        private void Shoot(Enemy targetEnemy)
        {
            _isCoolsDown = true;
            _countdownToShoot = _shootInterval;

            Bullet bullet = new Bullet(targetEnemy, _gunTransform);
            bullet.OnBulletHitTargetEvent += OnBulletHitTargetEventHandler;
            _bullets.Add(bullet);
        }

        private void OnBulletHitTargetEventHandler(Bullet bullet, Enemy targetEnemy)
        {
            targetEnemy.Damage(_damage);
        }
    }
}