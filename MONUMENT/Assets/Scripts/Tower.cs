using UnityEngine;

namespace MONUMENT
{
    public class Tower : MonoBehaviour
    {
        private const float SPEED = 12f;
        private Vector3 velocity;
        private bool switching = false;

        private void Start()
        {
            gameObject.isStatic = false;

            velocity = Vector3.down * SPEED;
        }

        private void FixedUpdate()
        {
            if (switching) { return; }
            
            transform.Translate(velocity * Time.fixedDeltaTime);

            if (transform.position.y < -transform.localScale.y * 0.5f) 
            {
                switching = true;
                
                Invoke(nameof(Switch), 1f);
            }
            else if (transform.position.y > 0f)
            {
                Destroy(this);
            }
        }

        private void Switch() 
        {
            velocity *= -0.5f;

            switching = false;
        }
    }
}