using Assets._RecruitmentTask.Scripts.Enemy.Data;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Wave.Data
{
    [CreateAssetMenu(fileName = "[NAME]_WaveDataSO", menuName = "ScriptableObject/Wave/Base data")]
    public class WaveDataSO : ScriptableObject
    {
        [Header("Properties")]
        [SerializeField]
        private int m_pointsToEarn;

        [SerializeField]
        private float m_enemiesSpawnTime;

        [Header("Enemies")]
        [SerializeField] 
        private EnemyDataSO[] m_enemies;

        public int PointsToEarn => m_pointsToEarn;
        public float EnemiesSpawnTime => m_enemiesSpawnTime;
        public EnemyDataSO[] Enemies => m_enemies;
        public EnemyDataSO RandomEnemyData => m_enemies.Length == 1 ? m_enemies[0] : m_enemies[Random.Range(0, m_enemies.Length)];
    }
}
