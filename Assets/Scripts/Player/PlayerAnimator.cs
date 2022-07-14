using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    
    private const string Speed = "Speed";


    private void Update()
    {
        if(_joystick.Dragged)
            _animator.SetFloat(Speed,_joystick.Magnitude);
        else
        {
            _animator.SetFloat(Speed,0);
        }
    }
}
