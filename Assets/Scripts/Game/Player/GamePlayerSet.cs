using UnityEngine;
using System.Collections.Generic;

namespace ModifiedObject.Scripts.Game.Player
{

    /// <summary>
    /// The result of when the player gets added.
    /// </summary>
    public enum AddPlayerResult
    {
        RESULT_FAILED_NAME,
        RESULT_FAILED_COLOR,
        RESULT_FAILED_MAXPLAYERS,
        RESULT_SUCCESS
    }

    /// <summary>
    /// The set of players.
    /// </summary>
    [CreateAssetMenu(fileName = "Game Player Set", menuName = "Game/Game Player Set")]
    public class GamePlayerSet : ScriptableObject
    {

        #region fields

        [SerializeField]
        private Utils.References.IntegerReference maxPlayers;
        [SerializeField]
        private Utils.Events.GameEvent activePlayerChanged;

        private int _minPlayers = 1;
        private List<GamePlayer> _players
            = new List<GamePlayer>();
        private int _currentActiveIndex = 0;

        #endregion

        #region properties

        public int MinPlayers
            => this._minPlayers;

        public int CurrentPlayers
            => this._players.Count;

        public int MaxPlayers
            => this.maxPlayers.Value;

        public GamePlayer ActivePlayer
            => this._players[this._currentActiveIndex];

        #endregion

        #region methods

        public AddPlayerResult AddGamePlayer(GamePlayer player)
        {
            if(this.CurrentPlayers >= this.MaxPlayers)
            {
                return AddPlayerResult.RESULT_FAILED_MAXPLAYERS;
            }

            GamePlayer byName = this.GetGamePlayer(player.Name);
            if(byName != null)
            {
                return AddPlayerResult.RESULT_FAILED_NAME;
            }

            GamePlayer byColor = this.GetGamePlayerFromColor(player.PlayerColor);
            if(byColor != null)
            {
                return AddPlayerResult.RESULT_FAILED_COLOR;
            }

            this._players.Add(player);
            return AddPlayerResult.RESULT_SUCCESS;
        }

        public GamePlayer GetGamePlayer(string name)
        {
            return this._players.Find((GamePlayer searched) =>
            {
                return searched.Name == name;
            });
        }

        public GamePlayer GetGamePlayerFromColor(PlayerColor color)
        {
            return this._players.Find((GamePlayer searched) =>
            {
                return searched.PlayerColor == color;
            });
        }

        public void SetNextPlayerAsActive()
        {
            if(++this._currentActiveIndex > this.CurrentPlayers)
            {
                this._currentActiveIndex = 0;
            }
            this.activePlayerChanged?.CallEvent();
        }

        #endregion
    }
}

