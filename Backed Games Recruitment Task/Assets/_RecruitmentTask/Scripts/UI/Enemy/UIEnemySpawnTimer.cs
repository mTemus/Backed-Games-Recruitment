using Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents;
using Assets._RecruitmentTask.Scripts.Architecture.Other.SubscriptableValue.ConcreteScriptableValue;
using Assets.Repos.Codebase.Code.Core.SubscripableValue;
using TMPro;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.UI.Enemy
{
    public class UIEnemySpawnTimer : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private TextMeshProUGUI m_spawnTimerText;

        [SerializeField]
        private ScriptableSubscriptableValueFloat m_spawnTimerValue;

        [Header("Events")] 
        [SerializeField] 
        private ScriptableEventEmpty m_onGameStart;

        [SerializeField] 
        private ScriptableEventEmpty m_onGameOver;

        public void OnGameStart()
        {
            m_spawnTimerValue.Value.AddChangedListener(OnTimerValueChanged);
        }

        public void OnGameOver()
        {
            m_spawnTimerValue.Value.RemoveChangedListener(OnTimerValueChanged);
        }

        private void OnTimerValueChanged(SubscriptableValueBase obj)
        {
            var timer = obj.GetValueAs<float>();

            if (timer < 0)
                timer = 0f;
        
            m_spawnTimerText.text = timer.ToString("0.00");
        }
    }
}
