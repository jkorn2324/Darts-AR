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
        [SerializeField]
        private Utils.References.BooleanReference canShoot;

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
            this._animator.Play("InTransition");
            this.playerText.text = playerName.ToUpper() + " IS NOW UP";
        }

        /// <summary>
        /// Called when the animation has ended.
        /// </summary>
        private void OnAnimationEnd()
        {
            this.canShoot.Value = true;
        }
    }
}
