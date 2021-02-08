using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedObject.Scripts.Game.UI
{

    [System.Serializable]
    public struct HUDReferences
    {
        [SerializeField]
        public Utils.Events.GameEvent shootEvent;
        [SerializeField]
        public Utils.References.BooleanReference targetFound;
    }

    /// <summary>
    /// The HUD Canvas.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class HUDCanvas : Utils.EventContainerComponent
    {
        [SerializeField]
        private IntegerTextReferenceUpdater score;
        [SerializeField]
        private HUDReferences references;

        private Canvas _canvas;

        protected override void OnStart()
        {
            this._canvas = this.GetComponent<Canvas>();
            this.OnFoundTargetChanged(this.references.targetFound.Value);
        }

        protected override void HookEvents()
        {
            this.score.HookEvent();
            this.references.targetFound.ChangedValueEvent += this.OnFoundTargetChanged;
        }

        protected override void UnHookEvents()
        {
            this.score.UnHookEvent();
            this.references.targetFound.ChangedValueEvent -= this.OnFoundTargetChanged;
        }

        /// <summary>
        /// Called when the shoot button has been selected.
        /// </summary>
        public void OnShootButtonSelected()
        {
            this.references.shootEvent?.CallEvent();
        }

        private void OnFoundTargetChanged(bool foundTarget)
        {
            this._canvas.enabled = foundTarget;
        }
    }
}

