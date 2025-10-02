using System;
using UnityEngine.Events;

[Serializable]
public class JumpEvent
{
    public UnityEvent OnEventDone;
    public float eventValue;
}

