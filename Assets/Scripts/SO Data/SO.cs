using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO : ScriptableObject
{
    [SerializeField]
    private float _value;
    public float value
    {
        get { return _value; }
        set { _value = value; }
    }
    
    
}
