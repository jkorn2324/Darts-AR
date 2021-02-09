using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


namespace ModifiedObject.Scripts.Game
{

    /// <summary>
    /// The component that updates its position according to a target tracker.
    /// </summary>
    public abstract class ATargetComponent : Utils.EventContainerComponent
    {
        [SerializeField]
        private Utils.References.Vector3Reference positionReference;
        [SerializeField]
        private Utils.References.QuaternionReference rotationReference;

        private void Update()
        {
            this.transform.position = this.positionReference.Value;
            this.transform.rotation = this.rotationReference.Value;

            this.OnUpdate();
        }

        protected override void HookEvents() { }

        protected override void UnHookEvents() { }

        protected virtual void OnUpdate() { }
    }

    /// <summary>
    /// The placed target tracker.
    /// </summary>
    public class PlacedTargetTracker : Utils.EventContainerComponent
    {
        [SerializeField]
        private Utils.Events.GameEvent replaceEvent;
        [SerializeField]
        private Utils.References.Vector3Reference positionReference;
        [SerializeField]
        private Utils.References.QuaternionReference rotationReference;
        [SerializeField]
        private Utils.References.BooleanReference foundTarget;

        private ARSession _session;

        private static int numTargetsCount = 0;

        protected override void OnStart()
        {
            this._session = FindObjectOfType<ARSession>();
        }

        protected override void OnEnabled()
        {
            numTargetsCount++;
            if (this.foundTarget.Value)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            this.foundTarget.Value = true;
        }

        protected override void HookEvents()
        {
            this.replaceEvent?.HookEvent(this.OnReplaced);
        }

        protected override void UnHookEvents()
        {
            this.replaceEvent?.UnHookEvent(this.OnReplaced);
        }

        protected override void OnDisabled()
        {
            if(--numTargetsCount <= 0)
            {
                this.foundTarget.Value = false;
            }
        }

        private void OnReplaced()
        {
            // Resets the ar session.
            if(this._session != null)
            {
                this._session.Reset();
            }
            Destroy(this.gameObject);
        }

        private void Update()
        {
            this.positionReference.Value = this.transform.position;
            this.rotationReference.Value = this.transform.rotation;
        }
    }
}

