using UnityEngine;

namespace MONUMENT
{
    public class Tower : MonoBehaviour
    {
        private float dir = -1f;
        
        private void Start()
        {
            gameObject.isStatic = false;
        }

        private void FixedUpdate()
        {
            transform.Translate(dir * 6f * Time.fixedDeltaTime * Vector3.up);

            if (transform.position.y <= -250f) 
            {
                dir = 1f;
                
                //Destroy(gameObject);
            }
            else if (transform.position.y > 10f) 
            {
                dir = -1f;
            }
        }
    }
}