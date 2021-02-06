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
    }

    /// <summary>
    /// The HUD Canvas.
    /// </summary>
    public class HUDCanvas : Utils.EventContainerComponent
    {
        [SerializeField]
        private IntegerTextUpdater score;
        [SerializeField]
        private HUDReferences references;


        protected override void HookEvents()
        {
            this.score.HookEvent();
        }

        protected override void UnHookEvents()
        {
            this.score.UnHookEvent();
        }

        /// <summary>
        /// Called when the shoot button has been selected.
        /// </summary>
        public void OnShootButtonSelected()
        {
            this.references.shootEvent?.CallEvent();
        }
    }
}

