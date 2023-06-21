using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float SO", menuName = "SOData/FloatSO")]
public class FloatSO : ScriptableObject
{
    [SerializeField]
    private float _value = 10f;
    public float Value
    {
        get { return _value; }
        set { _value = value; }
    }
    
    
    private void OnEnable()
    {
        _value = 10f; // Reset value to 10 when the object is created or loaded
    }
}
