using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.UI
{
    /// <summary>
    /// The tracking change ui.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UIChangeTracking : Utils.EventContainerComponent
    {
        [SerializeField]
        private Misc.TargetPlacerReference placerReference;
        [SerializeField]
        private Utils.References.BooleanReference foundTarget;

        private Canvas _canvas;

        protected override void OnStart()
        {
            this._canvas = this.GetComponent<Canvas>();
        }

        public void ChangeTrackingType(int targetPlacer)
        {
            Misc.TargetPlacerType placerType = (Misc.TargetPlacerType)targetPlacer;
            this.placerReference.Value = placerType;
        }

        protected override void HookEvents()
        {
            this.foundTarget.ChangedValueEvent += this.OnFoundTarget;
        }

        protected override void UnHookEvents()
        {
            this.foundTarget.ChangedValueEvent -= this.OnFoundTarget;
        }

        private void OnFoundTarget(bool foundTarget)
        {
            this._canvas.enabled = !foundTarget;
        }
    }
}


