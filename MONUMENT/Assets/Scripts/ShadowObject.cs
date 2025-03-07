using System.Collections.Generic;
using UnityEngine;

namespace MONUMENT
{
    /// <summary>
    /// This now shows the past, how the heck am i gonna make the future? maybe ask lucas or bram ???
    /// </summary>
    public class ShadowObject : MonoBehaviour
    {
        public Queue<Frame> frames = new Queue<Frame>();

        [SerializeField] private Transform target = default;
        [SerializeField] private int frameTimeCount = default;
        [SerializeField] private Vector3 offset = default;

        private Frame frame;

        private void FixedUpdate()
        {
            frames.Enqueue(new Frame(target.position, target.rotation));
            
            if (frames.Count > frameTimeCount)
            {
                frame = frames.Dequeue();

                transform.SetPositionAndRotation(frame.pos + offset, frame.rot);
            }
        }

        public struct Frame
        {
            public Vector3 pos;
            public Quaternion rot;

            public Frame(Vector3 pos, Quaternion rot)
            {
                this.pos = pos;
                this.rot = rot;
            }
        }
    }
}