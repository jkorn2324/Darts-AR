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
    }

    /// <summary>
    /// The dart shooter component.
    /// </summary>
    public class DartShooterComponent : MonoBehaviour
    {
        [SerializeField]
        private DartShooterReferences references;
        [SerializeField]
        private GameObject dartPrefab;

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

