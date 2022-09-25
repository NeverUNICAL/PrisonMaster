using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _cinemachines;
    [SerializeField] private GlobalTutorial _globalTutorial;
    [SerializeField] private float _delayChangeCamera;
    [SerializeField] private float _delayAssistantMovingFollow = 6f;
    [SerializeField] private float _delayPrisonerMovingFollow = 14f;
    [SerializeField] private float _delayAllViewMovingFollow = 4f;
    [SerializeField] private float _delayBusMovingFollow = 5f;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Transform _assistantsShopPanel;
    [SerializeField] private AssistantsShop _assistantsShop;
    [SerializeField] private CellQueueContainer _queueContainer;
    [SerializeField] private Cell _cell;

    private int _playerCamNumber = 0;
    private int _targetCamNumber = 1;
    private int _assistantCamNumber = 2;
    private int _prisonerCamNumber = 3;
    private int _allViewCamFollow = 4;
    private int _busCamFollow = 5;
    
    private int _counter = 0;

    private bool _isFirst = true;

    private const int PriorityValue = 1;
    private const int NotPriorityValue = 0;

    private void OnEnable()
    {
        _globalTutorial.PointerShown += OnPointerShown;
        _assistantsShop.CountUpgraded += OnUpgraded;
        _queueContainer.PrisonerEmptyed += OnPrisonerEmptyed;
        _cell.PrisonerSendToPool += OnPoolPrisonerAdded;
    }

    private void OnDisable()
    {
        _globalTutorial.PointerShown -= OnPointerShown;
    }

    private void Start()
    {
        ChangeCamera(_playerCamNumber);
    }

    private void OnPointerShown(Transform transform)
    {
        if (_isFirst)
        {
            _isFirst = false;
            StartCoroutine(FollowBus(_busCamFollow, _delayBusMovingFollow));
        }
        else if (_counter > 5 && _counter < 8)
        {
            _cinemachines[_targetCamNumber].m_Follow = transform;
        }
        else
        {
            _cinemachines[_targetCamNumber].m_Follow = transform;
            ChangeStateJoystick(false);
            ChangeCamera(_targetCamNumber);
            StartCoroutine(DelayChangeCamera(_playerCamNumber, _delayChangeCamera));
        }

        _counter++;
    }

    private void OnUpgraded(int value1, int value2)
    {
        ChangeStateJoystick(false);
        StartCoroutine(FollowAssistant(_assistantCamNumber, _delayAssistantMovingFollow));
        _assistantsShop.CountUpgraded -= OnUpgraded;
    }

    private void OnPrisonerEmptyed(PrisonerMover prisoner, CellQueueContainer cellQueueContainer)
    {
        _cinemachines[_prisonerCamNumber].m_Follow = prisoner.transform;
        _queueContainer.PrisonerEmptyed -= OnPrisonerEmptyed;
    }

    private void OnPoolPrisonerAdded()
    {
        ChangeStateJoystick(false);
        StartCoroutine(FollowPrisoner(_prisonerCamNumber, _delayPrisonerMovingFollow));
        _cell.PrisonerSendToPool -= OnPoolPrisonerAdded;
    }

    private void ChangeCamera(int targetCamera)
    {
        for (int i = 0; i < _cinemachines.Length; i++)
        {
            if (i == targetCamera)
                _cinemachines[i].Priority = PriorityValue;
            else
                _cinemachines[i].Priority = NotPriorityValue;
        }
    }

    private IEnumerator DelayChangeCamera(int targetCamera, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_counter == 7)
            _assistantsShopPanel.gameObject.SetActive(true);

        ChangeStateJoystick(true);
        ChangeCamera(targetCamera);
    }

    private IEnumerator FollowAssistant(int targetCamera, float delay)
    {
        ChangeCamera(targetCamera);
        _assistantsShopPanel.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        ChangeCamera(_targetCamNumber);
        StartCoroutine(DelayChangeCamera(_playerCamNumber, _delayChangeCamera));
    }

    private IEnumerator FollowPrisoner(int targetCamera, float delay)
    {
        ChangeCamera(targetCamera);

        yield return new WaitForSeconds(delay);

        StartCoroutine(FollowAllView(_allViewCamFollow, _delayAllViewMovingFollow));
    }

    private IEnumerator FollowAllView(int targetCamera, float delay)
    {
        ChangeCamera(targetCamera);

        yield return new WaitForSeconds(delay);

        StartCoroutine(DelayChangeCamera(_playerCamNumber, 0));
    }

    private IEnumerator FollowBus(int targetCamera, float delay)
    {
        ChangeCamera(targetCamera);

        yield return new WaitForSeconds(delay);

        ChangeCamera(_playerCamNumber);
    }

    private void ChangeStateJoystick(bool value)
    {
        _joystick.enabled = value;

        if (value == false)
            _joystick.OnPointerUp(null);
    }
}
