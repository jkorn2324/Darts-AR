using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{

    [System.Serializable]
    public struct DartShooterReferences
    {
        [SerializeField]
        public Utils.References.BooleanReference foundTarget;
        [SerializeField]
        public Utils.References.BooleanReference canShootTarget;
        [SerializeField]
        public Utils.References.BooleanReference isPlaying;
        [SerializeField]
        public Utils.Events.GameEvent shootEvent;
    }

    /// <summary>
    /// The dart shooter component.
    /// </summary>
    public class DartShooterComponent : Utils.EventContainerComponent
    {
        [SerializeField]
        private DartShooterReferences references;
        [SerializeField]
        private Player.GamePlayerSet gamePlayerSet;

        protected override void HookEvents()
        {
            this.references.shootEvent?.HookEvent(this.ShootDart);
        }

        protected override void UnHookEvents()
        {
            this.references.shootEvent?.UnHookEvent(this.ShootDart);
        }

        /// <summary>
        /// Shoots the dart.
        /// </summary>
        public void ShootDart()
        {
            if(!references.foundTarget.Value)
            {
                return;
            }

            if(!this.references.canShootTarget.Value)
            {
                return;
            }

            Player.GamePlayer activePlayer = this.gamePlayerSet.ActivePlayer;
            if(activePlayer != null)
            {
                this.references.canShootTarget.Value = !this.references.isPlaying.Value;
                Instantiate(activePlayer.PlayerColor.DartPrefab);
            }
        }
    }
}

