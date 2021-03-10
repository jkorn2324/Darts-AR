using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedObject.Scripts.Utils.References
{

    /// <summary>
    /// The Generic reference abstact class.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public abstract class GenericReference<T>
    {
        [SerializeField]
        protected bool isConstant = true;
        [SerializeField]
        protected T constantValue;

        protected abstract T ReferenceValue
        {
            get;
            set;
        }

        public abstract bool HasVariable
        {
            get;
        }

        public T Value
        {
            set
            {
                if(this.isConstant)
                {
                    return;
                }
                this.ReferenceValue = value;
            }
            get => this.isConstant ? this.constantValue : this.ReferenceValue;
        }

        abstract public void Reset();
    }

    /// <summary>
    /// The Integer Reference class definition.
    /// </summary>
    [System.Serializable]
    public class IntegerReference : GenericReference<int>
    {
        [SerializeField]
        private Variables.IntegerVariable variable;

        public event System.Action<int> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override int ReferenceValue
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

    /// <summary>
    /// The String Reference class definition.
    /// </summary>
    [System.Serializable]
    public class StringReference : GenericReference<string>
    {
        [SerializeField]
        private Variables.StringVariable variable;

        public event System.Action<string> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;

                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override string ReferenceValue
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

    /// <summary>
    /// The Float Reference class definition.
    /// </summary>
    [System.Serializable]
    public class FloatReference : GenericReference<float>
    {
        [SerializeField]
        private Variables.FloatVariable variable;

        public event System.Action<float> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;

                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override float ReferenceValue
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

    /// <summary>
    /// The Boolean Reference class definition.
    /// </summary>
    [System.Serializable]
    public class BooleanReference : GenericReference<bool>
    {
        [SerializeField]
        private Variables.BooleanVariable variable;

        public event System.Action<bool> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;

                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override bool ReferenceValue
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

    /// <summary>
    /// The Vector3 Reference class definition.
    /// </summary>
    [System.Serializable]
    public class Vector3Reference : GenericReference<Vector3>
    {
        [SerializeField]
        private Variables.Vector3Variable variable;

        public event System.Action<Vector3> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;

                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override Vector3 ReferenceValue
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

    /// <summary>
    /// The Vector2 Reference class definition.
    /// </summary>
    [System.Serializable]
    public class Vector2Reference : GenericReference<Vector2>
    {
        [SerializeField]
        private Variables.Vector2Variable variable;

        public event System.Action<Vector2> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;

                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override Vector2 ReferenceValue
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

    /// <summary>
    /// The Vector3 Reference class definition.
    /// </summary>
    [System.Serializable]
    public class QuaternionReference : GenericReference<Quaternion>
    {
        [SerializeField]
        private Variables.QuaternionVariable variable;

        public event System.Action<Quaternion> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;

                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override Quaternion ReferenceValue
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