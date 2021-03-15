using UnityEngine;
using System.Collections;

namespace ModifiedObject.Scripts.Utils
{
    /// <summary>
    /// The event container component.
    /// </summary>
    public abstract class EventContainerComponent : MonoBehaviour
    {
        protected void Start()
        {
            this.OnStart();
            this.HookEvents();
        }

        protected virtual void OnStart() { }

        protected void OnEnable()
        {
            this.OnEnabled();
            this.HookEvents();
        }

        protected virtual void OnEnabled() { }

        abstract protected void HookEvents();

        protected void OnDisable()
        {
            this.OnDisabled();
            this.UnHookEvents();
        }

        abstract protected void UnHookEvents();

        protected virtual void OnDisabled() { }
    }

}
