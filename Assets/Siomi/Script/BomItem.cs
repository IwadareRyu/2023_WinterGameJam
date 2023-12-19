using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomItem : MonoBehaviour
{
    [SerializeField, Header("アイテムの使用インターバル")] float _interval;
    float _timer;
    /// <summary> UI用のアイテムインターバルの割合 </summary>
    float _uiPercent;
    [SerializeField] GameObject _bomPrefab;
    Transform _playerPos;
    private void Start()
    {
        _timer = _interval;
    }

    void Update()
    {
        //インターバルよりもタイマーが小さい場合に出す。
        if (!(_timer >= _interval))
            _timer += Time.deltaTime;
        //UIのスライダーに表示する
        UiKousin();
    }

    void UiKousin()
    {
        _uiPercent = _timer / _interval;
    }
    void Action()
    {
        if (_timer >= _interval)
        {
            _playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
            //爆弾を生成する
            Instantiate(_bomPrefab, _playerPos.position, Quaternion.identity);
            _timer = 0;
        }
        
    }
}
