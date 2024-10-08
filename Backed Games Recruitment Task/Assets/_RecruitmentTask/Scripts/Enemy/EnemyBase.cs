using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._RecruitmentTask.Scripts.Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IInteractable, IPoolable
    {
        [Header("References")] 
        [SerializeField]
        private Renderer m_renderer;

        private Color m_myColor;
        private int m_points;
        private IEnemyParent m_parent;

        public IEnemyParent Parent
        {
            get => m_parent;
            set => m_parent = value;
        }

        public void SetColor(Color color)
        {
            m_myColor = color;
            m_renderer.materials[0].color = m_myColor;
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        #region IInteractable

        public void Interact(GameObject interactor, Vector3 position)
        {
            m_parent.OnEnemyDeath(this);
        }

        #endregion

        #region IPoolable

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
            m_points = 0;
            m_myColor = Color.clear;
            m_parent = null;
        }

        #endregion

        #region Builder

        public class Builder
        {
            private Color m_color = Color.clear;
            private float m_scale = float.NegativeInfinity;
            private IEnemyParent m_parent;
            private Vector3 m_position;
            private Transform m_parentTransform;
            private EnemyBase m_prefab;

            public Builder WithRandomColor()
            {
                m_color = Random.ColorHSV();
                return this;
            }

            public Builder WithScale(float scale)
            {
                m_scale = scale;
                return this;
            }

            public Builder WithParent(IEnemyParent parent)
            {   
                m_parent = parent;  
                return this;
            }

            public Builder WithPosition(Vector3 position)
            {
                m_position = position;
                return this;
            }

            public Builder WithParentTransform(Transform parentTransform)
            {
                m_parentTransform = parentTransform;
                return this;
            }

            public Builder WithPrefab(EnemyBase prefab)
            {
                m_prefab = prefab;
                return this;
            }

            public EnemyBase Build()
            {
                if (m_prefab == null)
                {
                    Debug.LogError($"EnemyBuilder --- Can't build because prefab is null!");
                    return null;
                }

                var instance = LeanPool.Spawn(m_prefab, m_position, Quaternion.identity, m_parentTransform);
                instance.Parent = m_parent;

                if (m_color != Color.clear)
                    instance.SetColor(m_color);

                if (m_scale > float.NegativeInfinity)
                    instance.SetScale(m_scale);

                return instance;
            }
        }

        #endregion
    }
}
