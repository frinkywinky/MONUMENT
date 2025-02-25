using System.Collections;
using UnityEngine;

namespace MONUMENT
{
    /// <summary>
    /// Project add pixelated look with rendertextur.
    /// 
    /// Gotta try unity unit tests !! poggg
    /// </summary>
    public class ForcesMovement1 : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody rb = null;
        [SerializeField] private Transform eyes = null;
        [SerializeField] private CameraHandler handler = null;

        [Header("Movement Settings")]
        [SerializeField] private float movementCutoffVelocityMagnitude = 0f;
        [SerializeField] private float maxGroundedVelocity = 0f;
        [SerializeField] private float maxUngroundedVelocity = 0f;
        [SerializeField] private float movementAcceleration = 0f;
        [SerializeField] private float ungroundedAccelerationMultiplier = 0f;

        [SerializeField] private float jumpSpeed = 0f;

        [Header("Grounded Settings")]
        [SerializeField] private LayerMask groundMask = 0;
        private bool grounded;

        [SerializeField] private float groundColliderRadius = 0f;
        [SerializeField] private float groundColliderDownward = 0f;
        [SerializeField] private float maxGroundedAngle = 0f;

        float testVert;
        float testHori;
        bool testing;

        private void Start()
        {
            Application.targetFrameRate = 300;
            rb.sleepThreshold = 0f;

            StartCoroutine(TestMovementCoroutine());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded) { Jump(); }
        }

        private void FixedUpdate()
        {
            if (!testing)
            {
                Move(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
            }
            else
            {
                Move(testVert, testHori);
            }
        }

        private IEnumerator TestMovementCoroutine() 
        {
            testHori = 1f;
            testVert = 1f;
            testing = true;

            yield return new WaitForSeconds(3f);

            testHori = 0f;
            testVert = 0f;

            yield return new WaitForSeconds(1f);

            testing = false;
        }

        private void Jump()
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }

        private void Move(float vert, float hori)
        {
            CheckGrounded();

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, grounded ? maxGroundedVelocity : maxUngroundedVelocity);
            
            handler.Refresh(eyes.position, rb.velocity, Time.time);

            float acceleration = grounded ? movementAcceleration : movementAcceleration * ungroundedAccelerationMultiplier;

            float accel = movementAcceleration;

            if (grounded)
                accel *= ungroundedAccelerationMultiplier;

            Vector3 movement = (transform.right * hori) + (transform.forward * vert);
            movement.Normalize();

            Vector3 velocity = rb.velocity;
            velocity.y = 0f;

            float mag = velocity.magnitude;

            if (mag < movementCutoffVelocityMagnitude)
            {
                rb.AddForce(Vector3.ClampMagnitude(acceleration * Time.fixedDeltaTime * movement, movementCutoffVelocityMagnitude - mag), ForceMode.VelocityChange);
            }
            else if (grounded)
            {
                rb.AddForce(Vector3.ClampMagnitude(5f * Time.fixedDeltaTime * -velocity.normalized, mag - movementCutoffVelocityMagnitude), ForceMode.VelocityChange);
            }

            Vector3 counterMovement = acceleration * Time.fixedDeltaTime * ungroundedAccelerationMultiplier * -(velocity.normalized - movement);

            rb.AddForce(counterMovement, ForceMode.VelocityChange);
        }

        private void CheckGrounded()
        {
            grounded = false;

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, groundColliderRadius, Vector3.down, groundColliderDownward, groundMask, QueryTriggerInteraction.Ignore);

            foreach (RaycastHit hit in hits)
            {
                if (hit.distance == 0f) { continue; }

                if (Vector3.Angle(Vector3.up, hit.normal) <= maxGroundedAngle)
                {
                    grounded = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = (Color.blue + Color.white) / 2f;
            Gizmos.DrawWireSphere(transform.position + (Vector3.down * groundColliderDownward), groundColliderRadius);
        }
    }
}