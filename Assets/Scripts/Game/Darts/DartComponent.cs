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
    }

    [RequireComponent(typeof(Rigidbody))]
    public class DartComponent : MonoBehaviour
    {
        [SerializeField]
        private DartReferences references;

        private Rigidbody _rigidbody;

        /// <summary>
        /// Called when the dart is started.
        /// </summary>
        private void Start()
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

        /// <summary>
        /// Called when the dart collides with the target.
        /// </summary>
        /// <param name="collision">Gets the collision.</param>
        private void OnCollisionEnter(Collision collision)
        {
            TargetComponent target = collision.gameObject.GetComponent<TargetComponent>();
            if(target != null)
            {
                this._rigidbody.velocity = Vector3.zero;
                this._rigidbody.useGravity = false;

                target.OnDartCollide(this);
            }
        }
    }
}
