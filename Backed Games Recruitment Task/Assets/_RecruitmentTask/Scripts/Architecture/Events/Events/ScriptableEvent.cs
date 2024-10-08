using System.Collections.Generic;
using Assets._RecruitmentTask.Scripts.Architecture.Events.Listeners;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Events.Events
{
    public abstract class ScriptableEvent<T> : ScriptableObject
    {
        private readonly HashSet<ScriptableEventListener<T>> m_listeners = new();

        public void Invoke(T value)
        {
            foreach (var listener in m_listeners)
                listener.Raise(value);
        }

        public void RegisterListener(ScriptableEventListener<T> listener) => m_listeners.Add(listener);
        public void UnregisterListener(ScriptableEventListener<T> listener) => m_listeners.Remove(listener);
    }
}