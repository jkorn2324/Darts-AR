using UnityEngine.UI;
using UnityEngine;
using System.Threading.Tasks;

namespace ModifiedObject.Scripts.Game.UI
{

    [System.Serializable]
    public abstract class TextUpdater<T>
    {
        [SerializeField]
        protected Text text;
        [SerializeField]
        protected string textDisplay;

        abstract public void HookEvent();

        abstract public void UnHookEvent();

        protected virtual void OnChanged(T value)
        {
            if(this.text != null)
            {
                this.text.text = this.textDisplay.Replace("%v", value.ToString());
            }
        }
    }

    [System.Serializable]
    public class IntegerTextUpdater : TextUpdater<int>
    {
        [SerializeField]
        private Utils.References.IntegerReference variable;

        public override void HookEvent()
        {
            this.variable.ChangedValueEvent += this.OnChanged;
        }

        public override void UnHookEvent()
        {
            this.variable.ChangedValueEvent -= this.OnChanged;
        }
    }
}

