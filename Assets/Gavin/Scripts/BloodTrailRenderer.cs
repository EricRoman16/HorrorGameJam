using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTrailRenderer : MonoBehaviour
{
    public List<Transform> brushes;
    public float brushSize;
    public float brushStrength;
    Material mat;
    Texture2D bloodTrail;

    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        bloodTrail = new Texture2D(100, 50);
        bloodTrail.filterMode = FilterMode.Point;

        for (int x = 0; x < bloodTrail.width; x++)
        {
            for (int y = 0; y < bloodTrail.height; y++)
            {
                bloodTrail.SetPixel(x, y, new Color(0, 0, 0, 0));
            }
        }

        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform brush in brushes)
        {
            Vector2 pixelPos = WorldToTexture(brush.position);
            for (int x = 0; x < brushSize; x++)
            {
                for (int y = 0; y < brushSize; y++)
                {
                    Color color = Color.red;
                    Vector2Int currentPixelPos = new Vector2Int(Mathf.RoundToInt(pixelPos.x) + x - (int)brushSize / 2, Mathf.RoundToInt(pixelPos.y) + y - (int)brushSize / 2);
                    if (currentPixelPos.x < 0 || currentPixelPos.y < 0 || currentPixelPos.x >= bloodTrail.width || currentPixelPos.y >= bloodTrail.height)
                    {
                        continue;
                    }

                    color.a = (brushSize/2)-Vector2.Distance(pixelPos, new Vector2(pixelPos.x + x - (int)brushSize / 2, pixelPos.y + y - (int)brushSize / 2));
                    color.a = Mathf.Max(color.a, 0);
                    Color oldColor = bloodTrail.GetPixel(currentPixelPos.x, currentPixelPos.y);
                    color.a = Mathf.Pow(color.a, 0.1f);

                    bloodTrail.SetPixel(currentPixelPos.x, currentPixelPos.y, (color + oldColor));
                }
            }
        }
        bloodTrail.Apply();
        mat.SetTexture("_BloodTex", bloodTrail);
    }

    Vector2 WorldToTexture(Vector2 worldPos)
    {

        Vector2 objectPos = worldPos - (Vector2)transform.position;
        Vector2 spritePos = Vector2.zero;

        objectPos += (Vector2)transform.localScale/2;

        float pixelSize = transform.localScale.x / bloodTrail.width;

        Vector2 pixelPos = objectPos / pixelSize;


        return pixelPos;
    }
}
