using UnityEngine;

namespace MONUMENT
{
    public class MaterialColor : MonoBehaviour
    {
        [SerializeField] private Material concrete = default;

        [SerializeField] private Color sadColor = default;
        [SerializeField] private Color happyColor = default;
        [Range(0f, 1f)] public float value = default; 

        private void FixedUpdate()
        {
            concrete.color = Color.Lerp(sadColor, happyColor, value);
        }
    }
}