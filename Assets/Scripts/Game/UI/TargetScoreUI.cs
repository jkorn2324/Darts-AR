using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.UI
{

    public class TargetScoreUI : Utils.EventContainerComponent
    {
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private DartHitEvent dartHitEvent;

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
            if(outcome.throwOutcome == DartThrowOutcome.OUTCOME_HIT)
            {
                this.scoreText.text = "YOU SCORED " + outcome.points;
                return;
            }
            this.scoreText.text = "YOU MISSED";
        }
    }
}
