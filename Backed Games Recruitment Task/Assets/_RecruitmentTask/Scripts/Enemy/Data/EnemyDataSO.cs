using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Enemy.Data
{
    [CreateAssetMenu(fileName = "[NAME]_EnemyDataSO", menuName = "ScriptableObject/Enemy/Base data")]
    public class EnemyDataSO : ScriptableObject
    {
        [Header("Prefab")]
        [SerializeField] 
        private EnemyBase m_prefab;

        [Header("Properties")]
        [SerializeField]
        private float m_scale;

        [SerializeField] 
        private int m_pointsForDefeating;

        public EnemyBase Prefab => m_prefab;
        public float Scale => m_scale;
        public int PointsForDefeating => m_pointsForDefeating;
    }
}
