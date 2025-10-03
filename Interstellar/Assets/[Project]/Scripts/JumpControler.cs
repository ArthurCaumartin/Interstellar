using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JumpControler : MonoBehaviour
{
    [SerializeField] private Canvas _playerUI;
    [SerializeField] private Image _chargeImage;
    [Space]
    [SerializeField] private JumpSequence _jumpSequence;
    [SerializeField] private QTEControler _qteControler;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _toGainOnInput;
    [Space]
    [SerializeField] private List<JumpEvent> _jumpEventList = new List<JumpEvent>();
    private float _jumpTime;
    [Space]
    [SerializeField] private Sprite _baseSprite;
    [SerializeField] private Sprite _jumpSprite;
    [SerializeField] private Sprite _fallSprite;
    private bool _canJump = true;

    private void Start()
    {
        _playerUI.gameObject.SetActive(false);

        _qteControler.OnGoodInput.AddListener(() =>
        {
            _jumpTime += _toGainOnInput;
            if (_jumpTime >= 1)
                JumpInput(false);
        });

        _qteControler.OnBadInput.AddListener(() =>
        {
            // _jumpTime -= _toGainOnInput * 2;
            JumpInput(false);
        });
    }

    private void Update()
    {
        _chargeImage.fillAmount = _jumpTime;
        _qteControler.SetSpawnDelay(Mathf.Lerp(_maxSpawnDelay, _minSpawnDelay, _jumpTime));
    }

    public void OnJumpCharge(InputValue value)
    {
        if (_jumpSequence.IsJumping) return;

        bool isPresse = value.Get<float>() > .5f;
        // print("rfghu : " + isPresse);
        JumpInput(isPresse);
    }

    public void JumpInput(bool isPresse)
    {
        _qteControler.EnableQTE(isPresse);
        _playerUI.gameObject.SetActive(isPresse);
        GetComponentInChildren<SpriteRenderer>().sprite = isPresse ? _jumpSprite : _fallSprite;

        if (!isPresse)
        {
            Jump();
        }
    }

    private void Jump()
    {
        print("Jump from : " + _jumpTime);
        EventStuff(out float delay, out JumpEvent jumpEvent);
        print("Jump to : " + _jumpTime);
        _jumpSequence.Jump(_jumpTime, delay, jumpEvent, () =>
        {
            GetComponentInChildren<SpriteRenderer>().sprite = _baseSprite;
        });
        _jumpTime = 0;
    }

    private void EventStuff(out float delay, out JumpEvent jumpEvent)
    {
        delay = 0;
        jumpEvent = null;

        if (_jumpTime >= 1)
        {
            float max = -100;
            foreach (var item in _jumpEventList)
            {
                if (item.eventValue > max)
                {
                    max = item.eventValue;
                    delay = item.delay;
                    jumpEvent = item;
                }
            }
            _jumpTime = max;
            return;
        }

        foreach (var item in _jumpEventList)
        {
            if (_jumpTime - item.eventValue < .15f)
            {
                print(_jumpTime - item.eventValue);
                _jumpTime = item.eventValue;
                delay = item.delay;
                jumpEvent = item;
                return;
            }
        }
    }

    private void OnValidate()
    {
        foreach (var item in _jumpEventList)
        {
            if (item.eventPivot)
                item.eventPivot.position = _jumpSequence.GetPos(item.eventValue);
        }
    }
}

