using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public Shader shader;
    Material mat;
    Texture oldView;

    private Texture2D destinationTexture;
    private bool isPerformingScreenGrab;

    float transitionProgress = 1; //0-1
    float transitionSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);
        Camera.onPostRender += OnPostRenderCallback;
        isPerformingScreenGrab = true;

        destinationTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

    }

    public void StartTransition()
    {

        isPerformingScreenGrab = true;
        transitionProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transitionProgress < 1)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;
        }

    }

    void OnPostRenderCallback(Camera cam)
    {
        if (isPerformingScreenGrab)
        {
            // Check whether the Camera that has just finished rendering is the one you want to take a screen grab from
            if (cam == Camera.main)
            {
                // Define the parameters for the ReadPixels operation
                Rect regionToReadFrom = new Rect(0, 0, Screen.width, Screen.height);
                int xPosToWriteTo = 0;
                int yPosToWriteTo = 0;
                bool updateMipMapsAutomatically = false;

                // Copy the pixels from the Camera's render target to the texture
                destinationTexture.ReadPixels(regionToReadFrom, xPosToWriteTo, yPosToWriteTo, updateMipMapsAutomatically);

                // Upload texture data to the GPU, so the GPU renders the updated texture
                // Note: This method is costly, and you should call it only when you need to
                // If you do not intend to render the updated texture, there is no need to call this method at this point
                destinationTexture.Apply();

                // Reset the isPerformingScreenGrab state
                isPerformingScreenGrab = false;
            }
        }
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
        if (destinationTexture != null)
        {
            mat.SetTexture("_OldView", destinationTexture);
            mat.SetFloat("_Progress", transitionProgress);
        }
    }
    void OnDestroy()
    {
        Camera.onPostRender -= OnPostRenderCallback;
    }
}
