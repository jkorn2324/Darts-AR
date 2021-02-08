using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.UI
{

    [RequireComponent(typeof(Animator))]
    public class TargetScoreUI : Utils.EventContainerComponent
    {
        [SerializeField]
        private Canvas scoreUI;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private DartHitEvent dartHitEvent;

        private Animator _animator;

        protected override void OnStart()
        {
            this._animator = this.GetComponent<Animator>();
            // this.scoreUI.enabled = false;
        }

        protected override void HookEvents()
        {
            this.dartHitEvent.HookEvent(this.OnDartHit);
        }

        protected override void UnHookEvents()
        {
            this.dartHitEvent.UnHookEvent(this.OnDartHit);
        }

        private void OnDartHit(DartHitOutcome @outcome)
        {
            this._animator.Play("InAnimation");
            if(outcome.throwOutcome == DartThrowOutcome.OUTCOME_HIT)
            {
                this.scoreText.text = "YOU SCORED " + outcome.points;
                return;
            }
            this.scoreText.text = "YOU MISSED";
        }

        /// <summary>
        /// Called when the animation has ended.
        /// </summary>
        private void OnAnimationEnd()
        {
            Debug.Log("Switch player");
        }
    }
}
