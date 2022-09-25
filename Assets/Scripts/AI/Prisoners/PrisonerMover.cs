using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PrisonerMover : Prisoner
{
    [SerializeField] private GameObject[] _suits;
    [SerializeField] private ParticleSystem _angryEffect;
    [SerializeField] private ParticleSystem _poofEffect;

    private GameObject _nextPoint;
    private Vector3 _targetPoint;
    private bool _pathEnded;
    private Transform _lookTarget;
    private bool _isSittingInCell = false;

    private int _dirtySuit = 0;
    private int _cleanSuit = 1;
    private int _prisonUniform = 2;

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
        _suits[_cleanSuit].SetActive(false);
        _suits[_prisonUniform].SetActive(true);
        _poofEffect.Play();
    }

    public void EnableWashedSuit()
    {
        _suits[_prisonUniform].SetActive(false);
        _suits[_dirtySuit].SetActive(false);
        _suits[_cleanSuit].SetActive(true);
        _poofEffect.Play();
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