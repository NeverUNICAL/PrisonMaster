using ForCreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCustom : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _duration;
    [SerializeField] private DoorButton _doorButton;

    private bool _reached;
    private float _currentSize;
    private Camera _camera;

    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _currentSize = _camera.orthographicSize;
    }

    private void OnEnable()
    {
        _doorButton.Reached += OnReached;
        _doorButton.Exit += OnExit;
    }

    private void OnDisable()
    {
        _doorButton.Reached -= OnReached;
        _doorButton.Exit -= OnExit;
    }

    private void OnExit()
    {
        _reached = false;
        StartCoroutine(ChangeSize(_duration, _reached));
    }

    private void OnReached()
    {
        Debug.Log(true);
        _reached = true;
        StartCoroutine(ChangeSize(_duration, _reached));
    }

    private IEnumerator ChangeSize(float duration, bool reached)
    {
        for (float i = 0; i < duration; i += Time.deltaTime / duration)
        {
            if (reached)
                _currentSize += CalculateCurrentSize(duration);
            else
                _currentSize -= CalculateCurrentSize(duration);

            _camera.orthographicSize = _currentSize;

            yield return null;
        }
    }

    private float CalculateCurrentSize(float duration)
    {
        float currentVolume = Mathf.MoveTowards(0, 1, Time.deltaTime * 2);
        return currentVolume;
    }
}
