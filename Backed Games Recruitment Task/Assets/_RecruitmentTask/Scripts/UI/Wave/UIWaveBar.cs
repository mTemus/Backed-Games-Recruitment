using Assets._RecruitmentTask.Scripts.Enemy;
using Assets._RecruitmentTask.Scripts.Wave.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._RecruitmentTask.Scripts.UI.Wave
{
    public class UIWaveBar : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private Image m_progressBar;

        [SerializeField] 
        private TextMeshProUGUI m_progressText;

        private int m_maxProgress;
        private int m_currentProgress;

        public void OnWaveStart(WaveDataSO data)
        {
            m_maxProgress = data.PointsToEarn;
            UpdateProgress();
        }

        public void OnWaveEnd(WaveDataSO data)
        {
            ClearProgress();
        }

        public void OnGameRestart()
        {
            ClearProgress();
        }

        private void ClearProgress()
        {
            m_maxProgress = 0;
            m_currentProgress = 0;

            UpdateProgress();
        }

        public void OnEnemyDied(EnemiesManager.EnemyDeathData data)
        {
            m_currentProgress += data.Points;
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            m_progressBar.fillAmount = (float) m_currentProgress / m_maxProgress;
            m_progressText.text = $"{m_currentProgress}/{m_maxProgress}";
        }
    }
}
