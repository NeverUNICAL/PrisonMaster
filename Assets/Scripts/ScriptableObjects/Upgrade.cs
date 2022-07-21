using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private float _value;
    [SerializeField] private int _price;

    public int Level => _level;
    public float Value => _value;
    public int Price => _price;
}
