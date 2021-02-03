using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// The placement indicator.
    /// </summary>
    public class PlacementIndicator : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.Vector3Reference indicatorPosition;
        [SerializeField]
        private Utils.References.Vector3Reference indicatorRotation;
        [SerializeField]
        private Utils.References.BooleanReference indicatorVisible;

        [SerializeField]
        private GameObject indicatorVisuals;

        /// <summary>
        /// Updates the position of the indicator.
        /// </summary>
        private void Update()
        {
            this.transform.position = this.indicatorPosition.Value;
            this.SetEulers(this.indicatorRotation.Value);

            if(this.indicatorVisuals != null)
            {
                this.indicatorVisuals.SetActive(this.indicatorVisible.Value);
            }
        }

        /// <summary>
        /// Sets the euler angles.
        /// </summary>
        /// <param name="eulers">The euler angles.</param>
        private void SetEulers(Vector3 eulers)
        {
            Quaternion quat = this.transform.rotation;
            quat.eulerAngles = eulers;
            this.transform.rotation = quat;
        }
    }
}
