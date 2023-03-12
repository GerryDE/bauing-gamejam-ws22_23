using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class PostProcessAging : MonoBehaviour
{
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("Rendered");
        Debug.Log(material);
        if (material == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        Graphics.Blit(source, destination, material);
    }
}
