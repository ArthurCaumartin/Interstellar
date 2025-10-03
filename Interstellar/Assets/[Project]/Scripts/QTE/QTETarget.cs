using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class QTETarget : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;
    public Vector2Int inputTarget;
    public float speed;
    public Vector2 startPos;
    private float _time;
    private QTEControler _qte;

    public float CurrentTime => _time;
    private bool _isMoving = true;

    public void Init(Vector2Int inputTarget, float speed, QTEControler qte)
    {
        this.inputTarget = inputTarget;
        this.speed = speed;
        _qte = qte;
    }

    void Start()
    {
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (!_isMoving) return;
        _time += Time.deltaTime * speed;
        ((RectTransform)transform).anchoredPosition = Vector2.Lerp(startPos, Vector2.zero, _time);

        if (_time >= 1.1f)
        {
            _qte.OnQTEFail();
            Remove(false);
        }
    }

    public void Remove(bool isWin)
    {
        if (!_isMoving) return;
        _isMoving = false;

        _image.transform.DOScale(Vector3.one * 1.2f, .2f)
        .SetEase(Ease.OutElastic)
        .OnComplete(() => _image.transform.DOScale(Vector3.zero, .1f));

        _image.DOColor(isWin ? _winColor : _loseColor, .4f)
        .OnComplete(() => Destroy(gameObject));
    }
}