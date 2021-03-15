using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ModifiedObject.Scripts.Game.Misc
{

    /// <summary>
    /// The screen orientation tracker.
    /// </summary>
    public class ScreenOrientationTracker : MonoBehaviour
    {

        [SerializeField]    
        private Utils.References.ScreenOrientationReference currentOrientation;
        private static bool _initialized = false;

        private ScreenOrientation CurrentOrientation
        {
            get 
            {
                #if UNITY_EDITOR
                    if(Screen.width > Screen.height)
                    {
                        return ScreenOrientation.Landscape;
                    }
                    else
                    {
                        return ScreenOrientation.Portrait;
                    }
                #else
                return Screen.orientation;
                #endif
            }
        }

        private void Start()
        {
            if(_initialized)
            {
                Destroy(this.gameObject);
                return;
            }
            _initialized = true;
            this.currentOrientation.Value = this.CurrentOrientation;
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Updates the orientation value every frame.
        /// </summary>
        private void Update()
        {
            this.currentOrientation.Value = this.CurrentOrientation;
        }
    }
}
