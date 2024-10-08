using Assets._RecruitmentTask.Scripts.Architecture.Other.SubscriptableValue.ConcreteScriptableValue;
using Assets._RecruitmentTask.Scripts.Enemy;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Player
{
    public class PlayerPointsManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private ScriptableSubscriptableValueInt m_playerPoints;

        public void OnEnemyDied(EnemiesManager.EnemyDeathData data)
        {
            m_playerPoints.Value.Value += data.Points;
        }

        public void OnGameRestart()
        {
            m_playerPoints.Value.Value = 0;
        }

        private void OnDestroy()
        {
            m_playerPoints.Value.RemoveAllChangedListeners();
            m_playerPoints.Value.Value = 0;
        }
    }
}