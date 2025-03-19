using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MONUMENT
{
    public class ColorTest : MonoBehaviour
    {
        [SerializeField] private RenderTexture rend = default;
        [SerializeField] private Texture2D outTexture = default;
        [SerializeField] private Image image = default;
        [SerializeField] private int width, height;
        

        private void Start()
        {
            
        }

        private void FixedUpdate()
        {
            /*RenderTexture.active = rend;
            outTexture = new Texture2D(400, 270, TextureFormat.RGBA32, false, true);
            outTexture.ReadPixels(new Rect(0, 0, rend.width, rend.height), 0, 0, false);
            outTexture.SetPixel(10, 10, Color.black);
            outTexture.Apply();

            Rect rect = new Rect(0f, 0f, outTexture.width, outTexture.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            image.sprite = Sprite.Create(outTexture, rect, pivot, 100f);*/
        }

        private void Update()
        {
            Rect rect = new Rect(0f, 0f, width, height);

            RenderTexture.active = rend;
            outTexture = new Texture2D(width, height, TextureFormat.R8, false, true);
            outTexture.ReadPixels(rect, 0, 0, false);
            //outTexture.SetPixel(10, 10, Color.black);
            outTexture.filterMode = FilterMode.Point;
            outTexture.wrapMode = TextureWrapMode.Clamp;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    outTexture.SetPixel(x, y, outTexture.GetPixel(x, y).r > 0.5f ? Color.white : Color.black);
                }
            }

            //outTexture.com
            outTexture.Apply();
            //RenderTexture.active = null;
            
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            image.sprite = Sprite.Create(outTexture, rect, pivot, 100f);

            /*//Assume your RenderTexture is ARGB32 format, then
            RenderTexture.active = rend;
            //Texture2D tempTexture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
            Texture2D tempTexture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
            //tempTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tempTexture.SetPixels(rend.co);
            tempTexture.Apply();
            RenderTexture.active = null;

            //Then use another texture to save it:
            Color[] colorSrc = tempTexture.GetPixels(0, 0, width, height);
            outTexture = new Texture2D(width, height, TextureFormat.Alpha8, false, true);
            outTexture.SetPixels(colorSrc);
            outTexture.Apply(true, false);*/
        }
    }
}