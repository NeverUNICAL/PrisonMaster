using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washer : Construction
{
    [SerializeField] private Transform _suitsTransform;
    [SerializeField] private Vector3 _vector3Rotate = new Vector3(0,0,0.8f);
    
    private void Update()
    { 
        _suitsTransform.Rotate(_vector3Rotate);
    }
}
