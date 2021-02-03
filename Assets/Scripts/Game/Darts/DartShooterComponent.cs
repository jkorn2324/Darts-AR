using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{

    [System.Serializable]
    public struct DartShooterReferences
    {

        // TODO:

    }

    /// <summary>
    /// The dart shooter component.
    /// </summary>
    public class DartShooterComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject dartPrefab;
        [SerializeField]
        private KeyCode shooterKeyCode;


        private void Update()
        {
            if(Input.GetKeyDown(shooterKeyCode))
            {
                this.ShootDart();
            }
        }

        /// <summary>
        /// Shoots the dart.
        /// </summary>
        private void ShootDart()
        {
            Instantiate(this.dartPrefab);
        }
    }
}

