using UnityEngine;

namespace MONUMENT
{
    public class TowerActivator : MonoBehaviour
    {
        public bool Activating => activating;
        
        [SerializeField] private float towerSpeed = default;
        [SerializeField] private float waitPeriod = default;

        private bool activating;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Cube")) 
            {
                if (collision.transform.GetComponent<Tower>() == null) 
                {
                    collision.gameObject.AddComponent<Tower>().Setup(towerSpeed, waitPeriod);
                }

                activating = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.transform.CompareTag("Cube"))
            {
                return;
            }

            activating = false;
        }
    }
}