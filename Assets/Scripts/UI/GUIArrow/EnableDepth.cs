using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnableDepth : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        camera.depthTextureMode = DepthTextureMode.Depth;

#if UNITY_EDITOR
        // Force Game View to render depth in the editor.
        camera.forceIntoRenderTexture = true;
#endif
    }
}
