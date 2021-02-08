using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.Player
{

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
        public void SetActiveScore()
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
        private readonly GamePlayerColor _color;
        private PlayerScore _score;
        private bool _isActive = false;

        #endregion

        #region properties

        public string Name
            => this._name.ToLower();

        public GamePlayerColor PlayerColor
            => this._color;

        public PlayerScore Score
            => this._score;

        #endregion

        #region constructor

        public GamePlayer(string name, GamePlayerColor color,
            Utils.References.IntegerReference currentPlayerScore)
        {
            this._name = name;
            this._color = color;
            this._score = new PlayerScore(this, currentPlayerScore);
        }

        #endregion

        #region methods

        /// <summary>
        /// Called to set the player as active.
        /// </summary>
        /// <param name="active">Active value.</param>
        public void SetActive(bool active)
        {
            if(active)
            {
                this._score.SetActiveScore();
            }
            this._isActive = active;
        }

        #endregion
    }
}

