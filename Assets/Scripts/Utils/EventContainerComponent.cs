using UnityEngine;
using System.Collections;

namespace ModifiedObject.Scripts.Utils
{
    /// <summary>
    /// The event container component.
    /// </summary>
    public abstract class EventContainerComponent : MonoBehaviour
    {
        private bool _hasHookedEvents = false;
        private bool _hasStarted = false;

        public bool HasHookedEvents
            => this._hasHookedEvents;

        protected void Start()
        {
            if(!this._hasHookedEvents || !this._hasStarted)
            {
                this.HookEvents();
                this._hasHookedEvents = true;
            }
            this.OnStart();
            this._hasStarted = true;
        }

        protected virtual void OnStart() { }

        protected void OnEnable()
        {
            if(!this._hasHookedEvents && this._hasStarted)
            {
                this.HookEvents();
                this._hasHookedEvents = true;
            }
            this.OnEnabled();
        }

        protected virtual void OnEnabled() { }

        abstract protected void HookEvents();

        protected void OnDisable()
        {
            if(this._hasHookedEvents)
            {
                this.UnHookEvents();
                this._hasHookedEvents = false;
            }
            this.OnDisabled();
        }

        abstract protected void UnHookEvents();

        protected virtual void OnDisabled() { }
    }

}
