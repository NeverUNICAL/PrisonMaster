using UnityEngine;

public class LayersSwitcher : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        _camera.cullingMask = (int)CameraLayers.ArrowDefault;
        _camera.cullingMask = (int)CameraLayers.Arrow;
    }
}

public enum CameraLayers
{
    Nothing,
    Default,
    Arrow = 128,
    ArrowDefault = 129
}
