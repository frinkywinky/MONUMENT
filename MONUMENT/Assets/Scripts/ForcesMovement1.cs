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
        [SerializeField] private TowerActivator towerActivator = null;
        [SerializeField] private MaterialColor materialColor = null;

        [Header("Movement Settings")]
        [SerializeField] private float movementCutoffVelocityMagnitude = 0f;
        [SerializeField] private float maxGroundedVelocity = 0f;
        [SerializeField] private float maxUngroundedVelocity = 0f;
        [SerializeField] private float movementAcceleration = 0f;
        [SerializeField] private float ungroundedAccelerationMultiplier = 0f;

        [SerializeField] private float jumpSpeed = 0f;

        [Header("Grounded Settings")]
        [SerializeField] private LayerMask groundMask = 0;
        [SerializeField] private float groundColliderRadius = 0f;
        [SerializeField] private float groundColliderDownward = 0f;
        [SerializeField] private float maxGroundedAngle = 0f;

        private bool grounded;

        private void Start()
        {
            //Time.fixedDeltaTime = 1f / 600f;
            Application.targetFrameRate = 300;
            rb.sleepThreshold = 0f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded) { Jump(); }
        }

        private void Jump()
        {
            Vector3 vec = (towerActivator.Activating ? 3f : 1f) * jumpSpeed * Vector3.up;

            if (rb.velocity.y < 0f) { vec.y -= rb.velocity.y; }

            rb.AddForce(vec, ForceMode.VelocityChange);

            if (towerActivator.Activating) 
            {
                materialColor.R += 0.1f;
            }
            //materialColor.value = 1f - materialColor.value;
        }

        private void FixedUpdate()
        {
            CheckGrounded();

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, grounded ? maxGroundedVelocity : maxUngroundedVelocity);

            handler.Refresh(eyes.position, rb.velocity, Time.time);

            float acceleration = grounded ? movementAcceleration : movementAcceleration * ungroundedAccelerationMultiplier;

            Vector3 movement = (transform.right * Input.GetAxisRaw("Horizontal")) + (transform.forward * Input.GetAxisRaw("Vertical"));
            movement.Normalize();

            Vector3 velocity = rb.velocity;

            materialColor.value = velocity.magnitude / maxGroundedVelocity;

            velocity.y = 0f;

            float mag = velocity.magnitude;

            if (mag < movementCutoffVelocityMagnitude)
            {
                rb.AddForce(Vector3.ClampMagnitude(acceleration * Time.fixedDeltaTime * movement, movementCutoffVelocityMagnitude - mag), ForceMode.VelocityChange);
            }
            else if (grounded)
            {
                rb.AddForce(Vector3.ClampMagnitude(acceleration * Time.fixedDeltaTime * -velocity.normalized, mag - movementCutoffVelocityMagnitude), ForceMode.VelocityChange);
            }

            Vector3 counterMovement = acceleration * Time.fixedDeltaTime * ungroundedAccelerationMultiplier * -(velocity.normalized - movement);

            if (mag != 0f && counterMovement.magnitude > mag) { counterMovement = -velocity;  }

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