using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int SO", menuName = "SOData/IntSO")]
public class IntSO : ScriptableObject
{
    [SerializeField]
    private int _value;
    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
    
}
