using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GripThrowable : Throwable
{
    protected override void HandHoverUpdate(Hand hand) {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType == GrabTypes.Grip) {
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            hand.HideGrabHint();
        }
    }
}
