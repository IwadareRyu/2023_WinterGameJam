using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomItem : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���̎g�p�C���^�[�o��")] float _interval;
    float _timer;
    /// <summary> UI�p�̃A�C�e���C���^�[�o���̊��� </summary>
    float _uiPercent;
    [SerializeField] GameObject _bomPrefab;
    Transform _playerPos;
    private void Start()
    {
        _timer = _interval;
    }

    void Update()
    {
        //�C���^�[�o�������^�C�}�[���������ꍇ�ɏo���B
        if (!(_timer >= _interval))
            _timer += Time.deltaTime;
        //UI�̃X���C�_�[�ɕ\������
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
            //���e�𐶐�����
            Instantiate(_bomPrefab, _playerPos.position, Quaternion.identity);
            _timer = 0;
        }
        
    }
}
