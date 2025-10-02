using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QTEUI : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private RectTransform _up;
    [SerializeField] private RectTransform _down;
    [SerializeField] private RectTransform _right;
    [SerializeField] private RectTransform _left;
    [Space]
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private Sprite _leftSprite;
    private Vector2 _startScale;

    private void Start()
    {
        _startScale = _container.localScale;
        _container.localScale = Vector3.zero;
    }

    public void ShowUI(bool isShow)
    {
        _container.DOScale(isShow ? _startScale : Vector3.zero, .3f);
    }

    public void PlaceQTETarget(QTETarget target)
    {
        target.transform.SetParent(_container);
        Vector2 position;
        Sprite toSet;
        if (target.inputTarget == Vector2Int.up)
        {
            position = _up.anchoredPosition;
            toSet = _upSprite;
        }
        else if (target.inputTarget == Vector2Int.right)
        {
            position = _right.anchoredPosition;
            toSet = _rightSprite;
        }
        else if (target.inputTarget == Vector2Int.down)
        {
            position = _down.anchoredPosition;
            toSet = _downSprite;
        }
        else
        {
            position = _left.anchoredPosition;
            toSet = _leftSprite;
        }

        ((RectTransform)target.transform).anchoredPosition = position;
        target.startPos = position;
        target.GetComponentInChildren<Image>().sprite = toSet;
    }
}