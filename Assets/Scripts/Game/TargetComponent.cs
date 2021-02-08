using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ModifiedObject.Scripts.Game
{
    [System.Serializable]
    public struct TargetReferences
    {
        [SerializeField]
        public Utils.References.BooleanReference foundTarget;
        [SerializeField]
        public Utils.References.IntegerReference score;
        [SerializeField]
        public Utils.References.Vector3Reference targetPosition;
        [SerializeField]
        public Utils.References.Vector3Reference targetRotation;
        [SerializeField]
        public Utils.References.Vector3Reference targetFacing;
    }

    [System.Serializable]
    public struct TargetScore
    {
        [SerializeField, Range(0.0f, 100.0f)]
        private float maxDistancePercentage;
        [SerializeField]
        private int score;

        public int Score => this.score;

        public float MaxDistancePercentage
            => this.maxDistancePercentage / 100.0f;
    }

    [System.Serializable]
    public struct TargetValues
    {
        /// <summary>
        /// The Target sorter class.
        /// </summary>
        class TargetSorter : Comparer<TargetScore>
        {
            public override int Compare(TargetScore x, TargetScore y)
            {
                if(x.MaxDistancePercentage < y.MaxDistancePercentage)
                {
                    return 1;
                }
                else if (x.MaxDistancePercentage > y.MaxDistancePercentage)
                {
                    return -1;
                }
                return 0;
            }
        };

        [SerializeField]
        public float maxTargetRadius;
        [SerializeField]
        public int baseScore;
        [SerializeField]
        private List<TargetScore> scores;

        /// <summary>
        /// Sorts the target scores by distance.
        /// </summary>
        /// <param name="outScores">The scores.</param>
        public void GetTargetScores(ref List<TargetScore> outScores)
        {
            foreach(TargetScore s in scores)
            {
                outScores.Add(s);
            }
            outScores.Sort(new TargetSorter());
        }
    }

    /// <summary>
    /// The Target Component.
    /// </summary>c
    public class TargetComponent : MonoBehaviour
    {
        [SerializeField]
        private TargetReferences references;
        [SerializeField]
        private TargetValues values;

        private List<TargetScore> _scores;

        /// <summary>
        /// Called when the target has started.
        /// </summary>
        private void Start()
        {
            this._scores = new List<TargetScore>();
            this.values.GetTargetScores(ref this._scores);

            foreach(TargetScore s in this._scores)
            {
                Debug.Log(s.Score);
            }
        }

        /// <summary>
        /// Called when the target is enabled.
        /// </summary>
        private void OnEnable()
        {
            this.references.foundTarget.Value = true;
        }

        /// <summary>
        /// Called when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            this.references.foundTarget.Value = false;
        }

        private void Update()
        {
            this.references.targetPosition.Value = this.transform.position;
            this.references.targetRotation.Value = this.transform.eulerAngles;
            this.references.targetFacing.Value = this.transform.forward;
        }

        /// <summary>
        /// Called when the Dart hit the target.
        /// </summary>
        /// <param name="component">The dart component.</param>
        public int OnDartCollide(DartComponent component)
        {
            if(component == null)
            {
                return 0;
            }

            Vector3 center = this.transform.position;
            float distanceFromCenter = Mathf.Abs(Vector3.Distance(center, component.DartPosition));
            float percentageTargetRadius = distanceFromCenter / values.maxTargetRadius;
            int newScore = this.CalculateScore(percentageTargetRadius);
            int oldScore = this.references.score.Value;
            this.references.score.Value += newScore;
            return newScore - oldScore;
        }

        /// <summary>
        /// Calculates the score of the target.
        /// </summary>
        /// <param name="percentageFromCenter">The percentage from the center.</param>
        /// <returns>A float with the new score.</returns>
        private int CalculateScore(float percentageFromCenter)
        {
            if(this._scores.Count <= 0)
            {
                return this.values.baseScore;
            }

            if(this._scores.Count == 1)
            {
                TargetScore firstScore = this._scores[0];
                return percentageFromCenter <= firstScore.MaxDistancePercentage ?
                    firstScore.Score : this.values.baseScore;
            }
            // Recursively calculates the score.
            return this.CalculateScoreRecursion(
                percentageFromCenter, this.values.baseScore, 0);
        }

        /// <summary>
        /// Recursively calculates the score.
        /// </summary>
        /// <param name="percentageFromCenter">The percentage from center.</param>
        /// <param name="currentScore">The current score.</param>
        /// <param name="currentIndex">The current index.</param>
        /// <returns>The score.</returns>
        private int CalculateScoreRecursion(float percentageFromCenter, int currentScore, int currentIndex)
        {
            if(currentIndex >= this._scores.Count)
            {
                return currentScore;
            }

            TargetScore score = this._scores[currentIndex];
            if(percentageFromCenter <= score.MaxDistancePercentage)
            {
                return this.CalculateScoreRecursion(
                    percentageFromCenter, score.Score, ++currentIndex);
            }
            return currentScore;
        }
    }
}