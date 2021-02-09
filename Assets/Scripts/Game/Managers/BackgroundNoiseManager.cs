using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundNoiseManager : Utils.EventContainerComponent
    {
        [SerializeField]
        private AudioClip scoreSound;
        [SerializeField]
        private AudioClip noScoreSound;

        [SerializeField]
        private DartHitEvent dartHitEvent;

        private AudioSource _audioClip;

        protected override void OnStart()
        {
            this._audioClip = this.GetComponent<AudioSource>();
        }

        protected override void HookEvents()
        {
            this.dartHitEvent?.HookEvent(this.DartHitEvent);
        }

        protected override void UnHookEvents()
        {
            this.dartHitEvent?.UnHookEvent(this.DartHitEvent);
        }

        private void DartHitEvent(DartHitOutcome outcome)
        {
            this._audioClip.clip = (outcome.points > 0) ?
                this.scoreSound : this.noScoreSound;
            this._audioClip.Play();
        }
    }
}
