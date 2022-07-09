using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    
    private NavMeshAgent _navMeshAgent;
    //private Animator _animator;
    private bool _canMove;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        EnableMovement();
    }


    private void OnLose()
    {
        _canMove = false;
        _navMeshAgent.SetDestination(transform.position);
        //_animator.SetTrigger(Consts.Defeat);
        DisableMovement();
    }

    private void Update()
    {
        if(_canMove)
            Move();
    }

    private void Move()
    {
        float angle_rad = -45 * Mathf.Deg2Rad;
        Vector2 newJoystick = new Vector2(_joystick.Horizontal , _joystick.Vertical);
        newJoystick.x = _joystick.Horizontal * Mathf.Cos(angle_rad) - _joystick.Vertical * Mathf.Sin(angle_rad);
        newJoystick.y = _joystick.Horizontal * Mathf.Sin(angle_rad) + _joystick.Vertical * Mathf.Cos(angle_rad);
        Vector3 targetPos = transform.position + new Vector3(newJoystick.x, 0, newJoystick.y);
        
        _navMeshAgent.SetDestination(targetPos);
        
        /*if (targetPos == Vector3.zero)
            _animator.SetBool("IsRun", false);
        else
        {
            _animator.SetBool("IsRun", true);
            _animator.SetFloat("Speed", (targetPos - transform.position).magnitude);
        }*/
    }

    public void EnableMovement()
    {
        _canMove = true;
    }

    public void DisableMovement()
    {
        _canMove = false;
        //_animator.SetBool("IsRun", false);
    }

    public void Finish()
    {
        _canMove = false;
        _navMeshAgent.SetDestination(transform.position);
        //_animator.SetTrigger(Consts.Victory);
        DisableMovement();
    }
}
