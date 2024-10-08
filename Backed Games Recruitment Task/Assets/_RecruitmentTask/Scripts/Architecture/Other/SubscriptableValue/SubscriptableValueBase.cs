using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Repos.Codebase.Code.Core.SubscripableValue
{
    /// <summary>
    /// Base of a value wrapper that gives a mechanism for observing value changes. Listeners will be notified when value's value will be changed and this base class will be send as parameter.
    /// </summary>
    [Serializable]
    public abstract class SubscriptableValueBase
    {
        /// <summary>
        /// Owner script/object of the value.
        /// </summary>
        [SerializeField] public object Owner = null;

        [Header("Events")]
        /// <summary>
        /// Enable or disable calling listeners when value change.
        /// </summary>
        [SerializeField] private bool m_callingEventsEnabled = true;

        public bool CallingEventsEnabled
        {
            get => m_callingEventsEnabled;
            set => m_callingEventsEnabled = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private bool m_listenerRemovedDuringEvent;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private int m_isCallingCounter;

        /// <summary>
        /// Collection of all listeners waiting for value change.
        /// </summary>
        [SerializeField] private List<Action<SubscriptableValueBase>> m_onValueChangedListeners = new List<Action<SubscriptableValueBase>>();

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="callingEventsEnabled">Enable or disable calling listeners when value change.</param>
        public SubscriptableValueBase(bool callingEventsEnabled)
        {
            m_callingEventsEnabled = callingEventsEnabled;
        }

        #region Get

        /// <summary>
        /// Get the type of generic value - mostly used when getting the wrapper from onValueChanged listening.
        /// </summary>
        /// <returns>Type of value stored in wrapper.</returns>
        public abstract Type GetValueType();

        /// <summary>
        /// Get the wrapped value as generic an object.
        /// </summary>
        /// <returns>Wrapped value as an object.</returns>
        public abstract object GetValueAsObject();

        /// <summary>
        /// Get the wrapped value as object of type T.
        /// </summary>
        /// <typeparam name="T">>Wrapped value as an object cast to type T.</typeparam>
        /// <returns></returns>
        public T GetValueAs<T>() => (T)GetValueAsObject();

        #endregion

        #region Set

        /// <summary>
        /// Set the wrapped value as an generic object.
        /// </summary>
        /// <param name="value">New value.</param>
        public abstract void SetValueAsObject(object value);

        #endregion

        #region Events

        /// <summary>
        /// Inform all the listeners that value has changed and send them new value in wrapped form.
        /// </summary>
        protected void CallValueChanged()
        {
            m_isCallingCounter++;

            for (var i = 0; i < m_onValueChangedListeners.Count; ++i)
                m_onValueChangedListeners[i]?.Invoke(this);
        
            m_isCallingCounter--;

            if (m_isCallingCounter > 0) 
                return;

            if (!m_listenerRemovedDuringEvent) 
                return;
        
            m_listenerRemovedDuringEvent = false;

            for (var i = m_onValueChangedListeners.Count - 1; i >= 0; i--)
            {
                if (m_onValueChangedListeners[i] != null)
                    continue;

                m_onValueChangedListeners.RemoveAt(i);
            }
        }

        /// <summary>
        /// Inform all listeners manually and send them current value in wrapped form.
        /// </summary>
        public void CallListenersManually()
        {
            CallValueChanged();
        }

        /// <summary>
        /// Add a listener that will be informed when value will be changed.
        /// </summary>
        /// <param name="listener">The listener that will be called every value change.</param>
        /// <param name="callNow">Defines if listener should be called after adding, so it will get the current value.</param>
        public void AddChangedListener(Action<SubscriptableValueBase> listener, bool callNow = true)
        {
            m_onValueChangedListeners.Add(listener);

            if (CallingEventsEnabled && callNow)
                listener.Invoke(this);
        }

        /// <summary>
        /// Add a listener that will be informed when value will be changed, if it is not already added.
        /// </summary>
        /// <param name="listener">The listener that will be called every value change.</param>
        /// <param name="callNow">Defines if listener should be called after adding, so it will get the current value.</param>
        public void AddChangedListenerIfNotAdded(Action<SubscriptableValueBase> listener, bool callNow = true)
        {
            if (m_onValueChangedListeners.Contains(listener))
                return;

            m_onValueChangedListeners.Add(listener);

            if (CallingEventsEnabled && callNow)
                listener.Invoke(this);
        }

        /// <summary>
        /// Remove the listener from value.
        /// </summary>
        /// <param name="listener">Listener which was listening for value changes.</param>
        public void RemoveChangedListener(Action<SubscriptableValueBase> listener)
        {
            for (var i = 0; i < m_onValueChangedListeners.Count; ++i)
            {
                if (m_onValueChangedListeners[i] != listener) 
                    continue;

                if (m_isCallingCounter > 0)
                {
                    m_onValueChangedListeners[i] = null;
                    m_listenerRemovedDuringEvent = true;
                }
                else
                {
                    m_onValueChangedListeners.RemoveAt(i);
                }

                return;
            }
        }

        #endregion
    }
}
