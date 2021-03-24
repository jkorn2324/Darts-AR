using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedObject.Scripts.Game
{

    [RequireComponent(typeof(Canvas))]
    public class TargetPlacerUI : Utils.EventContainerComponent
    {

        private static List<Misc.TargetPlacerType> _targetUIsLoaded = 
            new List<Misc.TargetPlacerType>();

        [SerializeField]
        private Utils.References.BooleanReference foundTarget; 
        [SerializeField]
        private Misc.TargetPlacerType targetPlacerType;
        [SerializeField]
        private Misc.TargetPlacerReference targetPlacerReference;
        
        private Canvas _canvas;

        protected override void OnStart()
        {
            if (_targetUIsLoaded.Contains(this.targetPlacerType))
            {
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);
            _targetUIsLoaded.Add(this.targetPlacerType);
            this._canvas = this.GetComponent<Canvas>();
            this.OnFoundTargetChanged(foundTarget.Value);
        }

        protected override void HookEvents()
        {
            this.foundTarget.ChangedValueEvent += this.OnFoundTargetChanged;
            this.targetPlacerReference.TargetPlacerChanged += this.OnTargetPlacerChanged;
        }

        protected override void UnHookEvents()
        {
            this.foundTarget.ChangedValueEvent -= this.OnFoundTargetChanged;
            this.targetPlacerReference.TargetPlacerChanged -= this.OnTargetPlacerChanged;
        }

        private void OnFoundTargetChanged(bool target)
        {
            if(this.targetPlacerReference.Value != this.targetPlacerType)
            {
                this._canvas.enabled = false;
                return;
            }
            this._canvas.enabled = !target;
        }

        private void OnTargetPlacerChanged(Misc.TargetPlacerType targetPlacerType)
        {
            if(targetPlacerType == this.targetPlacerType)
            {
                this._canvas.enabled = !this.foundTarget.Value;
                return;
            }
            this._canvas.enabled = false;
        }
    }
}

