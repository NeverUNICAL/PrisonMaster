using Agava.IdleGame;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _cinemachines;
    [SerializeField] private GlobalTutorial _globalTutorial;
    [SerializeField] private float _delayChangeCamera;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private UpgradesShop _upgradesShop;
    [SerializeField] private Transform _upPanel;
    [SerializeField] private RoomBuyZone _hrBuyZone;
    [SerializeField] private Transform _hrPanel;
    [SerializeField] private CellQueueContainer _queueContainer;
    [SerializeField] private Cell _cell;
    [SerializeField] private AssistantBuyZone _assistantBuyZone;

    [Header("Delayes")]
    [SerializeField] private float _delayAssistantMovingFollow = 6f;
    [SerializeField] private float _delayPrisonerMovingFollow = 14f;
    [SerializeField] private float _delayAllViewMovingFollow = 4f;
    [SerializeField] private float _delayBusMovingFollow = 5f;

    private int _playerCamNumber = 0;
    private int _targetCamNumber = 1;
    private int _assistantCamNumber = 2;
    private int _prisonerCamNumber = 3;
    private int _allViewCamFollow = 4;
    private int _busCamFollow = 5;

    private bool _isFirst = true;

    private const int PriorityValue = 1;
    private const int NotPriorityValue = 0;

    private void OnEnable()
    {
        _assistantBuyZone.Unlocked += OnAssistantUnlock;
        _globalTutorial.GloalTutorialCompleted += OnTutorialCompleted;
        _globalTutorial.PointerShown += OnPointerShown;
        _upgradesShop.SpeedUpgraded += OnSpeedUpgraded;
        _queueContainer.PrisonerEmptyed += OnPrisonerEmptyed;
        _cell.PrisonerSendToPool += OnPoolPrisonerAdded;
    }

    private void OnDisable()
    {
        _assistantBuyZone.Unlocked -= OnAssistantUnlock;
        _globalTutorial.GloalTutorialCompleted -= OnTutorialCompleted;
        _globalTutorial.PointerShown -= OnPointerShown;
        _upgradesShop.SpeedUpgraded -= OnSpeedUpgraded;
        _queueContainer.PrisonerEmptyed -= OnPrisonerEmptyed;
        _cell.PrisonerSendToPool -= OnPoolPrisonerAdded;
    }

    private void Start()
    {
        ChangeCamera(_playerCamNumber);
    }

    private void OnPointerShown(Transform transform, bool isHRZone)
    {
        if (_isFirst)
        {
            _isFirst = false;
            ChangeStateJoystick(false);
            StartCoroutine(FollowTarget(_busCamFollow, _delayBusMovingFollow, 0));
        }
        else
        {
            if (isHRZone == false)
            {
                _cinemachines[_targetCamNumber].m_Follow = transform;
                ChangeStateJoystick(false);
                ChangeCamera(_targetCamNumber);
                StartCoroutine(DelayChangeCamera(_playerCamNumber, _delayChangeCamera));
            }
        }
    }

    private void OnAssistantUnlock(BuyZonePresenter buyZone)
    {
        ChangeStateJoystick(false);
        StartCoroutine(FollowTarget(_assistantCamNumber, _delayAssistantMovingFollow, 0));
    }

    private void OnPrisonerEmptyed(PrisonerMover prisoner, CellQueueContainer cellQueueContainer)
    {
        _cinemachines[_prisonerCamNumber].m_Follow = prisoner.transform;
        _queueContainer.PrisonerEmptyed -= OnPrisonerEmptyed;
    }

    private void OnPoolPrisonerAdded()
    {
        ChangeStateJoystick(false);
        _cinemachines[_targetCamNumber].m_Follow = _assistantBuyZone.transform;
        StartCoroutine(FollowPrisoner(_prisonerCamNumber, _delayPrisonerMovingFollow));
        _cell.PrisonerSendToPool -= OnPoolPrisonerAdded;
    }

    private void OnSpeedUpgraded(int value1, float value2, int value3)
    {
        OnShopUpgraded();
    }

    private void OnShopUpgraded()
    {
        _upPanel.gameObject.SetActive(false);

        ChangeCamera(_targetCamNumber);
        StartCoroutine(DelayChangeCamera(_playerCamNumber, _delayChangeCamera));

        _upgradesShop.SpeedUpgraded -= OnSpeedUpgraded;
    }

    private void OnTutorialCompleted()
    {
        StopAllCoroutines();
        StartCoroutine(FollowTarget(_allViewCamFollow, _delayAllViewMovingFollow, 0));
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

        ChangeStateJoystick(true);
        ChangeCamera(targetCamera);
    }

    //private IEnumerator FollowAssistant(int targetCamera, float delay)
    //{
    //    ChangeCamera(targetCamera);

    //    yield return new WaitForSeconds(delay);

    //    StartCoroutine(DelayChangeCamera(_playerCamNumber, _delayChangeCamera));
    //}

    private IEnumerator FollowPrisoner(int targetCamera, float delay)
    {
        ChangeCamera(targetCamera);

        yield return new WaitForSeconds(delay);

        StartCoroutine(FollowTarget(_targetCamNumber, _delayChangeCamera, _delayChangeCamera));
    }

    private IEnumerator FollowTarget(int targetCamera, float delay, float delayBackCam)
    {
        ChangeCamera(targetCamera);

        yield return new WaitForSeconds(delay);

        StartCoroutine(DelayChangeCamera(_playerCamNumber, delayBackCam));
    }

    //private IEnumerator FollowBus(int targetCamera, float delay)
    //{
    //    ChangeCamera(targetCamera);

    //    yield return new WaitForSeconds(delay);

    //    StartCoroutine(DelayChangeCamera(_playerCamNumber, 0));
    //}

    private void ChangeStateJoystick(bool value)
    {
        _joystick.enabled = value;

        if (value == false)
            _joystick.OnPointerUp(null);
    }
}
