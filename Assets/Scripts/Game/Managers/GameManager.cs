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
        [SerializeField]
        private Utils.References.BooleanReference canShootDart;
        [SerializeField]
        private Utils.References.BooleanReference isPlaying;

        private void Start()
        {
            this.isPlaying.Reset();
            this.canShootDart.Reset();
            this.score.Reset();
        }
    }
}
