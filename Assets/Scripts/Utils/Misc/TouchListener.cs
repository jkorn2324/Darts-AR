using UnityEngine;

namespace ModifiedObject.Scripts.Utils.Misc
{

    /// <summary>
    /// The Touch listener...
    /// </summary>
    public class TouchListener : MonoBehaviour
    {

        [SerializeField]
        private Utils.References.Vector2Reference touchTravelled;
        [SerializeField]
        private Utils.Events.FloatEvent touchFinishedEvent;
        
        private float _touchTime = 0.0f;
        private bool _touching = false;

        private static bool _initialized = false;

        private void Start()
        {
            if(_initialized)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            _initialized = true;
        }
        
        private void Update()
        {
            this.UpdateTouch(Time.deltaTime);
            this.UpdateTouchTime(Time.deltaTime);
        }

        private void UpdateTouch(float deltaTime)
        {
            if(Input.touchCount <= 0)
            {
                this._touching = false;
                return;
            }

            Touch firstTouch = Input.GetTouch(0);
            if(firstTouch.phase == TouchPhase.Began)
            {
                this.OnTouchBegin(firstTouch);
            }
            else if (firstTouch.phase == TouchPhase.Ended)
            {
                this.OnTouchEnd();
            }
            else if (firstTouch.phase == TouchPhase.Moved)
            {
                this.OnTouchMoved(firstTouch);
            }
        }

        private void OnTouchBegin(Touch beginTouch)
        {
            this._touchTime = 0.0f;
            this._touching = true;
            this.touchTravelled.Value = Vector3.zero;
        }

        private void OnTouchEnd()
        {
            this.touchFinishedEvent?.CallEvent(this._touchTime);
            this._touching = false;
        }

        private void OnTouchMoved(Touch endTouch)
        {
            this.touchTravelled.Value += (Vector2)endTouch.deltaPosition;
        }

        private void UpdateTouchTime(float deltaTime)
        {
            if(!this._touching)
            {
                if(this._touchTime > 0.0f)
                {
                    this._touchTime = 0.0f;
                }
                return;
            }
            this._touchTime += deltaTime;
        }
    }
}