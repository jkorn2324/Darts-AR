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


        public event System.Action<T> ChangedValueEvent
            = delegate { };


        public T Value
        {
            get => this.value;
            set
            {
                if(!this.value.Equals(value))
                {
                    this.ChangedValueEvent(value);
                }
                this.value = value;
            }
        }

        public void Reset()
        {
            this.value = this.originalValue;
        }
    }
}

