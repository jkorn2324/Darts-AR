using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.Player
{
    /// <summary>
    /// Handles the players in the game.
    /// </summary>
    public class GamePlayerManager : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.IntegerReference activePlayerScore;
        [SerializeField]
        private GamePlayerSet playerSet;
        [SerializeField]
        private GamePlayerColorSet colorSet;

        private static bool _playerManagerExists = false;

        /// <summary>
        /// The Start Method.
        /// </summary>
        private void Start()
        {
            if(_playerManagerExists)
            {
                Destroy(this.gameObject);
                return;
            }

            _playerManagerExists = true;
            this.GenerateDefaultPlayers();
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Generates the default players.
        /// </summary>
        private void GenerateDefaultPlayers()
        {
            this.playerSet?.GenerateDefaultPlayers(this.colorSet, activePlayerScore);
        }
    }
}
