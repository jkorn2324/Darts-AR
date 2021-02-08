using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedObject.Scripts.Game
{

    [RequireComponent(typeof(Canvas))]
    public class TargetUI : Utils.EventContainerComponent
    {
        private static bool _targetUILoaded = false;

        [SerializeField]
        private Utils.References.BooleanReference foundTarget; 
        private Canvas _canvas;

        protected override void OnStart()
        {
            if (_targetUILoaded)
            {
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);
            _targetUILoaded = true;
            this._canvas = this.GetComponent<Canvas>();
            this.OnFoundTargetChanged(foundTarget.Value);
        }

        protected override void HookEvents()
        {
            this.foundTarget.ChangedValueEvent += this.OnFoundTargetChanged;
        }

        protected override void UnHookEvents()
        {
            this.foundTarget.ChangedValueEvent -= this.OnFoundTargetChanged;
        }

        private void OnFoundTargetChanged(bool target)
        {
            this._canvas.enabled = !target;
        }
    }
}

