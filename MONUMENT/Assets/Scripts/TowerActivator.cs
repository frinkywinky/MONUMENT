using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MONUMENT
{   
    public class TowerActivator : MonoBehaviour
    {
        [SerializeField] private float towerSpeed = default;
        [SerializeField] private float waitPeriod = default;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Cube")) 
            {
                if (collision.transform.GetComponent<Tower>() == null) 
                {
                    collision.gameObject.AddComponent<Tower>().Setup(towerSpeed, waitPeriod);
                }
            }
        }
    }
}