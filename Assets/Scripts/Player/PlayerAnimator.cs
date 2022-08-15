using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private AssistantTriggerCreo _triggerCreo;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private ParticleSystem _asksVFX;
    [SerializeField] private PlayerFinishTrigger _finishTrigger;
    
    private const string Speed = "Speed";
    private const string Angry = "Angry";
    private const string Salute = "salute";

    private void OnEnable()
    {
        _triggerCreo.Enter += OnEnter;
        _finishTrigger.FinishReached += OnFinish;
    }

    private void OnDisable()
    {
        _triggerCreo.Enter -= OnEnter;
        _finishTrigger.FinishReached -= OnFinish;
    }

    private void Update()
    {
        if(_joystick.Dragged)
            _animator.SetFloat(Speed,_joystick.Magnitude);
        else
        {
            _animator.SetFloat(Speed,0);
        }
    }

    private void OnEnter()
    {
        StartCoroutine(Delay(Salute));
    }

    private IEnumerator Delay(string animation)
    {
        yield return new WaitForSeconds(_delay);

        _animator.SetTrigger(animation);
    }

    private void OnFinish()
    {
        _asksVFX.Play();
        StartCoroutine(Delay(Angry));
    }
}
