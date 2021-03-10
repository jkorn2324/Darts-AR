using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.UI
{

    /// <summary>
    /// The UI Main Menu.
    /// </summary>
    public class UIMainMenu : Utils.EventContainerComponent
    {
        [SerializeField]
        private Utils.Events.FloatEvent touchFinishedEvent;

        protected override void HookEvents()
        {
            this.touchFinishedEvent?.HookEvent(this.OnTouchFinished);
        }

        protected override void UnHookEvents()
        {
            this.touchFinishedEvent?.UnHookEvent(this.OnTouchFinished);
        }

        private void OnTouchFinished(float touchTime)
        {
            Debug.Log(touchTime);
        }
    }
}