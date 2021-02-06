using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{

    [System.Serializable]
    public struct DartShooterReferences
    {
        [SerializeField]
        public Utils.References.BooleanReference foundTarget;
        [SerializeField]
        public Utils.Events.GameEvent shootEvent;
    }

    /// <summary>
    /// The dart shooter component.
    /// </summary>
    public class DartShooterComponent : Utils.EventContainerComponent
    {
        [SerializeField]
        private DartShooterReferences references;
        [SerializeField]
        private GameObject dartPrefab;

        protected override void HookEvents()
        {
            Debug.Log("Hook Event");
            this.references.shootEvent?.HookEvent(this.ShootDart);
        }

        protected override void UnHookEvents()
        {
            this.references.shootEvent?.UnHookEvent(this.ShootDart);
        }

        /// <summary>
        /// Shoots the dart.
        /// </summary>
        public void ShootDart()
        {
            if(!references.foundTarget.Value)
            {
                return;
            }
            Instantiate(this.dartPrefab);
        }
    }
}

