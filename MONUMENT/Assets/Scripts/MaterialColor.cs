using UnityEngine;

namespace MONUMENT
{
    public class MaterialColor : MonoBehaviour
    {
        [SerializeField] private Material concrete = default;

        [SerializeField] private Color sadColor = default;
        [SerializeField] private Color happyColor = default;
        [Range(0f, 1f)] public float value = default;

        public float R;

        private void FixedUpdate()
        {
            Color _color = Color.Lerp(sadColor, happyColor, value);

            _color.r += R;

            concrete.color = _color;
        }
    }
}