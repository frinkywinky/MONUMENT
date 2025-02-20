using UnityEngine;

namespace MONUMENT
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private Transform eyes = null;
         
        private Vector3 pos;
        private Vector3 vel;
        private float time;

        private void Update()
        {
            transform.SetPositionAndRotation(pos + (vel * (Time.time - time)), eyes.rotation);
        }

        public void Refresh(Vector3 pos, Vector3 vel, float time)
        {
            this.pos = pos;
            this.vel = vel;
            this.time = time;
        }
    }
}