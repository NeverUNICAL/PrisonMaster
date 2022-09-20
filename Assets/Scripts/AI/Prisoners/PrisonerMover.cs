using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PrisonerMover : Prisoner
{
    [SerializeField] private GameObject[] _suits;
    [SerializeField] private ParticleSystem _angryEffect;

    private GameObject _nextPoint;
    private Vector3 _targetPoint;
    private bool _pathEnded;
    private Transform _lookTarget;
    private bool _isSittingInCell = false;

    public bool IsSittingInCell => _isSittingInCell;
    public GameObject NextPoint => _nextPoint;

    private new void FixedUpdate()
    {
        if (AgentPath != NavMeshAgent.path)
        {
            NavMeshAgent.destination = _nextPoint.transform.position + _targetPoint;
            AgentPath = NavMeshAgent.path;
        }

        transform.LookAt(_lookTarget);

        base.FixedUpdate();
    }

    public bool PathEnded()
    {
        _pathEnded = false;

        if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
            _pathEnded = true;

        return _pathEnded;
    }

    public void EnableSuit()
    {
        _suits[0].SetActive(false);
        _suits[1].SetActive(true);
    }

    public void EnableWashedSuit()
    {
        _suits[2].SetActive(false);
        _suits[0].SetActive(true);
        _suits[1].SetActive(false);
    }

    public void SetTarget(GameObject target, Vector3 offset, Transform lookTarget = null)
    {
        _nextPoint = target;
        _targetPoint = offset;

        if (lookTarget == null)
            _lookTarget = _nextPoint.transform;
        else
            _lookTarget = lookTarget;
    }

    public void ChangePriority(int value)
    {
        NavMeshAgent.avoidancePriority = value;
    }

    public void ChangeAngryAnimation(bool value)
    {
        Animator.SetBool(IsAngryAnimation, value);
        _angryEffect.gameObject.SetActive(value);
    }

    public void ChangeStateSitting(bool value)
    {
        _isSittingInCell = value;
    }
}