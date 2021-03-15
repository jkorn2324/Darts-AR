using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Utils.Events
{

    /// <summary>
    /// The generic event
    /// </summary>
    public abstract class GenericEvent<T> : ScriptableObject
    {

        private EventDelegate<T> _event
            = new EventDelegate<T>();

        public void HookEvent(System.Action<T> func)
        {
            this._event += func;
        }

        public void UnHookEvent(System.Action<T> func)
        {
            this._event -= func;
        }

        public void CallEvent(T var)
        {
            this._event.Invoke(var);
        }
    }
}

