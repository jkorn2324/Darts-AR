using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// Class inherited by objects that can be destroyed.
    /// </summary>
    public interface IDestroyable
    {

        /// <summary>
        /// The destroyer that was hit.
        /// </summary>
        /// <param name="destroyer">The destroyer that hit the object.</param>
        void OnDestroyerHit(ObjectDestroyer destroyer);
    }

    [RequireComponent(typeof(Collider))]
    public class ObjectDestroyer : MonoBehaviour
    {
        /// <summary>
        /// Called when an object has collided with this trigger.
        /// </summary>
        /// <param name="other">The other collider.</param>
        private void OnTriggerEnter(Collider other)
        {
            IDestroyable destroyable = other.attachedRigidbody.GetComponent<IDestroyable>();
            if(destroyable != null)
            {
                destroyable.OnDestroyerHit(this);
            }
        }
    }
}
