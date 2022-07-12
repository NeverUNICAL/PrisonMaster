using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    


    private void Update()
    {
        if(_joystick.Dragged)
            _animator.SetFloat("Speed",_joystick.Magnitude);
        else
        {
            _animator.SetFloat("Speed",0);
        }
    }
}
