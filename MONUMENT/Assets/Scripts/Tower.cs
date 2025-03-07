using UnityEngine;

namespace MONUMENT
{
    public class Tower : MonoBehaviour
    {
        //private const float SPEED = 12f;

        //private float speed;
        private Vector3 velocity;
        private bool switching;
        private Vector3 originalPos;
        private float waitPeriod;

        private void FixedUpdate()
        {
            if (switching) { return; }
            
            transform.Translate(velocity * Time.fixedDeltaTime);

            if (transform.position.y < -transform.localScale.y * 0.5f) 
            {
                switching = true;
                
                Invoke(nameof(Switch), waitPeriod);
            }
            else if (transform.position.y > 0f)
            {
                Die();
            }
        }

        public void Setup(float speed, float waitPeriod) 
        {
            this.waitPeriod = waitPeriod;
            
            originalPos = transform.position;

            gameObject.isStatic = false;

            velocity = Vector3.down * speed;
        }

        private void Die()
        {
            transform.position = originalPos;
            gameObject.isStatic = true;
            Destroy(this);
        }

        private void Switch() 
        {
            velocity *= -0.5f;

            switching = false;
        }
    }
}