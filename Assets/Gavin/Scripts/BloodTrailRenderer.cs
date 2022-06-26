using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTrailRenderer : MonoBehaviour
{
    public List<Transform> brushes;
    Material mat;
    Texture2D bloodTrail;

    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        bloodTrail = new Texture2D(100, 50);
        bloodTrail.filterMode = FilterMode.Point;

        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform brush in brushes)
        {
            Vector2Int pixelPos = WorldToTexture(brush.position);
            print(pixelPos);
            if(pixelPos.x < 0 || pixelPos.y < 0 || pixelPos.x >= bloodTrail.width || pixelPos.y >= bloodTrail.height)
            {
                continue;
            }
            bloodTrail.SetPixel(pixelPos.x, pixelPos.y, Color.red);
        }
        bloodTrail.Apply();
        mat.SetTexture("_BloodTex", bloodTrail);
    }

    Vector2Int WorldToTexture(Vector2 worldPos)
    {

        Vector2 objectPos = worldPos - (Vector2)transform.position;
        Vector2 spritePos = Vector2.zero;

        objectPos += (Vector2)transform.localScale/2;

        float pixelSize = transform.localScale.x / bloodTrail.width;

        Vector2 pixelPos = objectPos / pixelSize;


        return new Vector2Int(Mathf.RoundToInt(pixelPos.x), Mathf.RoundToInt(pixelPos.y));
    }
}
