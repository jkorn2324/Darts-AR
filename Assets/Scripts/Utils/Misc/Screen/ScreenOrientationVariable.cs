using UnityEngine;

namespace ModifiedObject.Scripts.Utils
{

    namespace Variables
    {

        [CreateAssetMenu(fileName = "Screen Orientation Variable", menuName = "Variables/Screen Orientation Variable")]
        public class ScreenOrientationVariable : GenericVariable<ScreenOrientation> { }
    }


    namespace References
    {
        /// <summary>
        /// The Vector3 Reference class definition.
        /// </summary>
        [System.Serializable]
        public class ScreenOrientationReference : GenericReference<ScreenOrientation>
        {
            [SerializeField]
            private Variables.ScreenOrientationVariable variable;

            public event System.Action<ScreenOrientation> ChangedValueEvent
            {
                add
                {
                    if (this.variable == null) return;
                    this.variable.AddChangedValueEventCallback(value);
                }
                remove
                {
                    if (this.variable == null) return;
                    this.variable.RemoveChangedValueEventCallback(value);
                }
            }

            protected override ScreenOrientation ReferenceValue
            {
                get => this.variable.Value;
                set => this.variable.Value = value;
            }

            public override bool HasVariable
                => this.variable != null;

            public override void Reset()
            {
                this.variable?.Reset();
            }
        }
    }
}