using DG.Tweening;
using UnityEngine;

public class JumpSequence : MonoBehaviour
{
    [SerializeField] private Transform _playerTr;
    [SerializeField] private Transform _playerSpritePivot;
    [Space]
    [SerializeField] private float _jumpSpeed = 5;
    [SerializeField] private Transform _upPoint;
    [SerializeField] private Transform _groundPoint;

    public float time;

    public bool IsJumping;

    private void Start()
    {
        // Jump(time, 2);
    }

    public void Jump(float targetTime, float fallDelay, JumpEvent jumpEvent)
    {
        IsJumping = true;

        Vector3 target = Vector3.Lerp(_groundPoint.position, _upPoint.position, targetTime);
        Vector3 startPoint = _playerTr.position;
        float distance = Vector3.Distance(startPoint, target);
        Vector3 targetScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.5f, targetTime);

        DOTween.To((time) =>
        {
            _playerTr.position = Vector3.Lerp(startPoint, target, time);
            _playerSpritePivot.localScale = Vector3.Lerp(Vector3.one, targetScale, time);
        }, 0, 1, distance / _jumpSpeed)
        .SetEase(Ease.OutCubic)
        .OnComplete(() =>
        {
            jumpEvent?.OnEventDone.Invoke();
            DOTween.To((time) =>
            {
                _playerTr.position = Vector3.Lerp(target, _groundPoint.position, time);
                _playerSpritePivot.localScale = Vector3.Lerp(targetScale, Vector3.one, time);
            }, 0, 1, distance / _jumpSpeed)
            .SetEase(Ease.InCubic)
            .SetDelay(fallDelay)
            .OnComplete(() => IsJumping = false);
        });
    }

    public Vector3 GetPos(float time)
    {
        return Vector3.Lerp(_groundPoint.position, _upPoint.position, time);
    }
}
