using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.Player
{

    /// <summary>
    /// The player color.
    /// </summary>
    public enum PlayerColor
    {
        COLOR_RED,
        COLOR_BLUE,
        COLOR_GREEN,
        COLOR_BLACK,
        COLOR_YELLOW,
        COLOR_WHITE
    }

    public class PlayerScore
    {
        #region fields

        private Utils.References.IntegerReference _currentPlayerScore;
        private int _score;
        private readonly GamePlayer _parent;

        #endregion

        #region properties

        public int Score
        {
            get => this._score;
            set => this._score = value;
        }

        #endregion

        #region constructor

        public PlayerScore
            (GamePlayer player, Utils.References.IntegerReference currentPlayerScore)
        {
            this._score = 0;
            this._currentPlayerScore = currentPlayerScore;
            this._parent = player;
        }

        #endregion

        #region methods

        /// <summary>
        /// Called when the current player has been switched as active.
        /// </summary>
        public void OnSetCurrentThrowingPlayer()
        {
            this._currentPlayerScore.Value = this._score;
        }

        #endregion
    }

    /// <summary>
    /// The Game player class.
    /// </summary>
    public class GamePlayer
    {

        #region fields

        private readonly string _name;
        private readonly PlayerColor _color;
        private PlayerScore _score;

        #endregion

        #region properties

        public string Name
            => this._name.ToLower();

        public PlayerColor PlayerColor
            => this._color;

        public PlayerScore Score
            => this._score;

        #endregion

        #region constructor

        public GamePlayer(string name, PlayerColor color,
            Utils.References.IntegerReference currentPlayerScore)
        {
            this._name = name;
            this._color = color;
            this._score = new PlayerScore(this, currentPlayerScore);
        }

        #endregion

        #region methods

        /// <summary>
        /// Called when this player has become the current player
        /// to throw.
        /// </summary>
        public void SetCurrentThrowingPlayer()
        {
            this._score.OnSetCurrentThrowingPlayer();
        }

        #endregion
    }
}

