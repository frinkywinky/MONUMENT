using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MONUMENT
{
    public class TowerActivator : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Cube")) 
            {
                if (collision.transform.GetComponent<Tower>() == null) 
                {
                    collision.gameObject.AddComponent<Tower>();
                }
            }
        }
    }
}