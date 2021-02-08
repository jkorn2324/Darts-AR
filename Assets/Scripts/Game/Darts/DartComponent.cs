using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{

    [System.Serializable]
    public struct DartReferences
    {
        [SerializeField]
        public Utils.References.Vector3Reference originalPosition;
        [SerializeField]
        public Utils.References.Vector3Reference originalDirection;
        [SerializeField]
        public Utils.References.FloatReference originalForce;
        [SerializeField]
        public Utils.References.Vector3Reference targetPosition;
        [SerializeField]
        public Utils.References.QuaternionReference targetRotation;
        [SerializeField]
        public Utils.References.FloatReference despawnCooldownTime;
    }

    [System.Serializable]
    public struct DartEvents
    {
        [SerializeField]
        public DartHitEvent dartHitEvent;
    }

    [RequireComponent(typeof(Rigidbody))]
    public class DartComponent : Utils.EventContainerComponent, IDestroyable
    {
        [SerializeField]
        private DartReferences references;
        [SerializeField]
        private DartEvents events;
        [SerializeField]
        private GameObject pointToTrack;

        private Rigidbody _rigidbody;

        private Vector3 _prevTargetPosition = Vector3.zero;
        private Vector3 _prevTargetEulers = Vector3.zero;

        private float _despawnCooldownTime = 0.0f;
        private bool _hitTarget = false;

        public Vector3 DartPosition
            => this.pointToTrack != null ? this.pointToTrack.transform.position
                : this.transform.position;

        /// <summary>
        /// Called when the dart is started.
        /// </summary>
        protected override void OnStart()
        {
            // Sets the start position.
            this.transform.position = this.references.originalPosition.Value;

            // Sets the look rotation.
            Quaternion quat = this.transform.rotation;
            quat.eulerAngles = this.references.originalDirection.Value;
            this.transform.rotation = quat;

            this._rigidbody = this.GetComponent<Rigidbody>();
            this._rigidbody.AddForce(
                this.transform.forward * this.references.originalForce.Value,
                ForceMode.Impulse);
        }

        protected override void HookEvents()
        {
            this.references.targetPosition.ChangedValueEvent += this.OnTargetChangedPosition;
            this.references.targetRotation.ChangedValueEvent += this.OnTargetChangedRotation;
        }

        protected override void UnHookEvents()
        {
            this.references.targetPosition.ChangedValueEvent -= this.OnTargetChangedPosition;
            this.references.targetRotation.ChangedValueEvent -= this.OnTargetChangedRotation;
        }

        /// <summary>
        /// Updates the dart.
        /// </summary>
        private void Update()
        {
            float totalDespawnCooldownTime = this.references.despawnCooldownTime.Value;
            if(this._hitTarget && this._despawnCooldownTime < totalDespawnCooldownTime)
            {
                this._despawnCooldownTime += Time.deltaTime;

                if(this._despawnCooldownTime >= totalDespawnCooldownTime)
                {
                    // TODO: Disappear object
                    Destroy(this.gameObject);
                }
            }
        }

        /// <summary>
        /// Called when the dart collides with the target.
        /// </summary>
        /// <param name="collision">Gets the collision.</param>
        private void OnCollisionEnter(Collision collision)
        {
            TargetComponent target = collision.gameObject.GetComponent<TargetComponent>()
                ?? collision.gameObject.GetComponentInParent<TargetComponent>();
            if(target != null && !this._hitTarget)
            {
                this._rigidbody.velocity = Vector3.zero;
                this._rigidbody.useGravity = false;
                this._rigidbody.constraints = RigidbodyConstraints.FreezeAll;

                this._hitTarget = true;
                this._despawnCooldownTime = this.references.despawnCooldownTime.Value;

                this._prevTargetPosition = target.transform.position;
                this._prevTargetEulers = target.transform.eulerAngles;

                this.CallDartHitEvent(DartThrowOutcome.OUTCOME_HIT, target.OnDartCollide(this));
            }
        }

        /// <summary>
        /// Called when this object has been his by the object destoyer.
        /// </summary>
        /// <param name="destroyer">The object destroyer.</param>
        public void OnDestroyerHit(ObjectDestroyer destroyer)
        {
            this.CallDartHitEvent(DartThrowOutcome.OUTCOME_MISSED);
            Destroy(this.gameObject);
        }

        private void OnTargetChangedPosition(Vector3 targetPosition)
        {
            if(!this._hitTarget)
            {
                this._prevTargetPosition = targetPosition;
                return;
            }

            Vector3 newTargetDifference = targetPosition - this._prevTargetPosition;
            this.transform.position += newTargetDifference;
            this._prevTargetPosition = targetPosition;
        }

        private void OnTargetChangedRotation(Quaternion targetRotation)
        {
            if(!this._hitTarget)
            {
                this._prevTargetEulers = targetRotation.eulerAngles;
                return;
            }

            Vector3 newTargetDifference = targetRotation.eulerAngles - this._prevTargetEulers;
            Quaternion quat = this.transform.rotation;
            quat.eulerAngles += newTargetDifference;
            this.transform.rotation = quat;
            this._prevTargetEulers = targetRotation.eulerAngles;
        }

        private void CallDartHitEvent(DartThrowOutcome outcome, int points = 0)
        {
            DartHitOutcome hitOutcome = new DartHitOutcome();
            hitOutcome.points = points;
            hitOutcome.throwOutcome = outcome;
            this.events.dartHitEvent?.CallEvent(hitOutcome);
        }
    }
}
