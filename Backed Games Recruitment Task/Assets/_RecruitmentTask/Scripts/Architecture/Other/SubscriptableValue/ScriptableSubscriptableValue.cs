using Assets.Repos.Codebase.Code.Core.SubscripableValue;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Other.SubscriptableValue
{
    public abstract class ScriptableSubscriptableValue<T> : ScriptableObject
    {
        [SerializeField] 
        private SubscriptableValue<T> m_value;

        public SubscriptableValue<T> Value => m_value;
    }
}
