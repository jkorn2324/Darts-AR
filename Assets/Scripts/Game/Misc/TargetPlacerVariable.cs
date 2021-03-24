using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Game.Misc
{

    /// <summary>
    /// The target placer types.
    /// </summary>
    [System.Serializable]
    public enum TargetPlacerType
    {
        TYPE_IMAGE_TRACKING,
        TYPE_PLANE_TRACKING
    }


    [CreateAssetMenu(fileName = "Target Placer Variable", menuName = "Variables/Target Placer Variable")]
    public class TargetPlacerVariable : Utils.Variables.GenericVariable<TargetPlacerType> { }

    /// <summary>
    /// The Target placer reference variable.
    /// </summary>
    [System.Serializable]
    public class TargetPlacerReference : Utils.References.GenericReference<TargetPlacerType>
    {

        #region fields

        [SerializeField]
        private TargetPlacerVariable variable;  

        #endregion

        #region properties

        public override bool HasVariable 
            => this.variable != null;

        public event System.Action<TargetPlacerType> TargetPlacerChanged
        {
            add
            {
                if(this.variable == null) return;
                this.variable.AddChangedValueEventCallback(value);
            }
            remove
            {
                if(this.variable == null) return;
                this.variable.RemoveChangedValueEventCallback(value);
            }
        }

        protected override TargetPlacerType ReferenceValue 
        { 
            get => this.variable.Value; 
            set => this.variable.Value = value; 
        }

        #endregion

        #region methods

        public override void Reset()
        {
            this.variable?.Reset();
        }

        #endregion
    }
}