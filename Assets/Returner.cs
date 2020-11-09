using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Returner : MonoBehaviour
    {
        private Hand handToReturnTo;
        private Rigidbody rigidbody;
        private Coroutine returningRoutine;


        public float force = 10f;
        public float maxVelocity = 10f;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }

        protected void OnAttachedToHand(Hand hand) {
            handToReturnTo = hand;
        }

        private void Update() {
            if (handToReturnTo == null)
                return;

            GrabTypes startingGrabType = handToReturnTo.GetGrabStarting();
            GrabTypes endingGrabType = handToReturnTo.GetGrabEnding();

            if (endingGrabType == GrabTypes.Pinch && returningRoutine != null) {
                StopCoroutine(returningRoutine);
                returningRoutine = null;
                rigidbody.useGravity = true;
            }

            if (startingGrabType == GrabTypes.Pinch && returningRoutine == null) {
                rigidbody.useGravity = false;
                returningRoutine = StartCoroutine(Return());
            }
        }

        private IEnumerator Return() {
            while (true) {
                rigidbody.AddForce((handToReturnTo.transform.position - transform.position).normalized * force);
                if (rigidbody.velocity.magnitude > maxVelocity)
                    rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
                yield return null;
            }
        }
    }
}