using System.Collections.Generic;
using System.Linq;
using Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents;
using Assets._RecruitmentTask.Scripts.Architecture.Other.SubscriptableValue.ConcreteScriptableValue;
using Assets._RecruitmentTask.Scripts.Wave.Data;
using Lean.Pool;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Enemy
{
    public class EnemiesManager : MonoBehaviour, IEnemyParent
    {
        [Header("References")] 
        [SerializeField]
        private ScriptableSubscriptableValueFloat m_timerValue;

        [Header("Events")] 
        [SerializeField] 
        private ScriptableEventEmpty m_cannotSpawnEvent;

        [SerializeField]
        private ScriptableEventEnemyDeathData m_enemyDeathEvent;

        [Header("Properties")]
        [SerializeField]
        private float m_spawnScale = 1f;

        private WaveDataSO m_currentWaveData;
        private readonly Dictionary<EnemySlot, EnemyBase> m_spawnedEnemies = new();

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            m_timerValue.Value.Value -= m_spawnScale * Time.deltaTime;

            if (m_timerValue.Value.Value > 0) 
                return;

            SpawnEnemy();
            SetTimer();
        }

        private void OnDestroy()
        {
            m_timerValue.Value.RemoveAllChangedListeners();
            m_timerValue.Value.Value = 0;
        }

        private void SpawnEnemy()
        {
            if (TryGetFreeSlot(out var slot))
            {
                var data = m_currentWaveData.RandomEnemyData;

                var enemy = new EnemyBase.Builder()
                    .WithPrefab(data.Prefab)
                    .WithScale(data.Scale)
                    .WithParent(this)
                    .WithRandomColor()
                    .WithParentTransform(slot.transform)
                    .WithPosition(slot.transform.position)
                    .WithPoints(data.PointsForDefeating)
                    .Build();

                m_spawnedEnemies[slot] = enemy;
            }
            else
            {
                m_cannotSpawnEvent.Invoke(default);
                enabled = false;
            }
        }

        private void DespawnEnemy(EnemyBase enemy, int points)
        {
            m_enemyDeathEvent.Invoke(new EnemyDeathData(points, enemy.transform.position, enemy.MyColor));
            LeanPool.Despawn(enemy);
        }

        private bool TryGetFreeSlot(out EnemySlot freeSlot)
        {
            foreach (var pair in m_spawnedEnemies.Where(pair => pair.Value == null))
            {
                freeSlot = pair.Key;
                return true;
            }

            freeSlot = null;
            return false;
        }

        private void SetTimer()
        {
            m_timerValue.Value.Value = m_currentWaveData.EnemiesSpawnTime;
        }

        public void OnGameStart()
        {
            foreach (var enemySlot in GetComponentsInChildren<EnemySlot>())
                m_spawnedEnemies.Add(enemySlot, null);
        }

        public void OnWaveStart(WaveDataSO waveData)
        {
            m_currentWaveData = waveData;
            enabled = true;
        }

        public void OnWaveEnd(WaveDataSO waveData)
        {
            enabled = false;
            m_timerValue.Value.Value = 0;

            m_currentWaveData = null;
        }

        public void OnGameOver()
        {
            foreach (var spawnedEnemy in m_spawnedEnemies)
                DespawnEnemy(spawnedEnemy.Value, 0);
            
            m_spawnedEnemies.Clear();
        }

        public void OnEnemyDeath(EnemyBase enemy)
        {
            foreach (var spawnedEnemy in m_spawnedEnemies)
            {
                if (spawnedEnemy.Value != enemy) 
                    continue;

                m_spawnedEnemies[spawnedEnemy.Key] = null;
                break;
            }

            DespawnEnemy(enemy, enemy.Points);
        }

        public struct EnemyDeathData
        {
            public int Points;
            public Vector3 Position;
            public Color Color;

            public EnemyDeathData(int points, Vector3 position, Color color)
            {
                Points = points;
                Position = position;
                Color = color;
            }
        }
    }
}
