using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpControler : MonoBehaviour
{
    [SerializeField] private JumpSequence _jumpSequence;
    [SerializeField] private QTEControler _qteControler;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _toGainOnInput;
    [Space]
    [SerializeField] private List<JumpEvent> _jumpEventList = new List<JumpEvent>();
    private float _jumpTime;


    private void Start()
    {
        _qteControler.OnGoodInput.AddListener(() =>
        {
            _jumpTime += _toGainOnInput;
        });

        _qteControler.OnBadInput.AddListener(() =>
        {
            _jumpTime -= _toGainOnInput;
        });
    }

    public void OnJumpCharge(InputValue value)
    {
        bool isPresse = value.Get<float>() > .5f;
        // print("rfghu : " + isPresse);
        _qteControler.EnableQTE(isPresse);
        _qteControler.SetSpawnDelay(Mathf.Lerp(_maxSpawnDelay, _minSpawnDelay, _jumpTime));
        if (!isPresse)
        {
            EventStuff();
            print("Jump to : " + _jumpTime);
            _jumpSequence.Jump(_jumpTime, 0.25f);
            _jumpTime = 0;
        }
    }

    private void EventStuff()
    {
        foreach (var item in _jumpEventList)
        {
            if (_jumpTime - item.eventValue < 5)
            {
                _jumpTime = item.eventValue;
                return;
            }
        }
    }
}

