using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QTEControler : MonoBehaviour
{
    [SerializeField] private QTETarget _targetPrefab;
    [SerializeField] private QTEUI _qteUI;
    [SerializeField] private float _speed;
    [SerializeField] private float _spawnDelay;
    private float _spawnTime;

    public UnityEvent OnGoodInput;
    public UnityEvent OnBadInput;


    private void Update()
    {
        _spawnTime += Time.deltaTime;
        if (_spawnTime >= _spawnDelay)
        {
            AddTarget(Random.Range(0, 4));
            _spawnTime = 0;
        }
    }


    private void AddTarget(int directionIndex)
    {
        QTETarget target = Instantiate(_targetPrefab, transform);
        target.Init(directionIndex switch
        {
            0 => Vector2Int.up,
            1 => Vector2Int.right,
            2 => Vector2Int.down,
            3 => Vector2Int.left,
        },
        _speed,
        this);
        _qteUI.PlaceQTETarget(target);
    }

    public void OnDirection(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Vector2Int inputDir = new Vector2Int((int)input.x, (int)input.y);

        // print("in dir : " + inputDir);
        QTETarget[] targetArray = GetComponentsInChildren<QTETarget>();
        for (int i = 0; i < targetArray.Length; i++)
        {
            QTETarget t = targetArray[i].CurrentTime < .05f ? targetArray[i] : null;
            if (t)
            {
                if (t.inputTarget == inputDir)
                {
                    print("success");
                    Destroy(t.gameObject);
                    OnGoodInput.Invoke();
                }
                else
                {
                    print("fail");
                    OnQTEFail();
                }
            }
        }
    }

    public void OnQTEFail()
    {
        OnBadInput.Invoke();
    }
}
