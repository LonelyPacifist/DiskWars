using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GripThrowable : Throwable
{
    public float waitBeforeRotation = 0.1f;

    private new Collider collider;

    private new void Awake() {
        base.Awake();

        collider = GetComponentInChildren<Collider>();
    }

    protected override void HandHoverUpdate(Hand hand) {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType == GrabTypes.Grip) {
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            hand.HideGrabHint();

            //collider.enabled = false;
        }
    }

    public void RotateDisk() {
        StartCoroutine(RotateWithDelay());
    }

    private IEnumerator RotateWithDelay() {
        yield return new WaitForSeconds(waitBeforeRotation);
        SetRotation();
    }

    private void SetRotation() {
        Vector3 planeNormal = Vector3.Cross(Vector3.up, rigidbody.velocity);
        Vector3 up = Quaternion.AngleAxis(90, planeNormal) * rigidbody.velocity;
        Quaternion newRotation = new Quaternion();
        newRotation.SetLookRotation(rigidbody.velocity, up);
        transform.rotation = newRotation;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionExit(Collision collision) {
        SetRotation();
    }
}
