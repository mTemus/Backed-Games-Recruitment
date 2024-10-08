using Lean.Pool;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Effects.Particles
{
    public abstract class CustomParticlesSystem : MonoBehaviour, IPoolable
    {
        [Header("References")] 
        [SerializeField]
        private ParticleSystem m_particles;

        private ParticleSystem.MainModule m_mainModule;

        protected void OnParticleSystemStopped()
        {
            LeanPool.Despawn(this);
        }

        public void SetColor(Color color)
        {
            m_mainModule.startColor = color;
        }

        #region IPoolable

        public void OnSpawn()
        {
            m_mainModule = m_particles.main;
            m_particles.Play();
        }

        public void OnDespawn()
        {
            m_mainModule.startColor = Color.clear;
            m_mainModule = default;
        }

        #endregion

        public class Builder
        {
            private Vector3 m_position;
            private Color m_color;
            private CustomParticlesSystem m_prefab;

            public Builder WithPosition(Vector3 position)
            {
                m_position = position;
                return this;
            }

            public Builder WithColor(Color color) {
                m_color = color;
                return this;    
            }   

            public Builder WithPrefab(CustomParticlesSystem prefab) 
            { 
                m_prefab = prefab;
                return this;
            }   

            public CustomParticlesSystem Build()
            {
                if (m_prefab == null)
                {
                    Debug.LogError($"CustomParticlesSystem Builder --- Can't build because prefab is null!");
                    return null;
                }

                var instance = LeanPool.Spawn(m_prefab, m_position, Quaternion.identity);
                instance.SetColor(m_color);

                return instance;
            }
        }
    }
}
