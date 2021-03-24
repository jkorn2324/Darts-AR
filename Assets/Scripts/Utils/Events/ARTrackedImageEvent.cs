using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedObject.Scripts.Utils.Events
{
    [CreateAssetMenu(fileName = "AR Tracked Image Event", menuName = "Events/Game Event (AR Tracked Image)")]
    public class ARTrackedImageEvent : GenericEvent<ARTrackedImage> { }
}