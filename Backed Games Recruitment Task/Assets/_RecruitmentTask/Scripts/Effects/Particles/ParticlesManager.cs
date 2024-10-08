using Assets._RecruitmentTask.Scripts.Enemy;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Effects.Particles
{
    public class ParticlesManager : MonoBehaviour
    {
        [Header("Particles")]
        [SerializeField]
        private EnemyDeathParticles m_deathParticlesPrefab;

        public void OnEnemyDied(EnemiesManager.EnemyDeathData data)
        {
            new CustomParticlesSystem.Builder()
                .WithPrefab(m_deathParticlesPrefab)
                .WithColor(data.Color)
                .WithPosition(data.Position)
                .Build();
        }
    }
}
