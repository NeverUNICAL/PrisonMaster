using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraCustom : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _duration;

    private float _currentSize;
    private Camera _camera;
    private CharacterController _characterController;

    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponent<Camera>();
        _currentSize = _camera.orthographicSize;
    }

    private void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementInput.Normalize();
        Move(MovementInput);

        if (Input.GetKeyDown(KeyCode.Q))
            Finished();

        
    }

    private void Move(Vector2 movementInput)
    {
        var direction = new Vector3(movementInput.x, 14f, movementInput.y);
        _characterController.SimpleMove(direction * _speed);
    }

    private void Finished()
    {
        _characterController.enabled = false;
        StartCoroutine(ChangeSize(_duration));
    }

    private IEnumerator ChangeSize(float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime / duration)
        {
            _currentSize += CalculateCurrentSize(duration);

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
