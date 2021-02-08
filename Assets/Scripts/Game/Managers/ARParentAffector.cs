using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ModifiedObject.Scripts.Game
{
    /// <summary>
    /// Affects each of these objects based on whether or not the target exists.
    /// </summary>
    public class ARParentAffector : Utils.EventContainerComponent
    {
        [SerializeField]
        private Utils.References.BooleanReference foundTarget;
        [SerializeField]
        private List<GameObject> objectsAffected;

        private Dictionary<GameObject, bool> _prevObjectActiveState
            = new Dictionary<GameObject, bool>();

        protected override void OnStart()
        {
            this.OnFoundTargetChanged(this.foundTarget.Value);
        }

        protected override void HookEvents()
        {
            this.foundTarget.ChangedValueEvent += this.OnFoundTargetChanged;
        }

        protected override void UnHookEvents()
        {
            this.foundTarget.ChangedValueEvent -= this.OnFoundTargetChanged;
        }

        private void OnFoundTargetChanged(bool changed)
        {
            int i = 0;
            foreach(GameObject _object in this.objectsAffected)
            {
                if(_object == null)
                {
                    continue;
                }

                if(!changed)
                {
                    this._prevObjectActiveState.Add(_object, _object.activeSelf);
                    _object?.SetActive(changed);
                }
                else
                {
                    bool result = !this._prevObjectActiveState.ContainsKey(_object) ?
                        true : this._prevObjectActiveState[_object];
                    _object?.SetActive(result);
                }
                i++;
            }
            if(changed)
            {
                this._prevObjectActiveState.Clear();
            }
        }
    }
}

