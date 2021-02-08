using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// The Dart Throw outcome.
    /// </summary>
    public enum DartThrowOutcome
    {
        OUTCOME_MISSED,
        OUTCOME_HIT
    }

    /// <summary>
    /// The Dart Hit Outcome.
    /// </summary>
    public struct DartHitOutcome
    {
        public DartThrowOutcome throwOutcome;
        public int points;

        public DartHitOutcome(DartThrowOutcome outcome, int points)
        {
            this.throwOutcome = outcome;
            this.points = points;
        }
    }

    [CreateAssetMenu(fileName = "Dart Hit Event", menuName = "Events/Darts/Dart Hit Event")]
    public class DartHitEvent : Utils.Events.GenericEvent<DartHitOutcome> { }
}

