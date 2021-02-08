using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// The AR Data manager.
    /// </summary>
    public class ARDataManager : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.BooleanReference foundTarget;

        private static bool _dataManagerExists = false;

        /// <summary>
        /// Called when the ar data manager starts.
        /// </summary>
        private void Start()
        {
            if(_dataManagerExists)
            {
                Destroy(this.gameObject);
                return;
            }
            _dataManagerExists = true;
            DontDestroyOnLoad(this.gameObject);
            this.foundTarget.Value = false;
        }
    }
}

