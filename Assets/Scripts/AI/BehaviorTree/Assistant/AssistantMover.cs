using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AssistantMover : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;
    [SerializeField] private StackView _stackView;

    private bool _canMove = true;
    private float _time = 0;
    private ICharacterInputSource _inputSource;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = false;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        var movement = new Vector3(_inputSource.MovementInput.x, 0, _inputSource.MovementInput.y);

        if (_time >= 3 && _canMove)
        {
            Debug.Log(_navMeshAgent.isStopped);
        _navMeshAgent.SetDestination(movement);
            _canMove = false;
        }
    }

    private void OnValidate()
    {
        if (_inputSourceBehaviour && !(_inputSourceBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }
}
