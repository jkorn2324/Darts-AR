using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedObject.Scripts.Game
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ARTrackedImagesListener : Utils.EventContainerComponent
    {
        [SerializeField]
        private Utils.Events.ARTrackedImageEvent addedImageEvent;
        [SerializeField]
        private Utils.Events.ARTrackedImageEvent updatedImageEvent;
        [SerializeField]
        private Utils.Events.ARTrackedImageEvent removedImageEvent;

        private ARTrackedImageManager _imageManager;

        private void Awake()
        {
            this._imageManager = this.GetComponent<ARTrackedImageManager>();
        }

        protected override void HookEvents()
        {
            this._imageManager.trackedImagesChanged += this.OnTrackedImagesChanged;
        }

        protected override void UnHookEvents()
        {
            this._imageManager.trackedImagesChanged -= this.OnTrackedImagesChanged;
        }

        /// <summary>
        /// Called when the tracked images has changed.
        /// </summary>
        /// <param name="args">The args for the image changed.</param>
        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach(ARTrackedImage addedImage in args.added)
            {
                this.addedImageEvent?.CallEvent(addedImage);
            }

            foreach(ARTrackedImage updatedImage in args.updated)
            {
                if(updatedImage.trackingState == TrackingState.Tracking)
                {
                    this.updatedImageEvent?.CallEvent(updatedImage);
                    continue;
                }
                // Tracks whether the image is removed.
                this.removedImageEvent?.CallEvent(updatedImage);
            }

            foreach(ARTrackedImage removedImage in args.removed)
            {
                this.removedImageEvent?.CallEvent(removedImage);
            }
        }
    }
}
