using Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] 
        private ScriptableEventEmpty m_onGameStartEvent;

        [SerializeField]    
        private ScriptableEventEmpty m_onGameOverEvent;

        [SerializeField]
        private ScriptableEventEmpty m_onGameRestartEvent;

        private void Start()
        {
            m_onGameStartEvent.Invoke(default);
        }

        public void CallGameOver()
        {
            m_onGameOverEvent.Invoke(default);
        }

        public void Restart()
        {
            m_onGameRestartEvent.Invoke(default);
            m_onGameStartEvent.Invoke(default);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
