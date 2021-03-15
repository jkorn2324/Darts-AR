using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Utils.Variables
{
    /// <summary>
    /// The generic variable definition.
    /// </summary>
    /// <typeparam name="T">The generic variable.</typeparam>
    public abstract class GenericVariable<T> : ScriptableObject
    {
        [SerializeField]
        private T originalValue;
        [SerializeField]
        private T value;


        private EventDelegate<T> _changedValueEvent
            = new EventDelegate<T>();

        public T Value
        {
            get => this.value;
            set
            {
                if(!this.value.Equals(value))
                {
                    this._changedValueEvent.Invoke(value);
                }
                this.value = value;
            }
        }

        public void AddChangedValueEventCallback(System.Action<T> func)
        {
            this._changedValueEvent.AddCallback(func);
        }

        public void RemoveChangedValueEventCallback(System.Action<T> func)
        {
            this._changedValueEvent.RemoveCallback(func);
        }

        public void Reset()
        {
            this.value = this.originalValue;
        }
    }
}

