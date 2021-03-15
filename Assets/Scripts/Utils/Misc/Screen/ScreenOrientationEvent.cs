using UnityEngine;

namespace ModifiedObject.Scripts.Utils.Events
{

    /// <summary>
    /// A screen orientation event.
    /// </summary>
    [CreateAssetMenu(fileName = "Screen Orientation Event", menuName = "Events/Screen Orientation Event")]
    public class ScreenOrientationEvent : GenericEvent<ScreenOrientation> { }
}