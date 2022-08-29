using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraCustom : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private CharacterController _characterController;

    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementInput.Normalize();
        Move(MovementInput);
    }

    private void Move(Vector2 movementInput)
    {
        var direction = new Vector3(movementInput.x, 14f, movementInput.y);
        _characterController.SimpleMove(direction * _speed);
    }
}
