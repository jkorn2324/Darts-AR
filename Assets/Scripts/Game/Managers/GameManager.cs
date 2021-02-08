using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// The Game Manager.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.IntegerReference score;

        private void Start()
        {
            this.score.Reset();
        }
    }
}
