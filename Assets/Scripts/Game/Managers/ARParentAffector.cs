using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ModifiedObject.Scripts.Game
{

    [System.Serializable]
    public class AffectedObjectType
    {
        [SerializeField]
        private List<Misc.TargetPlacerType> activeTargetTypes;
        [SerializeField]
        private GameObject affectedObject;

        private bool _prevActiveState = true;

        public void OnFoundTargetChanged(bool foundTarget, Misc.TargetPlacerType placerType)
        {
            if(this.affectedObject == null)
            {
                return;
            }

            if(!this.activeTargetTypes.Contains(placerType))
            {
                this.affectedObject.SetActive(false);
                return;
            }

            if(!foundTarget)
            {
                this._prevActiveState = this.affectedObject.activeSelf;
                this.affectedObject.SetActive(foundTarget);
            }
            else
            {
                this.affectedObject.SetActive(this._prevActiveState);   
            }
        }
    }

    /// <summary>
    /// Affects each of these objects based on whether or not the target exists.
    /// </summary>
    public class ARParentAffector : Utils.EventContainerComponent
    {
        [SerializeField]
        private Utils.References.BooleanReference foundTarget;
        [SerializeField]
        private Misc.TargetPlacerReference targetPlacerReference;
        [SerializeField]
        private List<AffectedObjectType> objectsAffected;

        protected override void OnStart()
        {
            this.OnFoundTargetChanged(this.foundTarget.Value);
        }

        protected override void HookEvents()
        {
            this.foundTarget.ChangedValueEvent += this.OnFoundTargetChanged;
        }

        protected override void UnHookEvents()
        {
            this.foundTarget.ChangedValueEvent -= this.OnFoundTargetChanged;
        }

        private void OnFoundTargetChanged(bool changed)
        {
            foreach (AffectedObjectType objectType in this.objectsAffected)
            {
                objectType.OnFoundTargetChanged(changed, this.targetPlacerReference.Value);
            }   
        }
    }
}

