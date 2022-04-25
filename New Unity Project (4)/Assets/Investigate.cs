//======= Copyright (c) Vincent Lam, All rights reserved. ===============
//
// Investigate object by Vincent Lam
//
//=============================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class Investigate : MonoBehaviour
	{

		public UnityEvent attachEvent;
		public UnityEvent detachEvent;
		public UnityEvent hoverStartEvent;
		public UnityEvent hoverEndEvent;

		//private TextMesh textMesh;
		private Vector3 oldPosition;
		private Quaternion oldRotation;

		//private float attachTime;

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & ( ~Hand.AttachmentFlags.DetachOthers );

        private Interactable interactable;

        void Awake()
        {
            interactable = this.GetComponent<Interactable>();
        }

        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin( Hand hand )
		{
			//textMesh.text = "Hovering hand: " + hand.name;
			hoverStartEvent.Invoke();
		}


		//-------------------------------------------------
		// Called when a Hand stops hovering over this object
		//-------------------------------------------------
		private void OnHandHoverEnd( Hand hand )
		{
			hoverEndEvent.Invoke ();
			//textMesh.text = "No Hand Hovering";
		}


        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

            if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
            {
                // Save our position/rotation so that we can restore it when we detach
                oldPosition = transform.position;
                oldRotation = transform.rotation;

                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                hand.HoverLock(interactable);

                // Attach this object to the hand
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            }
            else if (isGrabEnding)
            {
                // Detach this object from the hand
                hand.DetachObject(gameObject);

                // Call this to undo HoverLock
                hand.HoverUnlock(interactable);

                // Restore position/rotation
                transform.position = oldPosition;
                transform.rotation = oldRotation;
            }
        }


        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        private void OnAttachedToHand( Hand hand )
		{
			//textMesh.text = "Attached to hand: " + hand.name;
			//attachTime = Time.time;
			attachEvent.Invoke();
		}


		//-------------------------------------------------
		// Called when this GameObject is detached from the hand
		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			//textMesh.text = "Detached from hand: " + hand.name;
			detachEvent.Invoke();
		}


		//-------------------------------------------------
		// Called every Update() while this GameObject is attached to the hand
		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
			//textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + ( Time.time - attachTime ).ToString( "F2" );
		}


		//-------------------------------------------------
		// Called when this attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
		}


		//-------------------------------------------------
		// Called when another attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
		}
	}
}
