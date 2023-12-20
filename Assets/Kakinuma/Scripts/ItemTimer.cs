using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTimer : MonoBehaviour
{
    Slider _slider = null;
    [SerializeField] float _interval = 1; // アイテムごとの使用インターバル _timer
    [Tooltip("～Itemがついたオブジェクト")]
    [SerializeField] GameObject _itemObject = null;

    BomItem _bomItemScript = null;
    float _nowInterval = 0; // アイテムごとの_uiPercent

    //テスト用
    //BomTest _bomItem;
    //[SerializeField] GameObject _item1 = null;

    // Start is called before the first frame update
    void Start()
    {
        _bomItemScript = _itemObject.GetComponent<BomItem>();

        //テスト用
        //_bomItem = _itemObject.GetComponent<BomTest>();
        //_nowInterval = 1;
        _slider = GetComponent<Slider>();
        //_slider.maxValue = _interval;
    }

    // Update is called once per frame
    void Update()
    {
        _nowInterval = 1 - _bomItemScript._uiPercent;

        //テスト用
        //_nowInterval = 1 - _bomItem._uiPercent;

        _slider.value = _nowInterval;
    }
}
