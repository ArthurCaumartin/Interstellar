using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class JumpEvent
{
    public string name;
    public float delay = 1;
    public float eventValue;
    public Transform eventPivot;
    public UnityEvent OnEventDone;
}

