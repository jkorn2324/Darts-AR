using UnityEngine;
using System.Collections;

namespace ModifiedObject.Scripts.Game
{

    public class DartMovement
    {

        private DartComponent _parent;


        public DartMovement(DartComponent parent)
        {
            this._parent = parent;
        }

        /// <summary>
        /// Updates the movement of the dart.
        /// </summary>
        /// <param name="deltaTime">The deltaTime.</param>
        public void UpdateMovement(float deltaTime)
        {
            // todo: implementation
        }
    }
}