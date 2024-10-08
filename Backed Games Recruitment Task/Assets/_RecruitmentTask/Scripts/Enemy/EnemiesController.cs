using System.Collections.Generic;
using System.Linq;
using Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents;
using Assets._RecruitmentTask.Scripts.Architecture.Other.SubscriptableValue.ConcreteScriptableValue;
using Assets._RecruitmentTask.Scripts.Wave.Data;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Enemy
{
    public class EnemiesController : MonoBehaviour, IEnemyParent
    {
        [Header("References")] 
        [SerializeField]
        private ScriptableSubscriptableValueFloat m_timerValue;

        [Header("Events")] 
        [SerializeField] private ScriptableEventEmpty m_cannotSpawnEvent;

        [Header("Properties")]
        [SerializeField]
        private float m_spawnScale = 1f;

        private WaveDataSO m_currentWaveData;
        private readonly Dictionary<EnemySlot, EnemyBase> m_spawnedEnemies = new();

        private void Awake()
        {
            enabled = false;

            foreach (var enemySlot in GetComponentsInChildren<EnemySlot>())
                m_spawnedEnemies.Add(enemySlot, null);
        }

        private void Update()
        {
            m_timerValue.Value.Value -= m_spawnScale * Time.deltaTime;

            if (m_timerValue.Value.Value > 0) 
                return;

            SpawnEnemy();
            SetTimer();
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

        public void OnWaveStart(WaveDataSO waveData)
        {
            m_currentWaveData = waveData;
            enabled = true;
        }

        public void OnEnemyDeath(EnemyBase enemy)
        {
            
        }
    }
}
