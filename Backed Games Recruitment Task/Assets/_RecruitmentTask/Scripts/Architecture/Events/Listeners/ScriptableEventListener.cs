using Assets._RecruitmentTask.Scripts.Architecture.Events.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._RecruitmentTask.Scripts.Architecture.Events.Listeners
{
    public abstract class ScriptableEventListener<T> : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private ScriptableEvent<T> m_scriptableEvent;

        [Header("Events")] 
        [SerializeField] 
        private UnityEvent<T> m_unityEvent;

        private void Awake()
        {
            m_scriptableEvent.RegisterListener(this);
        }

        private void OnDestroy()
        {
            m_scriptableEvent.UnregisterListener(this);
        }

        public void Raise(T value)
        {
            m_unityEvent?.Invoke(value);
        }
    }
}
