using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTimer : MonoBehaviour
{
    Slider _slider = null;
    [SerializeField] float _interval = 1; // �A�C�e�����Ƃ̎g�p�C���^�[�o�� _timer
    [Tooltip("�`Item�������I�u�W�F�N�g")]
    [SerializeField] GameObject _itemObject = null;

    BomItem _bomItemScript = null;
    float _nowInterval = 0; // �A�C�e�����Ƃ�_uiPercent

    //�e�X�g�p
    //BomTest _bomItem;
    //[SerializeField] GameObject _item1 = null;

    // Start is called before the first frame update
    void Start()
    {
        _bomItemScript = _itemObject.GetComponent<BomItem>();

        //�e�X�g�p
        //_bomItem = _itemObject.GetComponent<BomTest>();
        //_nowInterval = 1;
        _slider = GetComponent<Slider>();
        //_slider.maxValue = _interval;
    }

    // Update is called once per frame
    void Update()
    {
        _nowInterval = 1 - _bomItemScript._uiPercent;

        //�e�X�g�p
        //_nowInterval = 1 - _bomItem._uiPercent;

        _slider.value = _nowInterval;
    }
}
