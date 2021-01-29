using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedObject.Scripts.Game
{
    [RequireComponent(typeof(Camera))]
    public class CameraUpdater : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.Vector3Reference position;
        [SerializeField]
        private Utils.References.Vector3Reference rotation;
        [SerializeField]
        private Utils.References.Vector3Reference forwardDirection;

        /// <summary>
        /// Updates the camera.
        /// </summary>
        private void Update()
        {
            this.position.Value = this.transform.position;
            this.rotation.Value = this.transform.rotation.eulerAngles;
            this.forwardDirection.Value = this.transform.forward;
        }
    }
}

