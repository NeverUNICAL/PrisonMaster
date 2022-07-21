using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washer : Construction
{
    [SerializeField] private Transform _suitsTransform;
    [SerializeField] private float _rotateSpeed = 6f;
    
    private Vector3 _vector3Rotate;

    private void Start()
    {
        _vector3Rotate = new Vector3(0, 0, _rotateSpeed);
    }

    private void FixedUpdate()
    { 
        _suitsTransform.Rotate(_vector3Rotate);
    }
}
