using Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents;
using Assets._RecruitmentTask.Scripts.Wave.Data;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Wave
{
    public class LevelWaveSpawner : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] 
        private ScriptableEventWaveData m_waveStartEvent;
    
        [SerializeField] 
        private ScriptableEventWaveData m_waveEndEvent;

        [Header("Data")] 
        [SerializeField] 
        private WaveDataSO[] m_levelWavesData;
        
        private int m_currentWaveIndex = 0;

        public void OnGameStart()
        {
            StartWave();
        }

        public void OnGameEnd()
        {
            m_currentWaveIndex = 0;
        }

        public void StartNextWave()
        {
            m_currentWaveIndex++;

            if (m_currentWaveIndex > m_levelWavesData.Length - 1)
                m_currentWaveIndex = m_levelWavesData.Length - 1;
            
            StartWave();
        }

        public void StartWave()
        {
            m_waveStartEvent.Invoke(m_levelWavesData[m_currentWaveIndex]);
        }
    }
}
