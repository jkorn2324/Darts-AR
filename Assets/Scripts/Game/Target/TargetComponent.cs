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
        public Utils.References.IntegerReference score;
    }

    [System.Serializable]
    public class TargetScoreMultiplier
    {
        [SerializeField]
        public bool IsBullseye = false;
        [SerializeField]
        public bool IsMultiplier = true;
        [SerializeField]
        public int scoreIfNotMultiplier;
        [SerializeField, Range(0.0f, 100.0f)]
        private float maxDistancePercentage;
        [SerializeField]
        private float scoreMultiplier;

        public float ScoreMultiplier => this.scoreMultiplier;

        public float MaxDistancePercentage
            => this.maxDistancePercentage / 100.0f;


        public TargetScoreMultiplier()
        {
            this.maxDistancePercentage = 100.0f;
        }
    }

    [System.Serializable]
    public struct TargetScoreSection
    {
        [SerializeField, Range(0, 360)]
        private float minAngle;
        [SerializeField]
        private float differenceMaxAngle;
        [SerializeField]
        public int score;

        /// <summary>
        /// Determines if the angle is within each other.
        /// </summary>
        /// <param name="angle">The angle in degrees from 0-360</param>
        /// <returns>True if it is, false otherwise.</returns>
        public bool IsWithinAngle(float angle)
        {
            float maxAngle = differenceMaxAngle + minAngle;
            if(maxAngle > 360)
            {
                float minAngleChecker = 0;
                float maxAngleChecker = maxAngle - 360;
                if(angle >= minAngleChecker && angle <= maxAngleChecker)
                {
                    return true;
                }
                return angle >= minAngle && angle <= 360;
            }

            return angle >= minAngle && angle <= maxAngle;
        }
    }

    [System.Serializable]
    public struct TargetValues
    {
        /// <summary>
        /// The Target sorter class.
        /// </summary>
        class TargetSorter : Comparer<TargetScoreMultiplier>
        {
            public override int Compare(TargetScoreMultiplier x, TargetScoreMultiplier y)
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
        public List<TargetScoreSection> sections;
        [SerializeField]
        private List<TargetScoreMultiplier> scoreMultipliers;

        /// <summary>
        /// Sorts the target scores by distance.
        /// </summary>
        /// <param name="outScores">The scores.</param>
        public void GetTargetScores(ref List<TargetScoreMultiplier> outScores)
        {
            foreach(TargetScoreMultiplier s in scoreMultipliers)
            {
                outScores.Add(s);
            }
            outScores.Sort(new TargetSorter());
        }
    }

    [System.Serializable]
    public class TargetCenterPoints
    {
        [SerializeField]
        private List<GameObject> centerPoints;

        public float GetShortestDistance(Vector3 point, ref Vector3 centerPoint)
        {
            float maxDistance = float.PositiveInfinity;
            foreach(GameObject gameObject in this.centerPoints)
            {
                Vector3 position = gameObject.transform.position;
                float distanceFromCenter = Mathf.Abs(
                    Vector3.Distance(position, point));
                if(distanceFromCenter < maxDistance)
                {
                    centerPoint = position;
                    maxDistance = distanceFromCenter;
                }
            }
            return maxDistance;
        }
    }

    /// <summary>
    /// The Target Component.
    /// </summary>c
    public class TargetComponent : ATargetComponent
    {
        [SerializeField]
        private TargetReferences references;
        [SerializeField]
        private TargetValues values;
        [SerializeField]
        private TargetCenterPoints centerPoints;

        private List<TargetScoreMultiplier> _scores;

        /// <summary>
        /// Called when the target has started.
        /// </summary>
        protected override void OnStart()
        {
            this._scores = new List<TargetScoreMultiplier>();
            this.values.GetTargetScores(ref this._scores);
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
            Vector3 dartPosition = component.DartPosition;
            float angleInDeg = this.GetAngleFromPoint(dartPosition) * Mathf.Rad2Deg;
            float distanceFromCenter = this.centerPoints.GetShortestDistance(
                dartPosition, ref center);
            float percentageTargetRadius = distanceFromCenter / values.maxTargetRadius;
            int newScore = this.CalculateScore(percentageTargetRadius, angleInDeg);
            this.references.score.Value += newScore;
            return newScore;
        }

        /// <summary>
        /// Calculates the score of the target.
        /// </summary>
        /// <param name="percentageFromCenter">The percentage from the center.</param>
        /// <returns>A float with the new score.</returns>
        private int CalculateScore(float percentageFromCenter, float angleInDeg)
        {
            if(this.values.sections.Count <= 0)
            {
                return this.values.baseScore;
            }

            float angleInDegreesClamped = angleInDeg;
            if (angleInDegreesClamped < 0) angleInDegreesClamped += 360;

            TargetScoreSection? section = this.GetSectionFromAngle(
                angleInDegreesClamped);
            TargetScoreMultiplier multiplier = new TargetScoreMultiplier();
            multiplier = this.CalculateScoreRecursion(
                percentageFromCenter, multiplier, 0);

            int score = 0;
            if(section.HasValue)
            {
                Debug.Log(percentageFromCenter);
                Debug.Log(multiplier.IsMultiplier);
                Debug.Log(multiplier.scoreIfNotMultiplier);

                if (multiplier.IsMultiplier)
                {
                    score = (int)(section.Value.score * multiplier.ScoreMultiplier);
                }
                else
                {
                    score = (int)multiplier.scoreIfNotMultiplier;
                }
            }
            return score;
        }

        /// <summary>
        /// Gets the angle from a certain point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The angle in radians</returns>
        private float GetAngleFromPoint(Vector3 point)
        {
            Vector3 difference = point - this.transform.position;
            float angle = Mathf.Atan2(difference.y, difference.x);
            return angle;
        }

        /// <summary>
        /// Gets the section from the angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The target score section.</returns>
        private TargetScoreSection? GetSectionFromAngle(float angle)
        {
            foreach (TargetScoreSection section in this.values.sections)
            {
                if (section.IsWithinAngle(angle))
                {
                    return section;
                }
            }
            return null;
        }

        /// <summary>
        /// Recursively calculates the score.
        /// </summary>
        /// <param name="percentageFromCenter">The percentage from center.</param>
        /// <param name="currentScore">The current score.</param>
        /// <param name="currentIndex">The current index.</param>
        /// <returns>The score.</returns>
        private TargetScoreMultiplier CalculateScoreRecursion(float percentageFromCenter, TargetScoreMultiplier currentMultiplier, int currentIndex)
        {
            if(currentIndex >= this._scores.Count)
            {
                return currentMultiplier;
            }

            TargetScoreMultiplier score = this._scores[currentIndex];
            if(percentageFromCenter <= score.MaxDistancePercentage)
            {
                return this.CalculateScoreRecursion(
                    percentageFromCenter, score, ++currentIndex);
            }
            return currentMultiplier;
        }
    }
}