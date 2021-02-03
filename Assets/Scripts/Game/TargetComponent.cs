using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// The Target Component.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TargetComponent : MonoBehaviour
    {

        public void OnDartCollide(DartComponent component)
        {
            Debug.Log("Dart Collision Event");
        }
    }
}