using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public class PlacedTargetTracker : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.Vector3Reference positionReference;
        [SerializeField]
        private Utils.References.QuaternionReference rotationReference;
        [SerializeField]
        private Utils.References.BooleanReference foundTarget;

        private void OnEnable()
        {
            if(this.foundTarget.Value)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            this.foundTarget.Value = true;
        }

        private void OnDisable()
        {
            this.foundTarget.Value = false;
        }

        private void Update()
        {
            this.positionReference.Value = this.transform.position;
            this.rotationReference.Value = this.transform.rotation;
        }
    }
}

