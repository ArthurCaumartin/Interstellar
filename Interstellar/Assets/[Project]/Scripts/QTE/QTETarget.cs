using UnityEngine;

public class QTETarget : MonoBehaviour
{
    public Vector2Int inputTarget;
    public float speed;
    public Vector2 startPos;
    private float _time;
    private QTEControler _qte;

    public float CurrentTime => _time;

    public void Init(Vector2Int inputTarget, float speed, QTEControler qte)
    {
        this.inputTarget = inputTarget;
        this.speed = speed;
        _qte = qte;
    }

    private void Update()
    {
        _time += Time.deltaTime * speed;
        ((RectTransform)transform).anchoredPosition = Vector2.Lerp(startPos, Vector2.zero, _time);

        if (_time >= 1)
        {
            _qte.OnQTEFail();
            Destroy(gameObject);
        }
    }
}