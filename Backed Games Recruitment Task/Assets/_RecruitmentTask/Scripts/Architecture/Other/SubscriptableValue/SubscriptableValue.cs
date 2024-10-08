using System;
using UnityEngine;

namespace Assets.Repos.Codebase.Code.Core.SubscripableValue
{
    /// <summary>
    /// A value wrapper that enables adding listeners to it so they will be informed of every value changes.
    /// </summary>
    /// <typeparam name="T">Type of the wrapped value.</typeparam>
    [Serializable]
    public class SubscriptableValue<T> : SubscriptableValueBase
    {
        /// <summary>
        /// The wrapped value.
        /// </summary>
        [Header("Properties")]
        [SerializeField] protected T m_value;
        [SerializeField] protected bool m_callOnSameValue;

        [Header("Debug")]
        [SerializeField] public bool DebugLogValueOnChange;
        [SerializeField] public string DebugLogValueName = string.Empty;

        /// <summary>
        /// Base constructor.
        /// </summary>
        public SubscriptableValue() : base(true)
        {
            Owner = null;
        }

        /// <summary>
        /// Constructor with owner and calling events.
        /// </summary>
        /// <param name="callingEventsEnabled">Set if listeners should be informed of changes from the start.</param>
        /// <param name="callOnSameValue">Call listeners if new set value is same as old one. It won't call if new and current value are the default value of T.</param>
        /// <param name="owner">Owner object of the value.</param>
        public SubscriptableValue(bool callingEventsEnabled, bool callOnSameValue = false, object owner = null) : base(callingEventsEnabled)
        {
            Owner = owner;
            m_callOnSameValue = callOnSameValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callingEventsEnabled">Set if listeners should be informed of changes from the start.</param>
        /// <param name="startValue">Starting value of the variable.</param>
        /// <param name="owner">Owner object of the value.</param>
        public SubscriptableValue(bool callingEventsEnabled, T startValue, object owner = null) : base(callingEventsEnabled)
        {
            m_value = startValue;
            Owner = owner;
        }

        #region Set

        /// <summary>
        /// Basic setter.
        /// </summary>
        public virtual T Value
        {
            get => m_value;

            set
            {
                if (!IsNewValueDifferent(value))
                    return;

                if (DebugLogValueOnChange)
                    Debug.Log($"{DebugLogValueName} new value: {value}");

                m_value = value;

                if (CallingEventsEnabled)
                    CallValueChanged();
            }
        }

        /// <summary>
        /// Set the wrapped value as an generic object.
        /// </summary>
        /// <param name="value">New value.</param>
        public override void SetValueAsObject(object value) { Value = (T)value; }

        #endregion

        #region Get

        /// <summary>
        /// Get the type of generic value - mostly used when getting the wrapper from onValueChanged listening.
        /// </summary>
        /// <returns>Type of value stored in wrapper.</returns>
        public override Type GetValueType() => typeof(T);

        /// <summary>
        /// Get the wrapped value as generic an object.
        /// </summary>
        /// <returns>Wrapped value as an object.</returns>
        public override object GetValueAsObject() => Value;

        #endregion

        /// <summary>
        /// Checks if new value is different as current.
        /// </summary>
        /// <param name="newValue">New value</param>
        /// <returns>True if new value is different.</returns>
        protected bool IsNewValueDifferent(T newValue)
        {
            var oldValue = GetValueAsObject();

            // Both are null
            if (newValue == null && oldValue == null)
                return false;

            // New is null, but old is not, so they are different
            if (newValue == null)
                return true;

            // If both are same
            if (newValue.Equals(oldValue))
            {
                // But can call on same if bot are not default, if one is default, then both are
                return m_callOnSameValue && !newValue.Equals(default(T));
            }
            
            // If both are different
            return true;
        }
    }
}