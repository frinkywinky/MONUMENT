using UnityEngine;

namespace MONUMENT
{
    public class GridMaker : MonoBehaviour
    {
        [SerializeField] private GameObject standardCube = default;
        [SerializeField] private float height = default;
        [SerializeField] private float width = default;
        [SerializeField] private float spacing = default;
        [SerializeField] private int cubeCountSides = default;

        private void Start()
        {
            float xPlace = cubeCountSides * spacing * -0.5f;

            transform.position = new Vector3(xPlace, transform.position.y, xPlace);
            
            GameObject cube;
            
            for (int x = 0; x < cubeCountSides; x++)
            {
                for (int z = 0; z < cubeCountSides; z++)
                {
                    cube = Instantiate(standardCube, new Vector3(x * spacing, 0f, z * spacing) + transform.position, Quaternion.identity);
                    cube.transform.localScale = new Vector3(width, height, width);
                }
            }
        }
    }
}