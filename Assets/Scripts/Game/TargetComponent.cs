using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{

    [System.Serializable]
    public struct TargetScore
    {
        [SerializeField]
        private float maxDistancePercentage;
        [SerializeField]
        private float score;

        public float Score => this.score;

        public float MaxDistancePercentage => this.maxDistancePercentage;
    }

    /// <summary>
    /// The Target Component.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TargetComponent : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.BooleanReference foundTarget;

        [SerializeField, Range(0.01f, 100f)]
        private float targetRadius;

        [SerializeField]
        private float baseScore;
        [SerializeField]
        private List<TargetScore> scores;

        /// <summary>
        /// Called when the target is enabled.
        /// </summary>
        private void OnEnable()
        {
            this.foundTarget.Value = true;
        }

        /// <summary>
        /// Called when the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            this.foundTarget.Value = false;
        }


        /// <summary>
        /// Called when the Dart hit the target.
        /// </summary>
        /// <param name="component">The dart component.</param>
        public void OnDartCollide(DartComponent component)
        {
            if(component == null)
            {
                return;
            }

            Vector3 center = this.transform.position;
            Vector3 dartPosition = component.transform.position;
            float distanceFromCenter = Mathf.Abs(Vector3.Distance(center, dartPosition));
            float percentageTargetRadius = distanceFromCenter / targetRadius;
            float newScore = this.CalculateScore(percentageTargetRadius);

            Debug.Log("Percentage: "+ percentageTargetRadius);
            Debug.Log("New Score: " + newScore);
        }

        /// <summary>
        /// Calculates the score of the target.
        /// </summary>
        /// <param name="percentageFromCenter">The percentage from the center.</param>
        /// <returns>A float with the new score.</returns>
        private float CalculateScore(float percentageFromCenter)
        {
            if(this.scores.Count <= 0)
            {
                return this.baseScore;
            }

            TargetScore firstScore = this.scores[0];
            if(this.scores.Count == 1)
            {
                return percentageFromCenter <= firstScore.MaxDistancePercentage ?
                    firstScore.Score : this.baseScore;
            }

            // TODO: Implementation
            return firstScore.Score;
        }
    }
}