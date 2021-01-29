using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARFoundation;

namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// The references used by the object placer.
    /// </summary>
    [System.Serializable]
    public struct ObjectPlacerReferences
    {
        [SerializeField]
        public Utils.References.Vector3Reference indicatorPosition;
        [SerializeField]
        public Utils.References.BooleanReference indicatorVisible;

        [SerializeField]
        public Utils.References.Vector3Reference indicatorRotation;
        [SerializeField]
        public Utils.References.FloatReference indicatorRotationOffset;
    }

    /// <summary>
    /// The values used by the object placer.
    /// </summary>
    [System.Serializable]
    public class ObjectPlacerValues
    {
        [SerializeField]
        private ARRaycastManager raycastManager;
        [SerializeField]
        private float distance = Mathf.Infinity;

        public float Distance => this.distance;

        public ARRaycastManager RaycastManager => this.raycastManager;
    }

    /// <summary>
    /// The object placement controller; controls the position where
    /// the user places an object.
    /// </summary>
    public class ObjectPlacementPositionController
    {
        private ObjectPlacerValues _values;
        private ObjectPlacerReferences _references;

        private ARRaycastManager _raycastManager;

        public ObjectPlacementPositionController
            (ObjectPlacerValues values, ObjectPlacerReferences references)
        {
            this._values = values;
            this._references = references;
            this._raycastManager = this._values.RaycastManager;
        }

        /// <summary>
        /// Updates the position controller.
        /// </summary>
        public void Update()
        {
            // Shoots a raycast from the center of the screen.
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            this._raycastManager.Raycast(
                new Vector2(Screen.width / 2.0f, Screen.height / 2.0f),
                hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            // Determines if we hit a plane.
            if (hits.Count <= 0)
            {
                this._references.indicatorVisible.Value = false;
                return;
            }

            ARRaycastHit firstHit = hits[0];
            this._references.indicatorPosition.Value =
                firstHit.pose.position;
            this._references.indicatorRotation.Value =
                firstHit.pose.rotation.eulerAngles;
            this._references.indicatorVisible.Value = true;
        }
    }


    /// <summary>
    /// The object placer monobehaviour.
    /// </summary>
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField]
        private ObjectPlacerValues values;
        [SerializeField]
        private ObjectPlacerReferences references;

        private ObjectPlacementPositionController _positionController;

        /// <summary>
        /// Called
        /// </summary>
        private void Start()
        {
            this._positionController = new ObjectPlacementPositionController(
                this.values, this.references);
        }

        /// <summary>
		/// Updates the object placer.
		/// </summary>
        private void Update()
        {
            this._positionController?.Update();
        }
    }
}

