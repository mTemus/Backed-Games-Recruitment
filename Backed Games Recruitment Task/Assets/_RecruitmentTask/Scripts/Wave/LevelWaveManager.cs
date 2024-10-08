using Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents;
using Assets._RecruitmentTask.Scripts.Enemy;
using Assets._RecruitmentTask.Scripts.Wave.Data;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Wave
{
    public class LevelWaveManager : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] 
        private ScriptableEventWaveData m_waveStartEvent;
    
        [SerializeField] 
        private ScriptableEventWaveData m_waveEndEvent;

        [Header("Data")] 
        [SerializeField] 
        private WaveDataSO[] m_levelWavesData;

        [Header("Debug")]
        [SerializeField]
        private int m_currentWaveIndex = 0;

        [SerializeField]
        private int m_wavePointsToReach = 0;

        [SerializeField]
        private int m_currentWavePoints = 0;

        public void OnGameStart()
        {
            StartWave();
        }

        public void OnGameOver()
        {
            m_currentWaveIndex = 0;
        }

        private void StartNextWave()
        {
            m_waveEndEvent.Invoke(m_levelWavesData[m_currentWaveIndex]);

            m_currentWavePoints = 0;
            ++m_currentWaveIndex;

            if (m_currentWaveIndex >= m_levelWavesData.Length)
                m_currentWaveIndex = m_levelWavesData.Length - 1;
            
            StartWave();
        }

        public void StartWave()
        {
            m_wavePointsToReach = m_levelWavesData[m_currentWaveIndex].PointsToEarn;
            m_waveStartEvent.Invoke(m_levelWavesData[m_currentWaveIndex]);
        }

        public void OnEnemyDied(EnemiesManager.EnemyDeathData data)
        {
            m_currentWavePoints += data.Points;

            if (m_currentWavePoints >= m_wavePointsToReach) 
                StartNextWave();
        }

        public void OnGameRestart()
        {
            m_currentWaveIndex = 0;
            m_currentWavePoints = 0;
            m_wavePointsToReach = 0;
        }
    }
}
