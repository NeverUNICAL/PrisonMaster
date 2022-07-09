using UnityEngine;

namespace Agava.IdleGame.Examples
{
    [RequireComponent(typeof(Rigidbody))]
    public class BodyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private PlayerInput _input;

        private Rigidbody _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var rawDirection = new Vector3(_input.Direction.x, 0, _input.Direction.y);
            _body.velocity = (rawDirection.magnitude*transform.forward * _speed + Vector3.up * _body.velocity.y)*Time.deltaTime;

            if( rawDirection.magnitude > 0)
            {
            transform.rotation = Quaternion.LookRotation(rawDirection, Vector3.up) * Quaternion.Euler(0, 45, 0);
            }
            

        }
    }
}