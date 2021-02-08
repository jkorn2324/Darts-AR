using UnityEngine;
using UnityEngine.UI;

namespace ModifiedObject.Scripts.Game.UI
{
    [RequireComponent(typeof(Animator))]
    public class PlayerSwitchedUI : Utils.EventContainerComponent
    {
        [SerializeField]
        private Utils.Events.StringEvent switchedEvent;
        [SerializeField]
        private Text playerText;

        private Animator _animator;

        protected override void OnStart()
        {
            this._animator = this.GetComponent<Animator>();
        }

        protected override void HookEvents()
        {
            this.switchedEvent?.HookEvent(this.OnSwitchedPlayer);
        }

        protected override void UnHookEvents()
        {
            this.switchedEvent?.UnHookEvent(this.OnSwitchedPlayer);
        }

        private void OnSwitchedPlayer(string playerName)
        {
            // tODO: Begin animation
            this.playerText.text = playerName.ToUpper() + " IS NOW UP";
        }
    }
}
