using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Utils.Events
{

    /// <summary>
    /// The generic event
    /// </summary>
    [CreateAssetMenu(fileName = "Game Event", menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private event System.Action _event
            = delegate { };


        public void HookEvent(System.Action func)
        {
            this._event += func;
        }

        public void UnHookEvent(System.Action func)
        {
            this._event -= func;
        }

        public void CallEvent()
        {
            this._event();
        }
    }
}
