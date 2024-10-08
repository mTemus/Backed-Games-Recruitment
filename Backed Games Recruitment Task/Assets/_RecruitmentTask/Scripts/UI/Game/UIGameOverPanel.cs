using Assets._RecruitmentTask.Scripts.Architecture.Other.SubscriptableValue.ConcreteScriptableValue;
using TMPro;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.UI.Game
{
    public class UIGameOverPanel : UIPanel
    {
        [Header("References")] 
        [SerializeField]
        private ScriptableSubscriptableValueInt m_playerPoints;

        [SerializeField] 
        private TextMeshProUGUI m_scoreText;

        private void Awake()
        {
            Hide();
        }

        public override void Show()
        {
            m_scoreText.text = m_playerPoints.Value.Value.ToString();
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            m_scoreText.text = "0";
        }
    }
}
